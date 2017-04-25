using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleIdle<entity_type> : State<entity_type> where entity_type : AI
{
    private static BattleIdle<entity_type> instance;

    private BattleIdle()
    {

    }

    public static BattleIdle<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new BattleIdle<entity_type>();
        }

        return instance;
    }

    public void Excute(entity_type _entity)
    {
        _entity.BattleIdle();

        if(false == _entity.EnemyCheck())
        {
            MessageDispatcher.Instance.DispatchMessage(0, _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_IDLE, null);
            return;
        }

        if(_entity.AttackRange < _entity.TargetDistance() )
        {
            MessageDispatcher.Instance.DispatchMessage(0, _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_RUN, null);
            return;
        }
        if (true == _entity.AttackAble)
        {
            MessageDispatcher.Instance.DispatchMessage(0, _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_ATTACK, null);
            return;
        }

        //if (false == _moanster.ToBattleIdle())
        //{
        //    MessageDispatcher.Instance.DispatchMessage(0, _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_IDLE, null);
        //    _entity.NavAgent.Clear();
        //    _entity.EnemyClear();
        //    return;
        //}
    }

    public void Enter(entity_type _entity)
    {
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "BattleIdle", true);
    }

    public void Exit(entity_type _entity)
    {
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "BattleIdle", false);
    }

    /// <summary>
    /// Idle상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(entity_type _entity, Telegram _msg)
    {
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_ATTACK:
                _entity.StateMachine.ChangeState(eSTATE.ATTACK);
                return true;
            case (int)eMESSAGE_TYPE.TO_IDLE:
                _entity.StateMachine.ChangeState(eSTATE.IDLE);
                return true;

            case (int)eMESSAGE_TYPE.SET_FOMATION:
                Vector3 fomation = (Vector3)_msg.extraInfo;
                _entity.SetTarget(fomation);
                return true;

            case (int)eMESSAGE_TYPE.TO_RUN:
                _entity.StateMachine.ChangeState(eSTATE.RUN);
                return true;
        }

        return false;
    }
}