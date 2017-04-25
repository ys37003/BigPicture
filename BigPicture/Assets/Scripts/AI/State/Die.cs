using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : State 
{
    private static Die instance;
    AI entity;
    private Die()
    {

    }

    public static Die Instance()
    {
        if (instance == null)
        {
            instance = new Die();
        }

        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (AI)_entity;
        entity.Die();
        if (true == entity.EndDie())
        {
            AnimatorManager.Instance().SetAnimation(entity.Animator, "Die", false);
        }
    }

    public void Enter(object _entity)
    {
        entity = (AI)_entity;
        Debug.Log("Enter Die");
        MessageDispatcher.Instance.DispatchMessage(3, entity.ID, entity.ID, (int)eMESSAGE_TYPE.REMOVE_AND_DROP, null);
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Die", true);
    }

    public void Exit(object _entity)
    {
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
            case (int)eMESSAGE_TYPE.REMOVE_AND_DROP:
                entity.Clear();
                return true;
        }

        return false;
    }
}