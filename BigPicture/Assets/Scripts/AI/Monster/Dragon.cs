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

    [SerializeField]
    List<AttackablePart> attackablePart;

    [SerializeField]
    private GameObject Breath;
    [SerializeField]
    private GameObject Stamp;
    [SerializeField]
    private GameObject Sumon;

    public StatusData Status { get { return Data.StatusData + AddStatus; } }

    [SerializeField]
    public SumonList sumonList;
    // Use this for initialization
    void Start () {
        Data = new MonsterData(eTRIBE_TYPE.DRAGON, eJOB_TYPE.DRAGON, 1, 1, 1, 10, 10, 10, 10, 100, 10, 50);
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
        colEyeSight.center = new Vector3(0, this.transform.position.y - 50, Data.EyeSight);
        colEyeSight.size = new Vector3(Data.EyeSight * 3, 50 , Data.EyeSight * 2);

        SetDelegate();
        EntityGroup.Add(this);
        EnemyHandle = new EnemyHandle(this.gameObject);
        StateMachine = new StateMachine(this);
    }
	
	// Update is called once per frame
	void Update () {
        if (false == WorldManager.Instance.pause)
            StateMachine.Update();
<<<<<<< HEAD
=======


        //Debug.Log("Dragon State is " + StateMachine.CurrentState);
>>>>>>> d9d2db784d7519df296e8e108f3ec6ec6f86df26
    }

    private void SetDelegate()
    {
        switch (job_Type)
        {
            case eJOB_TYPE.DRAGON:
                SetDestination = Delegates.Instance.SetDestination_Boss;
                SetFomation = Delegates.Instance.SetFomation_Partner;
                Approach = Delegates.Instance.Approach_Dealer;

                //colliderAttack.Init(eTRIBE_TYPE.DRAGON, Animator, Data.StatusData, AddStatus, eDAMAGE_TYPE.PHYSICS, this);
                AttackElement = new BossAttack();

                Breath.GetComponent<ColliderAttack>().Init(eTRIBE_TYPE.DRAGON, Animator, Data.StatusData,AddStatus, eDAMAGE_TYPE.BLEEDING, this);
                Stamp.GetComponent<ColliderAttack>().Init(eTRIBE_TYPE.DRAGON, Animator, Data.StatusData, AddStatus, eDAMAGE_TYPE.SHOCK, this);
                AttackElement.Init(this, colliderAttack,null , Breath , Stamp , Sumon);

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

    public Vector3 GetHitColider(Vector3 _enemy)
    {
        attackablePart.Sort(delegate (AttackablePart A, AttackablePart B)
        {
            if (false == A.attackablePart && false == B.attackablePart) return 0;
            else if (false == A.attackablePart) return 1;
            else if (false == B.attackablePart) return -1;

            if (Vector3.Distance(_enemy , A.transform.position) < Vector3.Distance(_enemy, B.transform.position)) return 1;
            else if (Vector3.Distance(_enemy, A.transform.position) > Vector3.Distance(_enemy, B.transform.position)) return -1;
            return 0;
        });

        Debug.Log(attackablePart[0].name);
        return attackablePart[0].transform.position;
    }

    public override void BattleIdle()
    {
        EnemyHandle.RemoveEnemy();

        if (null == this.EntityGroup.EnemyGroup)
        {
            this.EnemyClear();
        }
        else
        {
            if (0 == EnemyHandle.Count())
            {
                GameObject dummy = this.EntityGroup.NearestEnemy(this.transform.position);
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

    public override void StartBattle()
    {
        for(int i = 0; i < EnemyHandle.EnemyList.Count; ++i)
        {
            GameObject dummy = EnemyHandle.EnemyList[i].enemy;
            dummy.GetComponent<AI>().AttackRange = 5.0f;
        }
    }

    public override void EndBattle()
    {
        for (int i = 0; i < EnemyHandle.EnemyList.Count; ++i)
        {
            GameObject dummy = EnemyHandle.EnemyList[i].enemy;
            dummy.GetComponent<AI>().AttackRange = 1.2f;
        }
    }
}
