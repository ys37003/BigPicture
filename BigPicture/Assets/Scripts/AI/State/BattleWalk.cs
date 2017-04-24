using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWalk<entity_type> : State<entity_type> where entity_type : AI
{

    private static BattleWalk<entity_type> instance;

    private BattleWalk()
    {

    }

    public static BattleWalk<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new BattleWalk<entity_type>();
        }

        return instance;
    }

    public void Excute(entity_type _entity)
    {
        _entity.BattleWalk();

        if(true == _entity.Approach(_entity.TargetDistance()))
        {
            MessageDispatcher.Instance.DispatchMessage(0, _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_RUN, null);
        }
    }

    public void Enter(entity_type _entity)
    {
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "BattleWalk", true);
        _entity.SetTarget(_entity.GetEnemyPosition());
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
            case (int)eMESSAGE_TYPE.TO_RUN:
                _entity.StateMachine.ChangeState(eSTATE.RUN);
                return true;
        }

        return false;
    }
}
