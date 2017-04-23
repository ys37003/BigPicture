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
    public GameObject enemy;

    [SerializeField]
    private BoxCollider colEyeSight;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    [SerializeField]
    eJOB_TYPE job_Type;
    [SerializeField]
    private float attackRange = 1.0f;

    public DGSetDestination SetDestination;
    public DGSetFomation SetFomation;
    public DGApproach Approach;

    [SerializeField]
    private float hp;
    void Start()
    {
        EntityInit(eENTITY_TYPE.MONSTER, eTRIBE_TYPE.HOODSKULL, job_Type);

        Data = DataManager.Instance().GetData(this.Tribe, this.Job);
        Animator = this.GetComponent<Animator>();
        NavAgent = this.GetComponent<NavAgent>();
        group = this.GetComponentInParent<Group>();
        AttackAble = true;
        // EyeSight Collider 초기화
        //colEyeSight = this.transform.FindChild("EyeSightCol").GetComponent<BoxCollider>();
        colEyeSight.center = new Vector3(0, this.transform.position.y, Data.EyeSight);
        colEyeSight.size = new Vector3(Data.EyeSight * 3, 1, Data.EyeSight * 2);

        colliderAttack.Init(eENTITY_TYPE.MONSTER, Animator, Data.StatusData);
        foreach (AnimationTrigger trigger in Animator.GetBehaviours<AnimationTrigger>())
        {
            trigger.ColliderAttack = colliderAttack;
        }
        this.GetGroup().Add(this);
        SetDelegate();


        stateMachine = new StateMachine<HoodSkull>(this);
    }

    private void SetDelegate()
    {
        switch (job_Type)
        {
            case eJOB_TYPE.DEALER:
                SetDestination = Delegates.Instance.SetDestination_Nomal;
                SetFomation = Delegates.Instance.SetFomation_Dealer;
                Approach = Delegates.Instance.Approach_Dealer;
                attackRange = 1.0f;
                break;
            case eJOB_TYPE.FORWARD:
                SetDestination = Delegates.Instance.SetDestination_Foword;
                SetFomation = Delegates.Instance.SetFomation_Foword;
                Approach = Delegates.Instance.Approach_Foword;
                attackRange = 5.0f;
                break;
            case eJOB_TYPE.SUPPORT:
                SetDestination = Delegates.Instance.SetDestination_Nomal;
                SetFomation = Delegates.Instance.SetFomation_Support;
                Approach = Delegates.Instance.Approach_Support;
                attackRange = 5.0f;
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
        //Debug.Log(this.Type + this.ID.ToString() + "'State is Idle");
    }

    public void BattleIdle()
    {
        //Debug.Log(this.Type + this.ID.ToString() + "'State is BattleIdle");
        this.transform.LookAt(GetEnemyPosition());
    }

    public void Walk()
    {
        //Debug.Log(this.Type + this.ID.ToString() + "'State is Walk");

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

    public void Hit()
    {
        //Debug.Log(this.Type + this.ID.ToString() + "'State is Hit");
    }
    public void Run()
    {
        //float velocity = this.GetComponent<Rigidbody>().velocity.magnitude;
        //float targetVelocity = enemy.GetComponent<Rigidbody>().velocity.magnitude;
        //float time = Vector3.Distance(this.transform.position, enemy.transform.position) / velocity ;
        //Vector3 newTarget = enemy.transform.position + (enemy.transform.forward * targetVelocity * time);

        //this.SetTarget(newTarget);
        //Debug.DrawLine(this.transform.position, this.NavAgent.target, Color.red);
        //Debug.Log(this.Type + this.ID.ToString() + "'State is Run");
        if (10.0f > Vector3.Distance(this.transform.position, this.GetEnemyPosition()))
            NavAgent.SetDestination(this.GetEnemyPosition());
    }

    public void Attack()
    {
        //Debug.Log(this.Type + this.ID.ToString() + "'State is Attack");
        //this.transform.LookAt(GetEnemyPosition());
    }

    public void BattleWalk()
    {
        //Debug.Log(this.Type + this.ID.ToString() + "'State is BattleWalk");
        this.transform.LookAt(GetEnemyPosition());
    }
    #endregion
    public void Clear()
    {
        group.ReMove(this);
        this.gameObject.SetActive(false);
        //Destroy(this);
    }

    public void Dies()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Die");
    }

    public override eSTATE GetCurrentState()
    {
        return GetStateMachine().GetCurrentState();
    }

    #region 상태변화 조건함수
    //public bool EndAttack()
    //{
    //    if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
    //    {
    //        if (0.5f < Animator.GetCurrentAnimatorStateInfo(0).normalizedTime )
    //            return true;
    //    }
    //    return false;
    //}

    //public bool EndHit()
    //{
    //    if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
    //    {
    //        //if (0.5f < Animator.GetCurrentAnimatorStateInfo(0).normalizedTime )
    //        return true;
    //    }
    //    return false;
    //}
    //public bool IsArrive()
    //{
    //    if (0.5f > this.NavAgent.GetDistance())
    //        return true;

    //    return false;
    //}
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

    //public bool Die()
    //{
    //    if (0 >= this.GetStatus().HP)
    //        return true;
    //    else
    //        return false;
    //}

    /// <summary>
    /// 목적지 설정
    /// </summary>
    /// <param name="_target"></param>
    public void SetTarget(Vector3 _destination)
    {
        NavAgent.SetDestination(_destination);
        StartCoroutine(NavAgent.MoveToTarget());
    }

    /// <summary>
    /// 시간 설정
    /// </summary>
    /// <param name="_clock"></param>
    /// 
    public float GetAttackRange()
    {
        return attackRange;
    }

    public void SetEnemy(GameObject _enemy)
    {
        enemy = _enemy;
    }

    public StatusData GetStatus()
    {
        return this.Data.StatusData;
    }

    public StatusData GetTotalStatus()
    {
        return this.GetStatus() + this.AddStatus;
    }

    public bool EnemyCheck()
    {
        if (null == enemy)
            return false;
        else
            return true;
    }

    public Vector3 GetEnemyPosition()
    {
        if( null == enemy )
        {
            Debug.Log("Enemy is NULL");
            return Vector3.zero;
        }
        return enemy.transform.position;
    }
    public void EnemyClear()
    {
        enemy = null;
    }

    public float TargetDistance()
    {
        return Vector3.Distance(this.transform.position, this.GetEnemyPosition());
    }
    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////


    #region Trigger
    void OnTriggerStay(Collider other)
    {
        eENTITY_TYPE colType = eENTITY_TYPE.NULL;

        if( "Player" == other.tag || "NPC" == other.tag)
            colType = other.GetComponent<BaseGameEntity>().Type;

        if (true == (colType == eENTITY_TYPE.PLAYER || colType == eENTITY_TYPE.NPC) &&
            enemy == null)
        {
            Debug.Log("Find Enemy");
            this.transform.LookAt(other.transform.position);
            this.GetGroup().DispatchMessageGroup(0, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, other.gameObject );
            //this.GetGroup().SetFomation(other.transform.position);
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

    //void OnTriggerEnter(Collider other)
    //{
    //    ColliderAttack ct = other.GetComponent<ColliderAttack>();
       
    //    if (ct != null && ct.EntitiType == eENTITY_TYPE.PLAYER)
    //    {
    //        MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.TO_HIT, null);
    //        //데미지 계산 (물리공격력 + 마법공격력 - 방어력)
    //        //this.Data.StatusData.HP -= (ct.Power - this.GetTotalStatus().Armor);
    //        this.Data.StatusData.HP -= ct.Power;
    //        //if (this.GetStatus().EvasionRate <= Random.Range(0, 100))
    //        //{
    //        //    Debug.Log(other.name + "의 공격 회피");
    //        //}
    //        //else
    //        //{
    //        //    MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.TO_HIT, null);
    //        //    //데미지 계산 (물리공격력 + 마법공격력 - 방어력)
    //        //    this.Data.StatusData.HP -= (ct.Power - this.GetTotalStatus().Armor);
    //        //    //this.Data.StatusData.HP -= (ct.Power);
    //        //    //this.Data.StatusData.HP -= 50.0f;
    //        //    if (true == this.Die())
    //        //        MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.TO_DIE, null);

    //        //    Debug.Log("Hit");
    //        //}
    //    }
    //}
    #endregion
}