using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStamp : State
{
    private static FootStamp instance;
    Dragon entity;

    private FootStamp()
    {

    }

    public static FootStamp Instance()
    {
        if (instance == null)
        {
            instance = new FootStamp();
        }
        return instance;
    }


    public void Excute(object _entity)
    {
        entity = (Dragon)_entity;

        if (true == entity.EndFootStamp())
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);
        }
    }

    public void Enter(object _entity)
    {
        entity = (Dragon)_entity;
        entity.AttackAble = false;
        AnimatorManager.Instance().SetAnimation(entity.Animator, "FootStamp", true);
    }

    public void Exit(object _entity)
    {
        entity = (Dragon)_entity;
        MessageDispatcher.Instance.DispatchMessage(Random.Range(3, 5), entity.ID, entity.ID, (int)eMESSAGE_TYPE.ATTACKABLE, null);
        AnimatorManager.Instance().SetAnimation(entity.Animator, "FootStamp", false);
    }

    /// <summary>
    /// Idle상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(object _entity, Telegram _msg)
    {
        entity = (Dragon)_entity;

        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_BATTLEIDLE:
                entity.StateMachine.ChangeState(eSTATE.BATTLEIDLE);
                return true;
        }
        return false;
    }
}
