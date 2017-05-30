using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : State
{
    private static Hit instance;
    AI entity;
    private Hit()
    {

    }

    public static Hit Instance()
    {
        if (instance == null)
        {
            instance = new Hit();
        }

        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (AI)_entity;
        entity.Hit();
        if (true == entity.EndHit())
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);
        }
    }

    public void Enter(object _entity)
    {
        entity = (AI)_entity;
        //Vector3 hitPos = entity.transform.position - (entity.transform.forward/2);
        //entity.AddSpeed(-2);
        //entity.SetTarget(hitPos);
        if (true == entity.DieCheck())
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_DIE, null);
            return;
        }

        AnimatorManager.Instance().SetAnimation(entity.Animator, "Hit", true);

    }

    public void Exit(object _entity)
    {
        entity = (AI)_entity;
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Hit", false);
    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(object _entity, Telegram _msg)
    {
        entity = (AI)_entity;
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_BATTLEIDLE:
                entity.StateMachine.ChangeState(eSTATE.BATTLEIDLE);
                return true;
            case (int)eMESSAGE_TYPE.TO_DIE:
                entity.StateMachine.ChangeState(eSTATE.DIE);
                return true;
        }
        return false;
    }
}
