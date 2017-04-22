using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFomation<entity_type> : State<entity_type> where entity_type : HoodSkull
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

    public void Excute(entity_type _monster)
    {
        if(true == _monster.NavAgent.IsArrive())
        {
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_BATTLEWALK, null);
        }
    }

    public void Enter(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "BattleWalk", true);
        _monster.SetTarget (_monster.SetFomation( _monster, _monster.GetGroup().GetCenter(_monster.GetEnemyPosition()) ) );
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
            case (int)eMESSAGE_TYPE.TO_BATTLEWALK:
                _monster.GetStateMachine().ChangeState(eSTATE.BATTLEWALK);
                return true;
        }

        return false;
    }
}
