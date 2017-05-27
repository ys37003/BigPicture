using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemy
{
    // 적
    public GameObject enemy;
    // 적이 입힌 데미지
    public float damage;
    // 적과의 거리
    public float distance;
}

public class EnemyHandle
{
    public List<CEnemy> enemyList = new List<CEnemy>();

    private GameObject owner;
    public List<CEnemy> EnemyList
    {
        get { return enemyList; }
        set { enemyList = value; }
    }

    public EnemyHandle(GameObject _owner)
    {
        owner = _owner;
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

    // Sort 공식 : 받은 데미지 - (거리/2)
    public void Sort()
    {
        enemyList.Sort(delegate (CEnemy A, CEnemy B)
        {
            if (0 == A.damage  && 0 == B.damage )
                return 0;

            if (A.damage + (10 - A.distance) > B.damage + (10 - B.distance)) return 1;
            else if (A.damage + (10 - A.distance) < B.damage + (10 - B.distance)) return -1;
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

    public void DistanceUpdate()
    {
        //GameObject owner = 
    }
}
