using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die<entity_type> : State<entity_type> where entity_type : AI
{
    private static Die<entity_type> instance;

    private Die()
    {

    }

    public static Die<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Die<entity_type>();
        }

        return instance;
    }

    public void Excute(entity_type _entity)
    {
        _entity.Die();
        if (true == _entity.EndDie())
        {
            AnimatorManager.Instance().SetAnimation(_entity.Animator, "Die", false);
        }
    }

    public void Enter(entity_type _entity)
    {
        Debug.Log("Enter Die");
        MessageDispatcher.Instance.DispatchMessage(3, _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.REMOVE_AND_DROP, null);
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "Die", true);
    }

    public void Exit(entity_type _entity)
    {
    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    /// 
    public bool OnMessage(entity_type _entity, Telegram _msg)
    {
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.REMOVE_AND_DROP:
                _entity.Clear();
                return true;
        }

        return false;
    }
}