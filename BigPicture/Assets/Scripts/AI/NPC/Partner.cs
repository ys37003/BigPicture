using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partner : AI, ICharacter
{
    [SerializeField]
    private BoxCollider colEyeSight;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    [SerializeField]
    private BaseGameEntity player;

    private CommandController commandController;

    public StatusData Status { get { return Data.StatusData; } }

    public int SkillPoint { get; set; }

    private eDAMAGE_TYPE damageType = eDAMAGE_TYPE.PHYSICS;
    public  eDAMAGE_TYPE DamageType
    {
                get { return damageType; }
        private set { damageType = value; }
    }

    public void Init(BoxCollider _colEyeSight , ColliderAttack _colliderAttack  , BaseGameEntity _player , eJOB_TYPE _job)
    {
        colEyeSight = _colEyeSight;
        colliderAttack = _colliderAttack;
        player = _player;
        job_Type = _job;
    }
    void Start()
    {
        Data = DataManager.Instance().GetMonsterData(eTRIBE_TYPE.HOODSKULL, this.Job);
        AddStatus = new StatusData(0, 0, 0, 0, 0, 0, 0, 0);
        //Data = new MonsterData(this.Tribe, this.Job, 1, 5, new StatusData(1, 1, 1, 1, 1, 1, 1, StatusData.MAX_HP));
        Animator = this.GetComponent<Animator>();
        NavAgent = this.GetComponent<NavAgent>();
        Group = this.GetComponentInParent<Group>();
        EntityInit(eENTITY_TYPE.NPC, eTRIBE_TYPE.HUMAN, job_Type , Group );
        AttackAble = true;
        SkillPoint = 5;

        colEyeSight.center = new Vector3(0, this.transform.position.y, Data.EyeSight);
        colEyeSight.size = new Vector3(Data.EyeSight * 3, 10, Data.EyeSight * 2);

        
        this.Group.Add(this);
        TeamManager.Instance.AddCharacter(this);
        SetDelegate();

        EnemyHandle = new EnemyHandle(this.gameObject);
        StateMachine = new StateMachine(this);
        commandController = new CommandController(this);
    }

    private void SetDelegate()
    {
        switch (job_Type)
        {
            case eJOB_TYPE.DEALER:
                SetDestination = Delegates.Instance.SetDestination_Partner;
                SetFomation = Delegates.Instance.SetFomation_Partner;
                Approach = Delegates.Instance.Approach_Dealer;

                colliderAttack.Init(eTRIBE_TYPE.HUMAN, Animator, Data.StatusData , AddStatus, eDAMAGE_TYPE.PHYSICS );
                AttackElement = new PhysicsAttack();
                foreach (AnimationTrigger trigger in Animator.GetBehaviours<AnimationTrigger>())
                {
                    trigger.ColliderAttack = colliderAttack;
                }

                AttackRange = 2.0f;
                break;
            case eJOB_TYPE.FORWARD:
                AttackRange = 5.0f;
                AttackElement = null;
                break;
            case eJOB_TYPE.SUPPORT:
                AttackRange = 5.0f;
                AttackElement = null;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        colEyeSight.center = new Vector3(0, this.transform.position.y + 1, Data.EyeSight);
        StateMachine.Update();
    }

    public override void HanleMessage(Telegram _msg)
    {
        if (true == StateMachine.HandleMessgae(_msg))
            return;

        if (true == commandController.HandleMessgae(_msg))
            return;
    }

    public override void Idle()
    {
        Vector3 playerPos = player.transform.position;
        if( true == this.IsFar(playerPos))
        {
            MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.FLLOW_ME, playerPos);
        }
    }

    bool IsFar(Vector3 _position)
    {
        if (5 < Vector3.Distance(this.transform.position, _position))
        {
            return true;
        }
        return false;
    }

    //void OnTriggerStay(Collider other)
    //{
    //    eTRIBE_TYPE colType = eTRIBE_TYPE.NULL;

    //    if ("Human" == other.tag || "Monster" == other.tag)
    //        colType = other.GetComponent<BaseGameEntity>().Tribe;

    //    if (colType != eTRIBE_TYPE.NULL && colType != this.Tribe && Enemy == null)
    //    {
    //        Debug.Log("Find Enemy");
    //        this.transform.LookAt(other.transform.position);
    //        this.Group.DispatchMessageGroup(0, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, other.gameObject);
    //    }
    //}
}
