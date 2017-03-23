using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ork : Monster {

    StateMachine<Ork> stateMachine;

    public bool isToPatrol = false;
    public bool isToIdle = false;
    MonsterData data;
    void Start()
    {
        EntityInit(eType.Ork, 0);
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
