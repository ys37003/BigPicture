using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<entity_type> where entity_type : Ork
{
    State<entity_type> currentState;
    State<entity_type> previousState;
    eSTATE ePreviousState;
    entity_type owner;

    public StateMachine(entity_type _owner)
    {
        owner = _owner;
        currentState = null;
        previousState = null;
        currentState = Idle<entity_type>.Instance();
        ePreviousState = eSTATE.IDLE;
    }

    // Update is called once per frame
    public void Update()
    {
        currentState.Excute(owner);
    }

    public void ChangeState( eSTATE _stateType )
    {
        if (ePreviousState != _stateType)
        {
            currentState.Exit(owner);
            previousState = currentState;
            EnumToState(_stateType);
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
        }
    }

    public bool HandleMessgae(Telegram _msg)
    {
        // 각 상태별 메세지 처리
        if (null != currentState && currentState.OnMessage(owner , _msg))
        {
            return true;
        }

        // 전역 메세지 처리

        switch(_msg.message)
        {
            case(int)eChangeState.FIND_ENEMY :
                ChangeState(eSTATE.RUN);
                owner.SetTarget((Vector3)_msg.extraInfo);
                return true;
        }

        return false;
    }
}
