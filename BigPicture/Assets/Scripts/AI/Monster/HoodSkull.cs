using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoodSkull : AI
{
    [SerializeField]
    private BoxCollider colEyeSight;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    void Start()
    {

        Data = DataManager.Instance().GetData(this.Tribe, this.Job);
        Animator = this.GetComponent<Animator>();
        NavAgent = this.GetComponent<NavAgent>();
        Group = this.GetComponentInParent<Group>();

        EntityInit(eENTITY_TYPE.MONSTER, eTRIBE_TYPE.HOODSKULL, job_Type , Group);

        AttackAble = true;
        // EyeSight Collider 초기화
        //colEyeSight = this.transform.FindChild("EyeSightCol").GetComponent<BoxCollider>();
        colEyeSight.center = new Vector3(0, this.transform.position.y, Data.EyeSight);
        colEyeSight.size = new Vector3(Data.EyeSight * 3, 1, Data.EyeSight * 2);

        colliderAttack.Init(eTRIBE_TYPE.HOODSKULL, Animator, Data.StatusData);
        //foreach (AnimationTrigger trigger in Animator.GetBehaviours<AnimationTrigger>())
        //{
        //    trigger.ColliderAttack = colliderAttack;
        //}
        this.GroupID = this.Group.member.Count;
        Group.Add(this);
        SetDelegate();


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

                AttackHandler = null;
                foreach (AnimationTrigger trigger in Animator.GetBehaviours<AnimationTrigger>())
                {
                    trigger.ColliderAttack = colliderAttack;
                }

                AttackRange = 1.5f;
                break;
            case eJOB_TYPE.FORWARD:
                SetDestination = Delegates.Instance.SetDestination_Foword;
                SetFomation = Delegates.Instance.SetFomation_Foword;
                Approach = Delegates.Instance.Approach_Foword;

                AttackHandler = new SpellAttack();
                AttackHandler.Init(this.transform.FindChild("Spell").gameObject);

                AttackRange = 5.0f;
                break;
            case eJOB_TYPE.SUPPORT:
                SetDestination = Delegates.Instance.SetDestination_Nomal;
                SetFomation = Delegates.Instance.SetFomation_Support;
                Approach = Delegates.Instance.Approach_Support;
                AttackHandler = new SpellAttack();
                AttackHandler.Init(this.transform.FindChild("Spell").gameObject);
                AttackRange = 5.0f;
                break;
        }
    }

    private void Update()
    {
       StateMachine.Update();
    }
    #region Trigger
    void OnTriggerStay(Collider other)
    {
        eTRIBE_TYPE colType = eTRIBE_TYPE.NULL;

        if ("Human" == other.tag || "Monster" == other.tag)
            colType = other.GetComponent<BaseGameEntity>().Tribe;
        
        if ( colType != eTRIBE_TYPE.NULL && colType != this.Tribe && Enemy == null)
        {
            Debug.Log("Find Enemy");
            this.transform.LookAt(other.transform.position);
            this.Group.EnemyGroup = other.GetComponent<BaseGameEntity>().EntityGroup;
            this.Group.EnemyGroup.DispatchMessageGroup(0, this.ID, (int)eMESSAGE_TYPE.I_SEE_YOU, this.Group);
            this.Group.DispatchMessageGroup(0, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, this.Group.EnemyGroup );
        }
    }

    #endregion
}