using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFomation<entity_type> : State<entity_type> where entity_type : AI
{
    private static SetFomation<entity_type> instance;

    private SetFomation()
    {

    }

    public static SetFomation<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new SetFomation<entity_type>();
        }

        return instance;
    }

    public void Excute(entity_type _entity)
    {
        if(true == _entity.NavAgent.IsArrive())
        {
            MessageDispatcher.Instance.DispatchMessage(0, _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_BATTLEWALK, null);
        }
    }

    public void Enter(entity_type _entity)
    {
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "BattleWalk", true);
        _entity.SetTarget (_entity.SetFomation( _entity, _entity.Group.GetCenter(_entity.GetEnemyPosition()) ) );
    }

    public void Exit(entity_type _entity)
    {
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "BattleWalk", false);
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
            case (int)eMESSAGE_TYPE.TO_BATTLEWALK:
                _entity.StateMachine.ChangeState(eSTATE.BATTLEWALK);
                return true;
        }

        return false;
    }
}
