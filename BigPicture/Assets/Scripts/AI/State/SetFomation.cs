using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFomation : State
{
    private static SetFomation instance;
    AI entity;
    private SetFomation()
    {

    }

    public static SetFomation Instance()
    {
        if (instance == null)
        {
            instance = new SetFomation();
        }

        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (AI)_entity;
        if (true == entity.NavAgent.IsArrive())
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_BATTLEWALK, null);
        }
    }

    public void Enter(object _entity)
    {
        entity = (AI)_entity;
        AnimatorManager.Instance().SetAnimation(entity.Animator, "BattleWalk", true);
        entity.SetTarget(entity.SetFomation(entity, entity.Group.GetCenter(entity.GetEnemyPosition()) ) );
    }

    public void Exit(object _entity)
    {
        entity = (AI)_entity;
        AnimatorManager.Instance().SetAnimation(entity.Animator, "BattleWalk", false);
    }

    /// <summary>
    /// Idle상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(object _entity, Telegram _msg)
    {
        entity = (AI)_entity;
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_BATTLEWALK:
                entity.SetTarget(entity.EnemyList[0].enemy.transform.position);
                entity.StateMachine.ChangeState(eSTATE.BATTLEWALK);
                return true;
        }

        return false;
    }
}
