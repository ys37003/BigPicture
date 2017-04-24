using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : BaseGameEntity
{
    private StateMachine<AI> stateMachine;
    private Group group;
    private GameObject enemy;
    private MonsterData data;
    public StatusData addStatus;
    private Animator animator;
    private NavAgent navAgent;
    private bool attackAble;

    [SerializeField]
    protected eJOB_TYPE job_Type;
    [SerializeField]
    private float attackRange = 1.0f;


    public DGSetDestination SetDestination;
    public DGSetFomation SetFomation;
    public DGApproach Approach;

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

    public StateMachine<AI> StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }

    public GameObject Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }

    public Group Group
    {
        get { return group; }

        set { group = value; }
    }

    public bool EndHit()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
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

    public bool DieCheck()
    {
        if (0 >= this.data.StatusData.HP)
            return true;
        else
            return false;
    }

    public void Idle()
    {
    }

    public void BattleIdle()
    {
        this.transform.LookAt(GetEnemyPosition());
    }

    public void Walk()
    {

    }

    public void Hit()
    {
    }
    public void Run()
    {
        if (10.0f > Vector3.Distance(this.transform.position, this.GetEnemyPosition()))
            NavAgent.SetDestination(this.GetEnemyPosition());
    }

    public void Attack()
    {
    }

    public void BattleWalk()
    {
        this.transform.LookAt(GetEnemyPosition());
    }
    public void Clear()
    {
        Group.ReMove(this);
        this.gameObject.SetActive(false);
    }

    public void Die()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Die");
    }

    public eSTATE GetCurrentState()
    {
        return StateMachine.GetCurrentState();
    }
    public override bool HanleMessage(Telegram _msg)
    {
        return StateMachine.HandleMessgae(_msg);
    }
    /// <summary>
    /// 목적지 설정
    /// </summary>
    /// <param name="_target"></param>
    public void SetTarget(Vector3 _destination)
    {
        NavAgent.SetDestination(_destination);
        StartCoroutine(NavAgent.MoveToTarget());
    }

    public void SetEnemy(GameObject _enemy)
    {
        Enemy = _enemy;
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
        if (null == Enemy)
            return false;
        else
            return true;
    }

    public Vector3 GetEnemyPosition()
    {
        if (null == Enemy)
        {
            Debug.Log("Enemy is NULL");
            return Vector3.zero;
        }
        return Enemy.transform.position;
    }
    public void EnemyClear()
    {
        Enemy = null;
    }

    public float TargetDistance()
    {
        return Vector3.Distance(this.transform.position, this.GetEnemyPosition());
    }
}
