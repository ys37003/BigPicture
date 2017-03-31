using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<entity_type> where entity_type : Ork
{
    State<entity_type> currentState;
    eSTATE ePreviousState = eSTATE.NULL;
    eSTATE eCurrentState = eSTATE.NULL;
    entity_type owner;

    public eSTATE CurrentState
    {
        get { return eCurrentState; }
    }

    public StateMachine(entity_type _owner)
    {
        owner = _owner;
        currentState = Idle<entity_type>.Instance();
        ChangeState(eSTATE.IDLE);
    }

    // Update is called once per frame
    public void Update()
    {
        currentState.Excute(owner);
    }

    public void ChangeState( eSTATE _stateType )
    {
        if (eCurrentState != _stateType)
        {
            currentState.Exit(owner);
            ePreviousState = eCurrentState;
            EnumToState(_stateType);
            eCurrentState = _stateType;
            currentState.Enter(owner);
        }
        else
        {
            Debug.Log("newState == currentState");
        }
    }

    private void EnumToState(eSTATE _stateType)
    {
        switch (_stateType)
        {
            case eSTATE.IDLE:
                currentState = Idle<entity_type>.Instance();
                break;

            case eSTATE.WALK:
                currentState = Walk<entity_type>.Instance();
                break;
            case eSTATE.DEAD:


                break;
            case eSTATE.AVOID:
                
                break;
            case eSTATE.RUN:
                currentState = Run<entity_type>.Instance();
                break;
            case eSTATE.ATTACK:
                currentState = Attack<entity_type>.Instance();
                break;
            case eSTATE.BATTLEIDLE:
                currentState = BattleIdle<entity_type>.Instance();
                break;
        }
    }

    public bool HandleMessgae(Telegram _msg)
    {
        // 각 상태별 메세지 처리
        if (null != currentState && currentState.OnMessage(owner , _msg ))
        {
            return true;
        }

        // 전역 메세지 처리

        return false;
    }
}
