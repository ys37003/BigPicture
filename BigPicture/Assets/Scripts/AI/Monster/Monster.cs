using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BaseGameEntity
{
    private MonsterData data;
    public StatusData addStatus;
    private Animator animator;
    private NavAgent navAgent;
    private bool attackAble;

    public float erorrCheckClock;


    public bool AttackAble
    {
        get { return attackAble; }
        set { attackAble = value; }
    }

    /// <summary>
    /// 몬스터가 가지고 있을 Data
    /// </summary>
    public MonsterData Data
    {
        get { return data; }
        set { data = value; }
    }

    public StatusData AddStatus
    {
        get { return addStatus; }
        set { addStatus = value; }
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

    public bool EndHit()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            //if (0.5f < Animator.GetCurrentAnimatorStateInfo(0).normalizedTime )
            return true;
        }
        return false;
    }

    public bool EndAttack()
    {
       
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            if (0.5f < Animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
                return true;
        }
        return false;
    }

    public bool EndDie()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            //if (1.0f < Animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
                return true;
        }
        return false;
    }

    public bool IsArrive()
    {
        if (0.5f > this.NavAgent.GetDistance())
            return true;

        return false;
    }

    public bool Die()
    {
        Debug.Log("fgggggggg");
        if (0 >= this.data.StatusData.HP)
            return true;
        else
            return false;
    }
    
    public virtual eSTATE GetCurrentState()
    {
        return eSTATE.NULL;
    }
}
