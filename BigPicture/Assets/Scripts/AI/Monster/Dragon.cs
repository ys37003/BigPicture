using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : AI {

    [SerializeField]
    private BoxCollider colEyeSight;

    [SerializeField]
    private Transform hud_ui_pivot;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    public StatusData Status { get { return Data.StatusData + AddStatus; } }

    // Use this for initialization
    void Start () {
        Data = new MonsterData(eTRIBE_TYPE.DRAGON, eJOB_TYPE.DRAGON, 10, 10, 10, 10, 10, 10, 10, 100, 10, 50);
        AddStatus = new StatusData(0, 0, 0, 0, 0, 0, 0, 0);

        Animator = this.GetComponent<Animator>();
        NavAgent = this.GetComponent<NavAgent>();
        EntityGroup = this.GetComponentInParent<Group>();

        EntityInit(eENTITY_TYPE.MONSTER, eTRIBE_TYPE.DRAGON, job_Type, EntityGroup);
        HUDUI = HUDUIPoolManager.Instance.GetMonsterHUDUI(hud_ui_pivot, Data.StatusData);
        buffUI = this.transform.GetComponent<BuffUI>();
        buffUI.Init(this);

        AttackAble = true;
        // EyeSight Collider 초기화
        //colEyeSight = this.transform.FindChild("EyeSightCol").GetComponent<BoxCollider>();
        colEyeSight.center = new Vector3(0, this.transform.position.y, Data.EyeSight);
        colEyeSight.size = new Vector3(Data.EyeSight * 3, 10, Data.EyeSight * 2);

        SetDelegate();
        EntityGroup.Add(this);
        EnemyHandle = new EnemyHandle(this.gameObject);
        StateMachine = new StateMachine(this);
    }
	
	// Update is called once per frame
	void Update () {
        if (false == WorldManager.Instance.pause)
            StateMachine.Update();


        Debug.Log("Dragon State is " + StateMachine.CurrentState);
    }

    private void SetDelegate()
    {
        switch (job_Type)
        {
            case eJOB_TYPE.DRAGON:
                SetDestination = Delegates.Instance.SetDestination_Boss;
                SetFomation = Delegates.Instance.SetFomation_Foword;
                Approach = Delegates.Instance.Approach_Foword;

                colliderAttack.Init(eTRIBE_TYPE.DRAGON, Animator, Data.StatusData, AddStatus, eDAMAGE_TYPE.PHYSICS, this);
                AttackElement = new BossAttack();
                AttackElement.Init(this, colliderAttack);

                AttackRange = 5.0f;
                break;
        }
    }

    public bool EndDragonBreath()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("DragonBreath"))
        {
            if (0.8f < Animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
                return true;
        }
        return false;
    }

    public bool EndFootStamp()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("FootStamp"))
        {
            if (0.8f < Animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
                return true;
        }
        return false;
    }


    void OnTriggerStay(Collider other)
    {
        eTRIBE_TYPE colType = eTRIBE_TYPE.NULL;

        if ("Human" == other.tag || "Monster" == other.tag)
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

        if (colType != eTRIBE_TYPE.NULL && colType != this.Tribe && 0 == EnemyHandle.Count())
        {
            this.transform.LookAt(other.transform.position);
            this.EntityGroup.EnemyGroup = other.GetComponent<BattleEntity>().EntityGroup;
            this.EntityGroup.EnemyGroup.DispatchMessageGroup(0, this.ID, (int)eMESSAGE_TYPE.I_SEE_YOU, this.EntityGroup);
            this.EntityGroup.DispatchMessageGroup(0, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, this.EntityGroup.EnemyGroup);
        }
    }
}
