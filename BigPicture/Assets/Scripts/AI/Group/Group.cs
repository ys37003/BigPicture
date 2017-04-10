using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : BaseGameEntity {
    public List<BaseGameEntity> member = new List<BaseGameEntity>();
    public GameObject test;
    [SerializeField]
    private float groupDistence;

    Vector3 center;
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

    public Vector3 GetGroupCenter()
    {
        for (int i = 0; i < member.Count; ++i)
        {
            center += member[i].transform.position;
        }
        Vector3 dummy = center / member.Count;
        center = Vector3.zero;
        return dummy;
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
