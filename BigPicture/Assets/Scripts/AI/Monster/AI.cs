using System.Collections.Generic;
using UnityEngine;

public class AI : BaseGameEntity
{
    private StateMachine stateMachine;
    private SpellAttack attackHandler;
    private Group group;
    private int groupID;
    private MonsterData data;
    public StatusData addStatus;
    private Animator animator;
    private NavAgent navAgent;

    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();

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

    public StateMachine StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }

    public List<GameObject> EnemyList
    {
        get { return enemyList; }
        set { enemyList = value; }
    }

    public Group Group
    {
        get { return group; }

        set { group = value; }
    }

    public int GroupID
    {
        get { return groupID; }
        set { groupID = value; }
    }

    public SpellAttack AttackHandler
    {
        get { return attackHandler; }
        set { attackHandler = value; }
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
            if (1.0f < Animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
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
        if (0.5f > this.NavAgent.GetDistance() || Vector3.zero ==  this.NavAgent.GetDestination())
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

    public virtual void Idle()
    {
    }

    public void BattleIdle()
    {
        this.transform.LookAt(GetEnemyPosition());

        RemoveEnemy();

        if (null == this.Group.EnemyGroup)
        {
            this.EnemyClear();
        }
        else
        {
            if (0 == EnemyList.Count)
            {
                GameObject dummy = this.Group.NearestEnemy(this.transform.position);
                if (null != dummy)
                    this.EnemyList.Add(dummy);
            }
        }

    }

    void RemoveEnemy()
    {
        //if (null == EnemyList[0] || false == EnemyList[0].activeSelf && 0 < EnemyList.Count)
        //{
        //    EnemyList.RemoveAt(0);
        //    RemoveEnemy();
        //}
        for(int i = 0; i < enemyList.Count;  ++i )
        {
            if (false == enemyList[i].activeSelf)
                enemyList.RemoveAt(i);
        }

    }
    public virtual void Walk()
    {
    }

    public void Escape()
    {
        this.transform.LookAt(GetEnemyPosition());

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
    }

    public eSTATE GetCurrentState()
    {
        return StateMachine.GetCurrentState();
    }
    public override void HanleMessage(Telegram _msg)
    {
        StateMachine.HandleMessgae(_msg);
    }
    /// <summary>
    /// 목적지 설정
    /// </summary>
    /// <param name="_target"></param>
    public void SetTarget(Vector3 _destination)
    {
        if( _destination == Vector3.zero)
        {
            Debug.Log("Destination is NULL");
        }
        NavAgent.SetDestination(_destination);
        StartCoroutine(NavAgent.MoveToTarget());
    }

    public void SetEnemy(GameObject _enemy)
    {
        if(false == EnemyList.Contains(_enemy))
            EnemyList.Add(_enemy);
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
        if (0 == enemyList.Count)
        {
            return false;
        }

        if ( null == EnemyList[0] || false == EnemyList[0].activeSelf)
            return false;
        else
            return true;
    }

    public Vector3 GetEnemyPosition()
    {
        if (false == this.EnemyCheck())
        {
            //this.EnemyClear();
            return Vector3.zero;
        }
        return EnemyList[0].transform.position;
    }
    public void EnemyClear()
    {
        EnemyList.Clear();
    }

    public float TargetDistance()
    {
        if( Vector3.zero == this.GetEnemyPosition() )
        {
            return 0.0f;
        }
        return Vector3.Distance(this.transform.position, this.GetEnemyPosition());
    }

    public Vector3 Escape(GameObject _entity)
    {
        Vector3 destination = _entity.transform.position - this.transform.position;

        destination -= this.transform.position;
        destination *= 3.0f;
        //destination *= 10;

        return destination;
    }
}
