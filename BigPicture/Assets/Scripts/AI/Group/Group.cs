using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : BaseGameEntity {
    public List<BaseGameEntity> member = new List<BaseGameEntity>();
    List<BaseGameEntity> dealerList = new List<BaseGameEntity>();
    BaseGameEntity forward;
    List<BaseGameEntity> supportList = new List<BaseGameEntity>();
    // Use this for initialization
    void Start() {
       
    }

    // Update is called once per frame
    void Update() {
        //test.transform.position = GetGroupCenter();
    }
    public void AddMember(BaseGameEntity _member)
    {
        Debug.Log(_member.Job);
        member.Add(_member);

        switch(_member.Job)
        {
            case eJOB_TYPE.DEALER:
                dealerList.Add(_member);
                break;
            case eJOB_TYPE.FORWARD:
                forward = _member;
                break;
            case eJOB_TYPE.SUPPORT:
                supportList.Add(_member);
                break;

            default:
                Debug.Log("AddMember is Fail");
                break;
        }
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

    public void SetFomation()
    {
        Vector3 fomation = forward.transform.position + (-forward.transform.forward * 3);
        fomation += forward.transform.right * -1;

        MessageDispatcher.Instance.DispatchMessage(0.5f, forward.ID, dealerList[0].ID, (int)eMESSAGE_TYPE.SETFOMATION, fomation);

        fomation = forward.transform.position + (-forward.transform.forward * 3);
        fomation += forward.transform.right * 1;

        MessageDispatcher.Instance.DispatchMessage(0.5f, forward.ID, dealerList[1].ID, (int)eMESSAGE_TYPE.SETFOMATION, fomation);

        fomation = forward.transform.position + (-forward.transform.forward * 6);
        MessageDispatcher.Instance.DispatchMessage(0.5f, forward.ID, supportList[0].ID, (int)eMESSAGE_TYPE.SETFOMATION, fomation);

    }

    public void DispatchMessageGroup(float _delay , int _sender, int _message , object _extreInfo)
    {
        for (int i = 0; i < member.Count; ++i)
        {
            int receiver = member[i].ID;

            if (_sender == receiver)
                continue;

            MessageDispatcher.Instance.DispatchMessage(_delay , _sender , receiver, _message , _extreInfo);
        }
    }

    public void DispatchMessageGroup(float _delay, int _sender, int _job , int _message, object _extreInfo)
    {
        for (int i = 0; i < member.Count; ++i)
        {
            BaseGameEntity receiver = member[i];

            if(_job == (int)receiver.Job)
                MessageDispatcher.Instance.DispatchMessage(_delay, _sender, receiver.ID, _message, _extreInfo);
        }
    }
}
