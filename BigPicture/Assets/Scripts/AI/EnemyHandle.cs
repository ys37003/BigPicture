using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemy
{
    public GameObject enemy;
    public float damage;
}


public class EnemyHandle
{

    public List<CEnemy> enemyList = new List<CEnemy>();

    public List<CEnemy> EnemyList
    {
        get { return enemyList; }
        set { enemyList = value; }
    }

    public List<CEnemy> GetEnemyList()
    {
        return EnemyList;
    }

    public CEnemy GetEnemy(GameObject _enemy)
    {
        for(int i = 0; i < enemyList.Count; ++ i)
        {
            if(_enemy == enemyList[i].enemy)
            {
                return enemyList[i];
            }
        }
        Debug.Log("GetEnemy is Fail");
        return null;
    }

    public CEnemy GetEnemy(int _index)
    {
        
        return enemyList[_index];
    }

    public void Sort()
    {
        enemyList.Sort(delegate (CEnemy A, CEnemy B)
        {
            if (A.damage > B.damage) return 1;
            else if (A.damage < B.damage) return -1;
            return 0;
        });
    }

    public void Clear()
    {
        enemyList.Clear();
    }

    public int Count()
    {
        return enemyList.Count;
    }

    public void Add(CEnemy _enemy)
    {
        if(false == enemyList.Contains(_enemy))
        {
            enemyList.Add(_enemy);
        }
    }
    public void RemoveEnemy()
    {
        for (int i = 0; i < enemyList.Count; ++i)
        {
            if (false == enemyList[i].enemy.activeSelf)
                enemyList.RemoveAt(i);
        }
    }

    public bool EnemyExistCheck()
    {
        if (null == EnemyList[0].enemy || false == EnemyList[0].enemy.activeSelf)
            return false;
        else
            return true;
    }
}
