﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ork : Monster
{
    /// <summary>
    /// 몬스터의 상태를 변화시켜줄 템플릿 스크립트
    /// </summary>
    StateMachine<Ork> stateMachine;

    /// <summary>
    /// 이동할 목적지가 정해졌을때 몬서터와 목적지 사이의 초기 거리
    /// </summary>
    public float distenceToTarget;

    public GameObject enemy;

    bool attackAble = true;

    public bool AttackAble
    {
        get { return attackAble; }
        set { attackAble = value;  }
    }

    BoxCollider colEyeSight;
    void Start()
    {
        EntityInit(eTYPE.MONSTER, eTRIBE_TYPE.Ork, eJOB_TYPE.TANKER);
        
        Data = DataManager.Instance().GetData(this.Tribe, this.Job);
        Animator = this.GetComponent<Animator>();
        NavAgent = this.GetComponent<NavAgent>();
        stateMachine = new StateMachine<Ork>(this);
        // EyeSight Collider 초기화
        colEyeSight = this.GetComponent<BoxCollider>();
        Vector3 colCenter = new Vector3(0, this.transform.position.y, Data.EyeSight / 2);
        Vector3 colSize = new Vector3(Data.EyeSight * 2, 1, Data.EyeSight);

        colEyeSight.center = colCenter;
        colEyeSight.size = colSize;


    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void Idle()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Idle");
    }
    public void BattleIdle()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is BattleIdle");
        this.transform.LookAt(enemy.transform.position);

        if(true == AttackAble)
        {
            MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.TO_ATTACK, null);
        }
    }
    public void Walk()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Walk");

        // 1초마다 목적지와의 거리를 검사후 줄지 않았을떄 목적지 재설정
        if (this.erorrCheckClock + 1.0f < Clock.Instance.GetTime())
        {
            if (distenceToTarget - 0.5f <= Vector3.Distance(this.transform.position, this.NavAgent.target))
            {
                NavAgent.target = MathAssist.Instance().RandomVector3(this.transform.position, 30.0f);
            }
            this.erorrCheckClock = Clock.Instance.GetTime();
        }
    }

    public void Run()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Run");
    }

    public void Attack()
    {
        //if (Animator.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
        //{
            
        //    SetClock(Time.time);
        //}
        Debug.Log(this.Type + this.ID.ToString() + "'State is Attack");
        this.transform.LookAt(enemy.transform.position);
    }

////////////////////////////////////////상태 변화 채크 함수들////////////////////////////////////////////////
    public bool ToBattleIdle()
    {
        if (enemy == null)
            return false;

        if (Data.Range > Vector3.Distance(this.transform.position, enemy.transform.position) )
        {
            return true;
        }
        return false;
    }

    public bool EndAttack()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
        {
            Debug.Log("Attack3");
            return true;
        }
        return false;
    }
    /// <summary>
    /// 상태 머신에 메세지 송출
    /// </summary>

    /// <summary>
    /// Idle 상태에서 5초후 true반환
    /// MonsterClock 설정은 Idle상태의 Enter함수에서
    /// </summary>
    //public bool IdleToWalk()
    //{
    //    if (this.MonsterClock + 5.0f < Clock.Instance.GetTime())
    //        return true;

    //    return false;
    //}

    /// <summary>
    /// 이동한지 5초후 또는 목적지에 도착했을때 true반환
    /// </summary>
    public bool ToIdle()
    {
        if ( true == IsArrive() )
            return true;

        return false;
    }

    public bool IsArrive()
    {
        if (1.0f > Vector3.Distance(this.transform.position, this.NavAgent.target))
            return true;

        return false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////Get, Set함수들 //////////////////////////////////////////////////
    public override bool HanleMessage(Telegram _msg)
    {
        return stateMachine.HandleMessgae(_msg);
    }

    public StateMachine<Ork> GetStateMachine() { return stateMachine; }
    /// <summary>
    /// 목적지 설정
    /// </summary>
    /// <param name="_target"></param>
    public void SetTarget(Vector3 _target)
    {
        NavAgent.target = _target;
        distenceToTarget = Vector3.Distance(this.transform.position, _target);
    }
    /// <summary>
    /// 시간 설정
    /// </summary>
    /// <param name="_clock"></param>
    public void SetClock(float _clock)
    {
        MonsterClock = _clock;
        EroorCheckClock = _clock;
    }

    public void SetEnemy(GameObject _enemy)
    {
        enemy = _enemy;
    }

    public void EnemyClear()
    {
        enemy = null;
    }

    //////////////////////////////////////////////////////////////////////////////////////////

    void OnTriggerStay(Collider other)
    {
        //eType colType = other.GetComponent<BaseGameEntity>().Type;

        //if(colType == eType.PLAYER || colType == eType.NPC )
        //{
        //    Vector3 colPos = other.transform.position;
        //    MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eChangeState.FIND_ENEMY, colPos);
        //}
        if ("Player" == other.tag && enemy == null )
        {
            SetEnemy(other.gameObject);
            SetTarget(enemy.transform.position);
            Vector3 colPos = other.transform.position;
            MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, colPos);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if ( "Player" == other.tag )
        {
            SetEnemy(other.gameObject);
            SetTarget(other.transform.position);
            Vector3 colPos = other.transform.position;
            MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, colPos);
        }
    }
}