using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BaseGameEntity
{
    
    MonsterData data;
    Animator animator;
    NavAgent navAgent;
    public float monsterClock;
    public float erorrCheckClock;

    /// <summary>
    /// State변경에 필요한 고유 시간
    /// </summary>
    public float MonsterClock
    {
        get { return monsterClock; }
        set { monsterClock = value; }
    }
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
