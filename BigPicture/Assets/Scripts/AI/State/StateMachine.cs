using System.Collections.Generic;
using UnityEngine;


public class StateMachine
{
    private State currentState;
    private eSTATE              ePreviousState = eSTATE.NULL;
    private eSTATE              eCurrentState  = eSTATE.NULL;
    private object              owner;

    public eSTATE CurrentState { get { return eCurrentState; } }

    public StateMachine(object _owner)
    {
        owner = _owner;
        currentState = Idle.Instance();
        ChangeState(eSTATE.IDLE);
    }

    public eSTATE GetCurrentState()
    {
        return eCurrentState;
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
            case eSTATE.IDLE:        currentState = Idle.Instance();         break;
            case eSTATE.WALK:        currentState = Walk.Instance();         break;
            case eSTATE.RUN:         currentState = Run.Instance();          break;
            case eSTATE.ATTACK:      currentState = Attack.Instance();       break;
            case eSTATE.BATTLEIDLE:  currentState = BattleIdle.Instance();   break;
            case eSTATE.BATTLEWALK:  currentState = BattleWalk.Instance();   break;
            case eSTATE.SETFOMATION: currentState = SetFomation.Instance();  break;
            case eSTATE.HIT        : currentState = Hit.Instance();          break;
            case eSTATE.DIE        : currentState = Die.Instance();          break;
            case eSTATE.COME_ON    : currentState = ComeOn.Instance();       break;
            case eSTATE.ESCAPE     : currentState = Escape.Instance();       break;
            case eSTATE.HEAL       : currentState = Heal.Instance();         break; 
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

        AI dummy = (AI)owner;
        switch (_msg.message)
        { 
            case (int)eMESSAGE_TYPE.ATTACKABLE:
                dummy.AttackAble = true;
                return true;
            case (int)eMESSAGE_TYPE.TO_HIT:
                AI AI_Owner = (AI)owner;
                if( eDAMAGE_TYPE.PHYSICS == (eDAMAGE_TYPE)_msg.extraInfo )
                {
                    Vector3 direction =  AI_Owner.transform.position - ( AI_Owner.transform.forward / 2.0f);
                    AI_Owner.SetTarget(direction);
                }
                //MathAssist.Instance().AddForce_Back(AIowner.GetComponent<Rigidbody>(), 10.0f);
                this.ChangeState(eSTATE.HIT);
                return true;
            case (int)eMESSAGE_TYPE.I_SEE_YOU:
                dummy.Group.EnemyGroup = (Group)_msg.extraInfo;
                return true;
        }
        return false;
    }
}