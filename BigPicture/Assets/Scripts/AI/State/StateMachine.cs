using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<entity_type> where entity_type : Ork
{
    State<entity_type> currentState;
    State<entity_type> previousState;
    State<entity_type> globalState;
    entity_type owner;

    public StateMachine(entity_type _owner)
    {
        owner = _owner;
        currentState = null;
        previousState = null;
        globalState = null;
        currentState = Idle<entity_type>.Instance();
    }

    // Update is called once per frame
    public void Update()
    {
        currentState.Excute(owner);
    }

    public void ChangeState( eSTATE _stateType )
    {
        currentState.Exit(owner);
        previousState = currentState;
        EnumToState(_stateType);
        currentState.Enter(owner);
    }

    private void EnumToState(eSTATE _stateType)
    {
        switch (_stateType)
        {
            case eSTATE.IDLE:
                currentState = Idle<entity_type>.Instance();
                break;
            case eSTATE.ATTACK:
                
                break;
            case eSTATE.PATROL:
                currentState = Patrol<entity_type>.Instance();
                break;
            case eSTATE.DIE:
                
                break;
            case eSTATE.ESCAPE:
                
                break;
            case eSTATE.TRACE:
               
                break;
        }
    }

    public bool HandleMessgae(Telegram _msg)
    {
        if (null != currentState && currentState.OnMessage(owner , _msg))
        {
            return true;
        }

        if(null != globalState && globalState.OnMessage(owner , _msg))
        {
            return true;
        }

        return false;
    }
}
