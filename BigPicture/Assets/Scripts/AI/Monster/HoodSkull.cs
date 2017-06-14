using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoodSkull : AI
{
    [SerializeField]
    private BoxCollider colEyeSight;

    [SerializeField]
    private Transform hud_ui_pivot;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    [SerializeField]
    private GameObject skillSpell;

    [SerializeField]
    private GameObject nomalSpell;
    HpHandle hpHandle;

    public StatusData Status { get { return Data.StatusData + AddStatus; } }

    void Start()
    {
        Data = DataManager.Instance().GetMonsterData(this.Tribe, this.Job);
        AddStatus = new StatusData(0,0,0,0,0,0,0,0);

        Animator = this.GetComponent<Animator>();
        NavAgent = this.GetComponent<NavAgent>();
        EntityGroup = this.GetComponentInParent<Group>();

        EntityInit(eENTITY_TYPE.MONSTER, eTRIBE_TYPE.HOODSKULL, job_Type , EntityGroup );
        HUDUI = HUDUIPoolManager.Instance.GetMonsterHUDUI(hud_ui_pivot, Data.StatusData );
        buffUI = this.transform.GetComponent<BuffUI>();
        buffUI.Init(this);

        AttackAble = true;
        // EyeSight Collider 초기화
        //colEyeSight = this.transform.FindChild("EyeSightCol").GetComponent<BoxCollider>();
        colEyeSight.center = new Vector3(0, this.transform.localPosition.y, Data.EyeSight);
        colEyeSight.size = new Vector3(Data.EyeSight * 3, 10, Data.EyeSight * 2);
        EntityGroup.Add(this);
        SetDelegate();
        this.gameObject.AddComponent<HpHandle>();
        hpHandle = this.transform.GetComponent<HpHandle>();
        EnemyHandle = new EnemyHandle(this.gameObject);
        StateMachine = new StateMachine(this);
    }

    private void SetDelegate()
    {
        switch (job_Type)
        {
            case eJOB_TYPE.DEALER:
                SetDestination = Delegates.Instance.SetDestination_Nomal;
                SetFomation = Delegates.Instance.SetFomation_Dealer;
                Approach = Delegates.Instance.Approach_Dealer;

                colliderAttack.Init(eTRIBE_TYPE.HOODSKULL, Animator, Data.StatusData, AddStatus, eDAMAGE_TYPE.PHYSICS, this);
                AttackElement = new PhysicsAttack();
                foreach (AnimationTrigger trigger in Animator.GetBehaviours<AnimationTrigger>())
                {
                    trigger.ColliderAttack = colliderAttack;
                }

                AttackElement.Init(this, colliderAttack);
                HUDUI.SetJob(eJob.Dealer);
                AttackRange = 1.5f;
                break;
            case eJOB_TYPE.FORWARD:
                SetDestination = Delegates.Instance.SetDestination_Foword;
                SetFomation = Delegates.Instance.SetFomation_Foword;
                Approach = Delegates.Instance.Approach_Foword;
               
                colliderAttack.Init(eTRIBE_TYPE.HOODSKULL, Animator, Data.StatusData, AddStatus, eDAMAGE_TYPE.BLEEDING, this);
                skillSpell.GetComponent<ColliderAttack>().Init(eTRIBE_TYPE.HOODSKULL, Animator, Data.StatusData, AddStatus, eDAMAGE_TYPE.BLEEDING, this);
                AttackElement = new SpellAttack();
                AttackElement.Init(this, colliderAttack, nomalSpell,null,skillSpell);

                HUDUI.SetJob(eJob.Wizard);
                AttackRange = 5.0f;
                break;
            case eJOB_TYPE.SUPPORT:
                SetDestination = Delegates.Instance.SetDestination_Nomal;
                SetFomation = Delegates.Instance.SetFomation_Support;
                Approach = Delegates.Instance.Approach_Support;

                colliderAttack.Init(eTRIBE_TYPE.HOODSKULL, Animator, Data.StatusData, AddStatus, eDAMAGE_TYPE.POISONING, this);
                skillSpell.GetComponent<ColliderAttack>().Init(eTRIBE_TYPE.HOODSKULL, Animator, Data.StatusData, AddStatus, eDAMAGE_TYPE.POISONING, this);
                AttackElement = new SpellAttack();
                AttackElement.Init(this, colliderAttack, nomalSpell,skillSpell);

                HUDUI.SetJob(eJob.Wizard);
                AttackRange = 5.0f;
                break;
        }
    }

    private void Update()
    {
        //colEyeSight.center = new Vector3(0, this.transform.position.y + 1, Data.EyeSight);

       if(false == WorldManager.Instance.pause)
            StateMachine.Update();
    }

    public override void StartBattle()
    {
        StartCoroutine(hpHandle.HpCheck());
    }

    public override void EndBattle()
    {
        StopCoroutine(hpHandle.HpCheck());
    }

    #region Trigger
    void OnTriggerStay(Collider other)
    {
        eTRIBE_TYPE colType = eTRIBE_TYPE.NULL;

        if ("Human" == other.tag )
        {
            try
            {
                colType = other.GetComponent<BaseGameEntity>().Tribe;
            }
            catch
            {
                colType = other.GetComponentInParent<BaseGameEntity>().Tribe;
            }
        }
        
        if ( colType != eTRIBE_TYPE.NULL && colType != this.Tribe && 0 == EnemyHandle.Count())
        {
            this.transform.LookAt(other.transform.position);
            this.EntityGroup.EnemyGroup = other.GetComponent<BattleEntity>().EntityGroup;
            this.EntityGroup.EnemyGroup.DispatchMessageGroup(0, this.ID, (int)eMESSAGE_TYPE.I_SEE_YOU, this.EntityGroup);
            this.EntityGroup.DispatchMessageGroup(0, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, this.EntityGroup.EnemyGroup );
        }
    }

    #endregion
}