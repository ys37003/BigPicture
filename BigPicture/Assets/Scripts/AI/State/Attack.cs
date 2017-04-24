using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack<entity_type> : State<entity_type> where entity_type : AI
{
    private static Attack<entity_type> instance;

    private Attack()
    {

    }

    public static Attack<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Attack<entity_type>();
        }

        return instance;
    }

    public void Excute(entity_type _entity)
    {
        _entity.Attack();
        if (true == _entity.EndAttack())
        {
            MessageDispatcher.Instance.DispatchMessage(0, _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);            
        }
    }

    public void Enter(entity_type _entity)
    {
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "Attack", true);
        _entity.AttackAble = false;
    }

    public void Exit(entity_type _entity)
    {
        MessageDispatcher.Instance.DispatchMessage(3, _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.ATTACKABLE, null);
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "Attack", false);
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
            case (int)eMESSAGE_TYPE.TO_BATTLEIDLE:
                _entity.StateMachine.ChangeState(eSTATE.BATTLEIDLE);
                return true;
        }

        return false;
    }
}