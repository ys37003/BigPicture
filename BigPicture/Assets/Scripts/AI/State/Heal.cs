using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : State
{
    private static Heal instance;
    AI entity;
    private Heal()
    {

    }

    public static Heal Instance()
    {
        if (instance == null)
        {
            instance = new Heal();
        }
        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (AI)_entity;
        if (true == entity.EndAttack())
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);
        }
    }

    public void Enter(object _entity)
    {
        entity = (AI)_entity;
        Debug.Log("Im Heal");
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Attack", true);
    }

    public void Exit(object _entity)
    {
        entity = (AI)_entity;
        entity.GetComponent<Healer>().Heal(30);
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Attack", false);
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
