using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ork : Monster {

    StateMachine<Ork> stateMachine;

    void Start()
    {
        EntityInit(eENTITY_TYPE.Ork, 0);
        stateMachine = new StateMachine<Ork>(this);
    }

    private void Update()
    {
        stateMachine.Update();
    }
    public void Idle()
    {
        Debug.Log(this.Type+ this.ID.ToString() + "'State is Idle" );
    }

    public void Patrol()
    {
        Debug.Log(this.Type + this.ID.ToString() + "'State is Patrol");
    }

    public StateMachine<Ork> GetStateMachine()
    {
        return stateMachine;
    }

    public override bool HanleMessage(Telegram _msg)
    {
        return stateMachine.HandleMessgae(_msg);
    }
}
