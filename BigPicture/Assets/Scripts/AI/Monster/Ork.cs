using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ork : Monster {

    StateMachine<Ork> stateMachine;
    MonsterData data;
    Animator animator;
    NavAgent navAgent;
    public float clock;
    void Start()
    {
        EntityInit( eType.MONSTER , eTRIBE_TYPE.Ork ,eJOB_TYPE.TANKER );
        stateMachine = new StateMachine<Ork>(this);
        data = DataManager.Instance().GetData(this.Tribe, this.Job);
        animator = this.GetComponent<Animator>();
        navAgent = this.GetComponent<NavAgent>();
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void Idle()
    {
        Debug.Log(this.Type+ this.ID.ToString() + "'State is Idle" );
    }

    public void Walk()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Walk");
    }

    public void Run()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Run");
    }


    public override bool HanleMessage(Telegram _msg)
    {
        return stateMachine.HandleMessgae(_msg);
    }

    public bool ToWalk()
    {
        if(this.clock + 5.0f <  Clock.Instance.GetTime())
           return true;

        return false;
    }

    public bool ToIdle()
    {
        if (this.clock + 5.0f < Clock.Instance.GetTime() ||
            1.0f> Vector3.Distance(this.transform.position , this.navAgent.target))
            return true;

        return false;
    }

    public StateMachine<Ork> GetStateMachine() { return stateMachine; }
    public Animator GetAnimator() { return animator; }
    public NavAgent GetNavAgent() { return navAgent; }
}
