using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BaseGameEntity
{
    private MonsterData data;
    private Animator animator;
    private NavAgent navAgent;

    public float erorrCheckClock;

    /// <summary>
    /// 몬스터가 가지고 있을 Data
    /// </summary>
    public MonsterData Data
    {
        get { return data; }
        set { data = value; }
    }

    /// <summary>
    /// 몬스터의 에니메이터
    /// </summary>
    public Animator Animator
    {
        get { return animator; }
        set { animator = value; }
    }

    /// <summary>
    /// 목적지 지정
    /// </summary>
    public NavAgent NavAgent
    {
        get { return navAgent; }
        set { navAgent = value; }
    }

    /// <summary>
    /// 에러확인에 사용
    /// </summary>
    public float EroorCheckClock
    {
        get { return erorrCheckClock; }
        set { erorrCheckClock = value; }
    }
}
