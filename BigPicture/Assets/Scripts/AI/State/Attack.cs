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

    public void Excute(entity_type _monster)
    {
        _monster.Attack();
        if (true == _monster.EndAttack())
        {
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);            
        }
    }

    public void Enter(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Attack", true);
        _monster.AttackAble = false;
    }

    public void Exit(entity_type _monster)
    {
        MessageDispatcher.Instance.DispatchMessage(3, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.ATTACKABLE, null);
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Attack", false);
    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_monster"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    /// 
    public bool OnMessage(entity_type _monster, Telegram _msg)
    {
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_BATTLEIDLE:
                _monster.StateMachine.ChangeState(eSTATE.BATTLEIDLE);
                return true;
        }

        return false;
    }
}