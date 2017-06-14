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
        AI entity = (AI)owner;
        
        currentState.Excute(owner);

        if (true == entity.DieCheck())
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_DIE, null);
            return;
        }

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
            case eSTATE.IDLE:        currentState  = Idle.Instance();         break;
            case eSTATE.WALK:        currentState  = Walk.Instance();         break;
            case eSTATE.RUN:         currentState  = Run.Instance();          break;
            case eSTATE.ATTACK:      currentState  = Attack.Instance();       break;
            case eSTATE.BATTLEIDLE:  currentState  = BattleIdle.Instance();   break;
            case eSTATE.BATTLEWALK:  currentState  = BattleWalk.Instance();   break;
            case eSTATE.SETFOMATION: currentState  = SetFomation.Instance();  break;
            case eSTATE.HIT        : currentState  = Hit.Instance();          break;
            case eSTATE.DIE        : currentState  = Die.Instance();          break;
            case eSTATE.COME_ON    : currentState  = ComeOn.Instance();       break;
            case eSTATE.ESCAPE     : currentState  = Escape.Instance();       break;
            case eSTATE.HEAL       : currentState  = Heal.Instance();         break;
            case eSTATE.DRAGONBREATH: currentState = DragonBreath.Instance(); break;
            case eSTATE.FOOTSTAMP: currentState    = FootStamp.Instance();    break;
            case eSTATE.SHOCK: currentState        = Shock.Instance();        break;
            case eSTATE.SUMONMONSTER: currentState = SumonMonster.Instance(); break;
        }                                                                    
    }

    public bool HandleMessgae(Telegram _msg)
    {
        // 각 상태별 메세지 처리
        if (null != currentState && currentState.OnMessage(owner, _msg))
        {
            return true;
        }

        // 전역 메세지 처리

        AI entity = (AI)owner;
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.ATTACKABLE:
                entity.AttackAble = true;
                return true;
            case (int)eMESSAGE_TYPE.TO_HIT:
                this.ChangeState(eSTATE.HIT);
                return true;
            case (int)eMESSAGE_TYPE.TO_SHOCK:
                this.ChangeState(eSTATE.SHOCK);
                return true;
            case (int)eMESSAGE_TYPE.I_SEE_YOU:
                entity.EntityGroup.EnemyGroup = (Group)_msg.extraInfo;
                return true;
            case (int)eMESSAGE_TYPE.TO_DIE:
                entity.StateMachine.ChangeState(eSTATE.DIE);
                return true;
        }

        return false;
    }
}