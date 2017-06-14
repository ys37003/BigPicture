using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeOn : State {

    private static ComeOn instance;
    Partner entity;

    private ComeOn()
    {

    }

    public static ComeOn Instance()
    {
        if (instance == null)
        {
            instance = new ComeOn();
        }

        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (Partner)_entity;
        entity.Walk();
        if (true == entity.IsArrive())
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_IDLE, null);
        }
    }

    public void Enter(object _entity)
    {
        entity = (Partner)_entity;
        entity.SetTarget(entity.SetDestination(entity, entity.EntityGroup));
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Walk", true);
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Command", true);
    }

    public void Exit(object _entity)
    {
        entity = (Partner)_entity;
        entity.NavAgent.Clear();
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Walk", false);
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Command", false);
    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(object _entity, Telegram _msg)
    {
        entity = (Partner)_entity;
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_IDLE:
                entity.StateMachine.ChangeState(eSTATE.IDLE);
                return true;
        }
        return false;
    }
}
