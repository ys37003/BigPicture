using UnityEngine;

public class BaseGameEntity : MonoBehaviour
{
    private eType entityType;
    private eTRIBE_TYPE entityTribe;
    private eJOB_TYPE entityJob;
    private int entityID;
    

    public eType Type
    {
        get
        {
            return entityType;
        }
        private set
        {
            entityType = value;
        }
    }

    public eTRIBE_TYPE Tribe
    {
        get
        {
            return entityTribe;
        }
        private set
        {
            entityTribe = value;
        }
    }

    public eJOB_TYPE Job
    {
        get
        {
            return entityJob;
        }
        private set
        {
            entityJob = value;
        }
    }
    public int ID
    {
        get
        {
            return entityID;
        }
        private set
        {
            entityID = value;
        }
    }

    protected void EntityInit(eType _type , eTRIBE_TYPE _tribe , eJOB_TYPE _job )
    {
        Type = _type;
        Tribe = _tribe;
        Job = _job;
        ID = EntityManager.Instance.GetCount();

        EntityManager.Instance.AddEntity(EntityManager.Instance.GetCount(), this);
    }

    public virtual bool HanleMessage(Telegram _msg)
    {
        return false;
    }

}

