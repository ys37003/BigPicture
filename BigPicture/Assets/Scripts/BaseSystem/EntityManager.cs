using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager
{
    public Dictionary<int, BaseGameEntity> entityDic = new Dictionary<int, BaseGameEntity>();
    private static EntityManager instance;

    private EntityManager()
    {
        //entityDic.Clear();
    }

    public static EntityManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EntityManager();
            }

            return instance;
        }
    }

    public void AddEntity(int _entityID, BaseGameEntity _newEntity)
    {
        entityDic.Add(_entityID, _newEntity);
    }

    public BaseGameEntity IDToEntity(int _entityID)
    {
        return entityDic[_entityID];
    }

    public int GetCount()
    {
        return entityDic.Count;
    }
}
