using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AI : BaseGameEntity
{
    private StateMachine stateMachine;
    private SpellAttack attackHandler;
    private Group group;

    private MonsterData data;

    public  StatusData addStatus;
    private Animator animator;
    private NavAgent navAgent;

    private float destinationCheck;
    private float oldDistance;

    private bool attackAble = true;
    [SerializeField]
    protected eJOB_TYPE job_Type;
    [SerializeField]
    private float attackRange = 1.0f;

    public DGSetDestination SetDestination;
    public DGSetFomation SetFomation;
    public DGApproach Approach;

    EnemyHandle enemyHandle;

#region Get,Set
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
   
    public Group Group
    {
        get { return group; }

        set { group = value; }
    }

    public SpellAttack AttackHandler
    {
        get { return attackHandler; }
        set { attackHandler = value; }
    }

    public float DestinationCheck
    {
        get { return destinationCheck; }
        set { destinationCheck = value; }
    }

    public EnemyHandle EnemyHandle
    {
        get
        {
            return enemyHandle;
        }

        set
        {
            enemyHandle = value;
        }
    }
    #endregion

    public bool EndHit()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            if (1.0f < Animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
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

        EnemyHandle.RemoveEnemy();

        if (null == this.Group.EnemyGroup)
        {
            this.EnemyClear();
        }
        else
        {
            if (0 == EnemyHandle.Count())
            {
                GameObject dummy = this.Group.NearestEnemy(this.transform.position);
                if (null != dummy)
                {
                    CEnemy enemy = new CEnemy();
                    enemy.enemy = dummy;
                    enemy.damage = 0;
                    EnemyHandle.Add(enemy);
                }
            }
        }

    }

    public virtual void Walk()
    {
        if(this.DestinationCheck + 1.0f < Time.time)
        {
            this.DestinationCheck = Time.time;
            oldDistance = Vector3.Distance(this.transform.position, this.NavAgent.destination);
            return;
        }

        if(0.01f > Mathf.Abs( oldDistance - Vector3.Distance(this.transform.position, this.NavAgent.destination ) ))
        {
            this.NavAgent.Clear();
        }
    }

    public void Escape()
    {
        this.transform.LookAt(GetEnemyPosition());

        if (this.DestinationCheck + 1.0f < Time.time)
        {
            this.DestinationCheck = Time.time;
            oldDistance = Vector3.Distance(this.transform.position, this.NavAgent.destination);
        }
    }
    public void Hit()
    {
        this.transform.LookAt(GetEnemyPosition());
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
            //Debug.Log("Destination is NULL");
        }
        NavAgent.SetDestination(_destination);
        StartCoroutine(NavAgent.MoveToTarget());
    }

    public void SetEnemy(Group _enemyGroup)
    {
        for(int i = 0; i < _enemyGroup.member.Count; ++ i)
        {
            CEnemy enemy = new CEnemy();
            enemy.enemy = _enemyGroup.member[i].gameObject;
            enemy.damage = 0;
            EnemyHandle.Add(enemy);
        }
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
        if (0 == EnemyHandle.Count())
        {
            return false;
        }

        if ( false == EnemyHandle.EnemyExistCheck())
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
        return EnemyHandle.GetEnemy(0).enemy.transform.position;
    }
    public void EnemyClear()
    {
        EnemyHandle.Clear();
    }

    public float TargetDistance()
    {
        if( Vector3.zero == this.GetEnemyPosition() )
        {
            return 0.0f;
        }
        return Vector3.Distance(this.transform.position, this.GetEnemyPosition());
    }

    public Vector3 EscapePosition(GameObject _entity)
    {
        Vector3 destination;
        NavMeshHit hit;
        
        destination = _entity.transform.position - this.transform.position;

        destination = this.transform.position - destination;
        NavMesh.SamplePosition(destination, out hit, 1, NavMesh.AllAreas);

        return hit.position;
    }

    public Vector3 EscapePosition(Vector3 _entityPos)
    {
        Vector3 destination;
        NavMeshHit hit;

        if (0 == Random.Range(0, 1))
        {
            destination = _entityPos - this.transform.position;

            destination = this.transform.position - destination;
        }
        else
        {
            destination = this.transform.position - this.Group.GetCenter();
            destination = this.transform.position - destination;
        }
        NavMesh.SamplePosition(destination, out hit, 1, NavMesh.AllAreas);
        return hit.position;
    }

    public void DamageReceiver(float _damage)
    {

    }

    public void SetSpeed()
    {
        this.NavAgent.SetSpeed(this.data.StatusData.MoveSpeed );
    }

    public void AddSpeed(float _speed)
    {
        this.NavAgent.SetSpeed(this.navAgent.GetSpeed() + _speed );
    }

    public virtual void StartBattle()
    {

    }

    public virtual void EndBattle()
    {

    }
}
