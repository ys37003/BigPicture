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

    public void Excute(entity_type _monster)
    {
        _monster.BattleWalk();

        if(true == _monster.Approach(_monster.TargetDistance()))
        {
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_RUN, null);
        }
    }

    public void Enter(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "BattleWalk", true);
        _monster.SetTarget(_monster.GetEnemyPosition());
    }

    public void Exit(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "BattleWalk", false);
    }

    /// <summary>
    /// Idle상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_monster"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(entity_type _monster, Telegram _msg)
    {
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_RUN:
                _monster.StateMachine.ChangeState(eSTATE.RUN);
                return true;
        }

        return false;
    }
}
