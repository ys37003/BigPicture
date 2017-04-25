using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : Singleton<GroupManager> {

    [SerializeField]
    List<Group> GroupList = new List<Group>();
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Add(Group _group)
    {
        GroupList.Add(_group);
    }

    public int Lenght()
    {
        return GroupList.Count;
    }

    public Group IDToGroup(int _id)
    {
        return GroupList[_id];
    }

    public Group FindNearGroup(Group _group)
    {
        Group nearGroup = GroupList[0];
        
        for (int i = 0; i < GroupList.Count; ++i )
        {
            if (_group == GroupList[i])
                continue;
            if(Vector3.Distance(nearGroup.GetCenter() , _group.GetCenter()) > Vector3.Distance(GroupList[i].GetCenter(), _group.GetCenter()))
            {
                nearGroup = GroupList[i];
            }
        }
        return nearGroup;
    }
}
