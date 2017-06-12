using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : State {

    private static Shock instance;
    AI entity;
    private Shock()
    {

    }

    public static Shock Instance()
    {
        if (instance == null)
        {
            instance = new Shock();
        }
        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (AI)_entity;
    }

    public void Enter(object _entity)
    {
        entity = (AI)_entity;
        entity.HUDUI.AddBuff(eBuff.Shock);
        MessageDispatcher.Instance.DispatchMessage(3.0f, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Hit", true);
    }

    public void Exit(object _entity)
    {
        entity = (AI)_entity;
        entity.HUDUI.RemoveBuff(eBuff.Shock);
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
        }
        return false;
    }
}
