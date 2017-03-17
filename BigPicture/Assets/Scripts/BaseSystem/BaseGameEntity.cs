

public abstract class BaseGameEntity
{
    protected ENTITY_TYPE entityType
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

    protected int entityID
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
    protected void SetEntity(ENTITY_TYPE _eEntity_Type, int _entityID)
    {
        entityType = _eEntity_Type;
        entityID = _entityID;
    }
}

