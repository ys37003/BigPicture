using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : State {

    private static Escape instance;
    AI entity;
    private Escape()
    {

    }

    public static Escape Instance()
    {
        if (instance == null)
        {
            instance = new Escape();
        }

        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (AI)_entity;
        entity.Escape();
        if (true == entity.AttackAble)
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_ATTACK, null);
            return;
        }

        if (true == entity.NavAgent.IsArrive())
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);
            return;
        }
    }

    public void Enter(object _entity)
    {
        entity = (AI)_entity;
        entity.AddSpeed(-2);
        AnimatorManager.Instance().SetAnimation(entity.Animator, "BattleWalk", true);
    }

    public void Exit(object _entity)
    {
        entity = (AI)_entity;
        entity.AddSpeed(2);
        entity.NavAgent.Clear();
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
            case (int)eMESSAGE_TYPE.TO_ATTACK:
                entity.StateMachine.ChangeState(eSTATE.ATTACK);
                return true;
            case (int)eMESSAGE_TYPE.TO_BATTLEIDLE:
                entity.StateMachine.ChangeState(eSTATE.BATTLEIDLE);
                return true;
        }

        return false;
    }
}
