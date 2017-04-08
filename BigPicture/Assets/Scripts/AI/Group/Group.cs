using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : BaseGameEntity {
    public List<GameObject> member = new List<GameObject>();
    public GameObject test;
    [SerializeField]
    private float groupDistence;

    Vector3 center;
    // Use this for initialization
    void Start() {
        for (int i = 0; i < this.transform.childCount; ++i)
            member.Add(this.transform.GetChild(i).gameObject);
    }

    // Update is called once per frame
    void Update() {
        test.transform.position = GetGroupCenter();
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

    public void SendMessageGroup(float _delay , int _sender, int _message , object _extreInfo)
    {
        for (int i = 0; i < member.Count; ++i)
        {
            int _receiver = member[i].GetComponent<BaseGameEntity>().ID;
            MessageDispatcher.Instance.DispatchMessage(_delay , _sender , _receiver, _message , _extreInfo);
        }
    }
}
