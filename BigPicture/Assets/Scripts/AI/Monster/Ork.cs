using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ork : Monster
{
    /// <summary>
    /// 몬스터의 상태를 변화시켜줄 템플릿 스크립트
    /// </summary>
    private StateMachine<Ork> stateMachine;

    /// <summary>
    /// 이동할 목적지가 정해졌을때 몬서터와 목적지 사이의 초기 거리
    /// </summary>
    public float distenceToTarget;

    public GameObject enemy;

    private bool attackAble = true;
    private bool rollingAble = true;

    public bool AttackAble
    {
        get { return attackAble; }
        set { attackAble = value;  }
    }

    public bool RollingAble
    {
        get { return rollingAble; }
        set { rollingAble = value; }
    }

    private BoxCollider colEyeSight;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    [SerializeField]
    private float attackRange = 1.0f;
    void Start()
    {
        EntityInit(eTYPE.MONSTER, eTRIBE_TYPE.Ork, eJOB_TYPE.TANKER);
        
        Data         = DataManager.Instance().GetData(this.Tribe, this.Job);
        Animator     = this.GetComponent<Animator>();
        NavAgent     = this.GetComponent<NavAgent>();
        stateMachine = new StateMachine<Ork>(this);

        // EyeSight Collider 초기화
        colEyeSight = this.transform.FindChild("EyeSightCol").GetComponent<BoxCollider>();
        colEyeSight.center = new Vector3(0, this.transform.position.y, Data.EyeSight / 2);
        colEyeSight.size = new Vector3(Data.EyeSight * 2, 1, Data.EyeSight);


        colliderAttack.Init(eTYPE.MONSTER, Animator, Data.StatusData );
        foreach (AnimationTrigger trigger in Animator.GetBehaviours<AnimationTrigger>())
        {
            trigger.ColliderAttack = colliderAttack;
        }

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

        if(false == ToBattleIdle())
        {
            MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.TO_IDLE, null);
        }
    }

    public void Walk()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Walk");

        // 1초마다 목적지와의 거리를 검사후 줄지 않았을떄 목적지 재설정
        if (this.erorrCheckClock + 0.5f < Time.time)
        {
            if (distenceToTarget - 0.5f <= Vector3.Distance(this.transform.position, this.NavAgent.target))
            {
                NavAgent.target = MathAssist.Instance().RandomVector3(this.transform.position, 30.0f);
            }
            this.erorrCheckClock = Time.time;
        }
    }

    public void Run()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Run");
    }

    public void Attack()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Attack");
        this.transform.LookAt(enemy.transform.position);
    }

    public void Rolling()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Rolling");

        if(false == RollingAble )
        {
            MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);
        }
    }
    ////////////////////////////////////////상태 변화 채크 함수들////////////////////////////////////////////////
    public bool ToBattleIdle()
    {
        if (enemy == null)
            return false;

        if ( attackRange > Vector3.Distance(this.transform.position, enemy.transform.position) )
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
        if (0.5f > Vector3.Distance(this.transform.position, this.NavAgent.target))
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
        eTYPE colType = other.GetComponent<BaseGameEntity>().Type;
        Debug.Log(other.name);
        if (true == (colType == eTYPE.PLAYER || colType == eTYPE.NPC) && 
            enemy == null )
        {
            SetEnemy(other.gameObject);
            SetTarget(enemy.transform.position);
            Vector3 colPos = other.transform.position;
            MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, colPos);
        }
        //if ("Player" == other.tag && enemy == null ) 
        //{
        //    SetEnemy(other.gameObject);
        //    SetTarget(enemy.transform.position);
        //    Vector3 colPos = other.transform.position;
        //    MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, colPos);
        //}
    }

    void OnTriggerExit(Collider other)
    {
        //if ("Player" == other.tag)
        //{
        //    EnemyClear();
        //    SetTarget(other.transform.position);
        //    Vector3 colPos = other.transform.position;
        //    MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, colPos);
        //}
        //Debug.Log("Exit");
    }

    void OnTriggerEnter(Collider other)
    {
        ColliderAttack ct = other.GetComponent<ColliderAttack>();

        if (ct != null && ct.EntitiType == eTYPE.PLAYER)
        {
            if (true == RollingAble)
            {
                MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.TO_ROLLING, null);
            }
            else
            {
                Debug.Log("Monster 피격, 데미지 계산 필요");
            }
        }
        //Debug.Log("Enter");
    }
}