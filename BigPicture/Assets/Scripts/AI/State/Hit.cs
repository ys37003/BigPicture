using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit<entity_type> : State<entity_type> where entity_type : HoodSkull
{

    private static Hit<entity_type> instance;

    private Hit()
    {

    }

    public static Hit<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Hit<entity_type>();
        }

        return instance;
    }

    public void Excute(entity_type _monster)
    {
        if (true == _monster.EndHit())
        {
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);
        }
    }

    public void Enter(entity_type _monster)
    {
        if (true == _monster.Die())
        {
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_DIE, null);
            return;
        }
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Hit", true);

    }

    public void Exit(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Hit", false);
    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_monster"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(entity_type _monster, Telegram _msg)
    {
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_BATTLEIDLE:
                _monster.GetStateMachine().ChangeState(eSTATE.BATTLEIDLE);
                return true;
            case (int)eMESSAGE_TYPE.TO_DIE:
                _monster.GetStateMachine().ChangeState(eSTATE.DIE);
                return true;
        }
        return false;
    }
}
