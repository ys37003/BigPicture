using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partner : AI
{
    [SerializeField]
    private BoxCollider colEyeSight;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    [SerializeField]
    private BaseGameEntity player;

    void Start()
    {
        EntityInit(eENTITY_TYPE.NPC, eTRIBE_TYPE.HUMAN, job_Type);

        //Data = DataManager.Instance().GetData(this.Tribe, this.Job);
        Data = new MonsterData(this.Tribe, this.Job, 1, 5, new StatusData(1, 1, 1, 1, 1, 1, 1, StatusData.MAX_HP));
        Animator = this.GetComponent<Animator>();
        NavAgent = this.GetComponent<NavAgent>();
        Group = this.GetComponentInParent<Group>();
        AttackAble = true;

        colEyeSight.center = new Vector3(0, this.transform.position.y, Data.EyeSight);
        colEyeSight.size = new Vector3(Data.EyeSight * 3, 1, Data.EyeSight * 2);

        colliderAttack.Init(eENTITY_TYPE.MONSTER, Animator, Data.StatusData);
        foreach (AnimationTrigger trigger in Animator.GetBehaviours<AnimationTrigger>())
        {
            trigger.ColliderAttack = colliderAttack;
        }
        this.Group.Add(this);
        SetDelegate();

        StateMachine = new StateMachine(this);
    }

    private void SetDelegate()
    {
        switch (job_Type)
        {
            case eJOB_TYPE.DEALER:
                SetDestination = Delegates.Instance.SetDestination_Partner;
                AttackRange = 1.0f;
                break;
            case eJOB_TYPE.FORWARD:
                AttackRange = 5.0f;
                break;
            case eJOB_TYPE.SUPPORT:
                AttackRange = 5.0f;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        StateMachine.Update();
    }

    public override void Idle()
    {
        Vector3 playerPos = player.transform.position;
        if( true == this.IsFar(playerPos))
        {
            MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.FLLOW_ME, playerPos);
        }
    }

    public override void Walk()
    {
    }

    bool IsFar(Vector3 _position)
    {
        if (5 < Vector3.Distance(this.transform.position, _position))
        {
            return true;
        }
        return false;
    }
}
