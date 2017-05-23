using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : BaseGameEntity
{
    public List<BaseGameEntity> member = new List<BaseGameEntity>();
    [SerializeField]
    private Transform center;
    private Group enemyGroup;

    public Group EnemyGroup
    {
        get
        {
            if (null == enemyGroup)
                return null;

            return enemyGroup;
        }
        set { enemyGroup = value; }
    }

    // Use this for initialization
    void Start()
    {
        GroupManager.Instance.Add(this);
    }
    public void Add(BaseGameEntity _member)
    {
        member.Add(_member);
    }

    public void ReMove(BaseGameEntity _member)
    {
        member.Remove(_member);
    }

    public Transform GetCenter(Vector3 _target)
    {
        for (int i = 0; i < member.Count; ++i)
            center.position += member[i].transform.position;

        center.position /= member.Count;

        center.LookAt(_target);

        return center;
    }

    public Vector3 GetCenter()
    {
        for (int i = 0; i < member.Count; ++i)
            center.position += member[i].transform.position;

        center.position /= member.Count;
        return center.position;
    }

    public List<BaseGameEntity> JobToEntitys(eJOB_TYPE _job)
    {
        List<BaseGameEntity> entitys = new List<BaseGameEntity>();

        for (int i = 0; i < member.Count; ++i)
        {
            if (_job == member[i].Job)
                entitys.Add(member[i]);
        }
        return entitys;
    }
    public BaseGameEntity JobToEntity(eJOB_TYPE _job)
    {
        for (int i = 0; i < member.Count; ++i)
        {
            if (_job == member[i].Job)
                return member[i];
        }
        Debug.Log("JobToEntity is Fail");

        return null;
    }

    public BaseGameEntity TypeToEntity(eENTITY_TYPE _job)
    {
        for (int i = 0; i < member.Count; ++i)
        {
            if (_job == member[i].Type)
                return member[i];
        }
        Debug.Log("TypeToEntity is Fail");

        return null;
    }

    public BaseGameEntity IndexToEntity(int _index)
    {
        return member[_index];
    }

    public void SetFomation(Vector3 _enemyPos)
    {
        BaseGameEntity forward = this.JobToEntity(eJOB_TYPE.FORWARD);
        List<BaseGameEntity> dealerList = this.JobToEntitys(eJOB_TYPE.DEALER);
        List<BaseGameEntity> supportList = this.JobToEntitys(eJOB_TYPE.SUPPORT);

        Debug.Log("SetFomation");
        ///// 수정 필요
        Vector3 fomation = forward.transform.position + (-forward.transform.forward * 3);
        fomation += forward.transform.right * -1;

        MessageDispatcher.Instance.DispatchMessage(0.5f, forward.ID, dealerList[0].ID, (int)eMESSAGE_TYPE.SET_FOMATION, fomation);
        fomation = Vector3.zero;

        fomation = forward.transform.position + (-forward.transform.forward * 3);
        fomation += forward.transform.right * 1;

        MessageDispatcher.Instance.DispatchMessage(0.5f, forward.ID, dealerList[1].ID, (int)eMESSAGE_TYPE.SET_FOMATION, fomation);
        fomation = Vector3.zero;

        fomation = forward.transform.position + (-forward.transform.forward * 6);
        MessageDispatcher.Instance.DispatchMessage(0.5f, forward.ID, supportList[0].ID, (int)eMESSAGE_TYPE.SET_FOMATION, fomation);
        fomation = Vector3.zero;
    }

    public void DispatchMessageGroup(float _delay, int _sender, int _message, object _extreInfo)
    {
        for (int i = 0; i < member.Count; ++i)
        {
            int receiver = member[i].ID;

            MessageDispatcher.Instance.DispatchMessage(_delay, _sender, receiver, _message, _extreInfo);
        }
    }

    public void DispatchMessageGroup(float _delay, int _sender, int _job, int _message, object _extreInfo)
    {
        for (int i = 0; i < member.Count; ++i)
        {
            BaseGameEntity receiver = member[i];

            if (_job == (int)receiver.Job)
                MessageDispatcher.Instance.DispatchMessage(_delay, _sender, receiver.ID, _message, _extreInfo);
        }
    }

    public void Command_ComeOn()
    {
        DispatchMessageGroup(0, 0, (int)eMESSAGE_TYPE.COMMAND_COME_ON, null);
    }
    public void Command_Focusing()
    {
        Character character = this.TypeToEntity(eENTITY_TYPE.PLAYER).gameObject.GetComponent<Character>();
        DispatchMessageGroup(0, 0, (int)eMESSAGE_TYPE.FIND_ENEMY, character.Group.EnemyGroup );
    }

    public GameObject RandomEntity()
    {
        while(true == EnemyGroup.BattleAble())
        {
            int random = Random.Range(0, member.Count);

            if (true == member[random].gameObject.activeSelf)
                return member[random].gameObject;
        }
        return null;
    }

    public bool BattleAble()
    {
        for (int i = 0; i < member.Count; ++i)
        {
            if (true == member[i].gameObject.activeSelf)
                return true;
        }
        return false;
    }

    public GameObject NearestEnemy(Vector3 _entityPos)
    {
        if ( null == this.EnemyGroup || false == this.EnemyGroup.BattleAble())
            return null;

        GameObject dummy = EnemyGroup.member[0].gameObject;

        for (int i = 0; i < EnemyGroup.member.Count; ++i)
        {
            if(Vector3.Distance(dummy.transform.position , _entityPos) > Vector3.Distance(EnemyGroup.member[i].transform.position, _entityPos))
            {
                if (2 > this.EnemyCount(EnemyGroup.member[i]))
                {
                    dummy = EnemyGroup.member[i].gameObject;
                }
            }
        }
        return dummy;
    }

    public int EnemyCount(BaseGameEntity _entity)
    {
        int count = 0;

        for (int i = 0; i < member.Count; ++i)
        {
            try
            {
                for (int j = 0; j < member[i].GetComponent<AI>().EnemyList.Count; ++j)
                {
                    if (_entity.gameObject == member[i].GetComponent<AI>().EnemyList[j].enemy)
                    {
                        ++count;
                    }
                }
            }
            catch
            {
                // 나중에 Character 클래스에 enemyList를 만들어야 하나... 없어도 되는 변수 같기도하다
                Debug.Log("Player EnmeyList don't exist");
            }
        }
        return count;
    }
}