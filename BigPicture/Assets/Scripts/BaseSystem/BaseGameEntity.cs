using UnityEngine;

public class BaseGameEntity : MonoBehaviour
{
    private eENTITY_TYPE entityType;
    private int entityID;

    public eENTITY_TYPE Type
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

    protected void EntityInit(eENTITY_TYPE _type, int _id)
    {
        Type = _type;
        ID = _id;

        EntityManager.Instance.AddEntity(_id, this);
    }

    public virtual bool HanleMessage(Telegram _msg)
    {
        return false;
    }

}

