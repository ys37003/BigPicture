using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoodSkull : Monster
{
    /// <summary>
    /// 몬스터의 상태를 변화시켜줄 템플릿 스크립트
    /// </summary>
    private StateMachine<HoodSkull> stateMachine;
    private Group group;
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
        set { attackAble = value; }
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
    eJOB_TYPE job_Type;
    [SerializeField]
    private float attackRange = 1.0f;

    public DGSetDestination SetDestination;
    public DGToWalk ToWalk;
    void Start()
    {
        EntityInit(eENTITY_TYPE.MONSTER, eTRIBE_TYPE.HOODSKULL, job_Type);

        Data = DataManager.Instance().GetData(this.Tribe, this.Job);
        Animator = this.GetComponent<Animator>();
        NavAgent = this.GetComponent<NavAgent>();
        group = this.GetComponentInParent<Group>();

        // EyeSight Collider 초기화
        colEyeSight = this.transform.FindChild("EyeSightCol").GetComponent<BoxCollider>();
        colEyeSight.center = new Vector3(0, this.transform.position.y, Data.EyeSight);
        colEyeSight.size = new Vector3(Data.EyeSight * 3, 1, Data.EyeSight * 2);

        colliderAttack.Init(eENTITY_TYPE.MONSTER, Animator, Data.StatusData);
        foreach (AnimationTrigger trigger in Animator.GetBehaviours<AnimationTrigger>())
        {
            trigger.ColliderAttack = colliderAttack;
        }

        this.GetGroup().AddMember(this);
        SetDelegate();


        stateMachine = new StateMachine<HoodSkull>(this);
    }

    private void SetDelegate()
    {
        switch (job_Type)
        {
            case eJOB_TYPE.DEALER:
                SetDestination = Delegates.Instance.SetDestination_Nomal;
                break;
            case eJOB_TYPE.FOWORD:
                SetDestination = Delegates.Instance.SetDestination_Foword;
                break;
            case eJOB_TYPE.SUPPORT:
                SetDestination = Delegates.Instance.SetDestination_Nomal;
                break;
            case eJOB_TYPE.TANKER:
                SetDestination = Delegates.Instance.SetDestination_Nomal;
                break;
        }
    }
    private void Update()
    {
        stateMachine.Update();
    }

    #region 상태 함수들
    public void Idle()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Idle");
    }

    public void BattleIdle()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is BattleIdle");
        this.transform.LookAt(enemy.transform.position);
    }

    public void Walk()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Walk");

        //if(this.erorrCheckClock + 1.0f < Time.time)
        //{
        //    this.erorrCheckClock = Time.time;
        //    distenceToTarget = Vector3.Distance(this.transform.position, this.NavAgent.target);
        //}


        //// 1초마다 목적지와의 거리를 검사후 줄지 않았을떄 Idle로
        //if (this.erorrCheckClock + 0.5f < Time.time)
        //{
        //    if (distenceToTarget <= Vector3.Distance(this.transform.position, this.NavAgent.target))
        //    {
        //        NavAgent.target = MathAssist.Instance().RandomVector3(this.GetGroup().GetGroupCenter(), 5.0f);
        //    }
            
        //}
    }

    public void Run()
    {
        //float velocity = this.GetComponent<Rigidbody>().velocity.magnitude;
        //float targetVelocity = enemy.GetComponent<Rigidbody>().velocity.magnitude;
        //float time = Vector3.Distance(this.transform.position, enemy.transform.position) / velocity ;
        //Vector3 newTarget = enemy.transform.position + (enemy.transform.forward * targetVelocity * time);
       
        //this.SetTarget(newTarget);
        //Debug.DrawLine(this.transform.position, this.NavAgent.target, Color.red);
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
    }
    #endregion
    #region 상태변화 조건함수

    public bool ToRun()
    {
        return false;
    }
    public bool ToBattleIdle()
    {
        if (enemy == null)
            return false;

        if (attackRange > Vector3.Distance(this.transform.position, enemy.transform.position))
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

    public bool EndRun()
    {
        if (true == this.ToBattleIdle())
            return true;

        return false;
    }

    public bool IsArrive()
    {
        if (0.5f > Vector3.Distance(this.transform.position, this.NavAgent.target))
            return true;

        return false;
    }
    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////Get, Set함수들 //////////////////////////////////////////////////

    #region Get, Set함수들
    public override bool HanleMessage(Telegram _msg)
    {
        return stateMachine.HandleMessgae(_msg);
    }

    public StateMachine<HoodSkull> GetStateMachine() { return stateMachine; }
    public Group GetGroup() { return group; }

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
    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////


    #region Trigger
    void OnTriggerStay(Collider other)
    {
        eENTITY_TYPE colType = eENTITY_TYPE.NULL;
        try
        {
            colType = other.GetComponent<BaseGameEntity>().Type;
        }
        catch
        {
            colType = other.GetComponentInParent<BaseGameEntity>().Type;
        }

        if (true == (colType == eENTITY_TYPE.PLAYER || colType == eENTITY_TYPE.NPC) &&
            enemy == null)
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

        if (ct != null && ct.EntitiType == eENTITY_TYPE.PLAYER)
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
    }
    #endregion
}