using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    private static Attack instance;
    AI entity;
    private Attack()
    {

    }

    public static Attack Instance()
    {
        if (instance == null)
        {
            instance = new Attack();
        }

        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (AI)_entity;
        entity.Attack();
        if (true == entity.EndAttack())
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);            
        }
    }

    public void Enter(object _entity)
    {
        entity = (AI)_entity;

        if (null != entity.AttackHandler)
        {
            entity.AttackHandler.Attack(entity.GetEnemyPosition());
            CorutineManager.Instance.StartCorutine(entity.AttackHandler.AttackDelay());
        }

        AnimatorManager.Instance().SetAnimation(entity.Animator, "Attack", true);
        entity.AttackAble = false;
    }

    public void Exit(object _entity)
    {
        entity = (AI)_entity;
        MessageDispatcher.Instance.DispatchMessage(3, entity.ID, entity.ID, (int)eMESSAGE_TYPE.ATTACKABLE, null);
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Attack", false);
    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    /// 
    public bool OnMessage(object _entity, Telegram _msg)
    {
        entity = (AI)_entity;
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_BATTLEIDLE:
                entity.StateMachine.ChangeState(eSTATE.BATTLEIDLE);
                return true;
        }

        return false;
    }
}