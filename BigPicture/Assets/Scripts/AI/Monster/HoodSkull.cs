﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoodSkull : AI
{
    public GameObject enemy;

    [SerializeField]
    private BoxCollider colEyeSight;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    void Start()
    {
        EntityInit(eENTITY_TYPE.MONSTER, eTRIBE_TYPE.HOODSKULL, job_Type);

        Data = DataManager.Instance().GetData(this.Tribe, this.Job);
        Animator = this.GetComponent<Animator>();
        NavAgent = this.GetComponent<NavAgent>();
        Group = this.GetComponentInParent<Group>();
        AttackAble = true;
        // EyeSight Collider 초기화
        //colEyeSight = this.transform.FindChild("EyeSightCol").GetComponent<BoxCollider>();
        colEyeSight.center = new Vector3(0, this.transform.position.y, Data.EyeSight);
        colEyeSight.size = new Vector3(Data.EyeSight * 3, 1, Data.EyeSight * 2);

        colliderAttack.Init(eENTITY_TYPE.MONSTER, Animator, Data.StatusData);
        foreach (AnimationTrigger trigger in Animator.GetBehaviours<AnimationTrigger>())
        {
            trigger.ColliderAttack = colliderAttack;
        }
        this.Group.Add(this);
        SetDelegate();


        base.StateMachine = new StateMachine<AI>(this);
    }

    private void SetDelegate()
    {
        switch (job_Type)
        {
            case eJOB_TYPE.DEALER:
                SetDestination = Delegates.Instance.SetDestination_Nomal;
                SetFomation = Delegates.Instance.SetFomation_Dealer;
                Approach = Delegates.Instance.Approach_Dealer;
                AttackRange = 1.0f;
                break;
            case eJOB_TYPE.FORWARD:
                SetDestination = Delegates.Instance.SetDestination_Foword;
                SetFomation = Delegates.Instance.SetFomation_Foword;
                Approach = Delegates.Instance.Approach_Foword;
                AttackRange = 5.0f;
                break;
            case eJOB_TYPE.SUPPORT:
                SetDestination = Delegates.Instance.SetDestination_Nomal;
                SetFomation = Delegates.Instance.SetFomation_Support;
                Approach = Delegates.Instance.Approach_Support;
                AttackRange = 5.0f;
                break;
        }
    }
    private void Update()
    {
        base.StateMachine.Update();
    }
    #region Trigger
    void OnTriggerStay(Collider other)
    {
        eENTITY_TYPE colType = eENTITY_TYPE.NULL;

        if( "Player" == other.tag || "NPC" == other.tag)
            colType = other.GetComponent<BaseGameEntity>().Type;

        if (true == (colType == eENTITY_TYPE.PLAYER || colType == eENTITY_TYPE.NPC) &&
            enemy == null)
        {
            Debug.Log("Find Enemy");
            this.transform.LookAt(other.transform.position);
            this.Group.DispatchMessageGroup(0, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, other.gameObject );
        }
    }

    #endregion
}