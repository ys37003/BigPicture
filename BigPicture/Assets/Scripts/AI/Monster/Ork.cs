using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ork : Monster {

    StateMachine<Ork> stateMachine;

    public bool isToPatrol = false;
    public bool isToIdle = false;
    MonsterData data;
    Animator animator;


    void Start()
    {
        EntityInit( eType.MONSTER , eTRIBE_TYPE.Ork ,eJOB_TYPE.TANKER );
        stateMachine = new StateMachine<Ork>(this);
        data = DataManager.Instance().GetData(this.Tribe, this.Job);
        animator = this.GetComponent<Animator>();
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

    public StateMachine<Ork> GetStateMachine()
    {
        return stateMachine;
    }

    public override bool HanleMessage(Telegram _msg)
    {
        return stateMachine.HandleMessgae(_msg);
    }

    public bool ToPatrol()
    {
        if (true == isToPatrol)
            return true;

        return false;
    }

    public bool ToIdle()
    {
        if (true == isToIdle)
            return true;

        return false;
    }
}
