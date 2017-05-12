﻿using UnityEngine;

public class BaseGameEntity : MonoBehaviour
{
    private eENTITY_TYPE entityType;
    private eTRIBE_TYPE  entityTribe;
    private eJOB_TYPE    entityJob;
    private Group        entityGroup;
    private int          entityID;

    public eENTITY_TYPE Type
    {
                get { return entityType; }
        private set { entityType = value; }
    }

    public eTRIBE_TYPE Tribe
    {
                get { return entityTribe; }
        private set { entityTribe = value; }
    }

    public eJOB_TYPE Job
    {
                get { return entityJob; }
        private set { entityJob = value; }
    }

    public int ID
    {
                get { return entityID; }
        private set { entityID = value; }
    }

    public Group EntityGroup
    {
        get { return entityGroup; }
        private set { entityGroup = value; }
    }

    protected void EntityInit(eENTITY_TYPE _type, eTRIBE_TYPE _tribe, eJOB_TYPE _job , Group _group)
    {
        EntityGroup = _group;
        Type  = _type;
        Tribe = _tribe;
        Job   = _job;
        ID    = EntityManager.Instance.GetCount();

        // 생성된 순서대로 EntityDIc에 저장
        EntityManager.Instance.AddEntity(EntityManager.Instance.GetCount(), this);
    }

    public virtual void HanleMessage(Telegram _msg)
    {
    }
}
