using UnityEngine;

public class BaseGameEntity : MonoBehaviour
{
    private eType entityType;
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

    protected void EntityInit(eType _type, int _id)
    {
        Type = _type;
        ID = EntityManager.Instance.GetCount();

        EntityManager.Instance.AddEntity(EntityManager.Instance.GetCount(), this);
    }

    public virtual bool HanleMessage(Telegram _msg)
    {
        return false;
    }

}

