﻿using System.Collections;
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

    private CommandController commandController;
    void Start()
    {
        
        //Data = DataManager.Instance().GetData(this.Tribe, this.Job);
        Data = new MonsterData(this.Tribe, this.Job, 1, 5, new StatusData(1, 1, 1, 1, 1, 1, 1, StatusData.MAX_HP));
        Animator = this.GetComponent<Animator>();
        NavAgent = this.GetComponent<NavAgent>();
        Group = this.GetComponentInParent<Group>();
        EntityInit(eENTITY_TYPE.NPC, eTRIBE_TYPE.HUMAN, job_Type , Group );
        AttackAble = true;

        colEyeSight.center = new Vector3(0, this.transform.position.y, Data.EyeSight);
        colEyeSight.size = new Vector3(Data.EyeSight * 3, 1, Data.EyeSight * 2);

        colliderAttack.Init(eTRIBE_TYPE.HUMAN, Animator, Data.StatusData);
        foreach (AnimationTrigger trigger in Animator.GetBehaviours<AnimationTrigger>())
        {
            trigger.ColliderAttack = colliderAttack;
        }
        this.GroupID = this.Group.member.Count;
        this.Group.Add(this);
        SetDelegate();

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
                AttackRange = 1.5f;
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
