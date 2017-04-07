using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling<entity_type> : State<entity_type> where entity_type : Ork
{ 
    private static Rolling<entity_type> instance;

    private Rolling()
    {

    }

    public static Rolling<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Rolling<entity_type>();
        }

        return instance;
    }

    public void Excute(entity_type _monster)
    {
        _monster.Rolling();

        if (true == _monster.RollingAble)
        {
            _monster.RollingAble = false;
        }
        else
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);
    }

    public void Enter(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Rolling", true);
        //_monster.RollingAble = false;
    }

    public void Exit(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Rolling", false);
        MessageDispatcher.Instance.DispatchMessage(3, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.ROLLINGABLE, null);
    }

    /// <summary>
    /// Idle상태에서 받은 메세지 처리
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
        }

        return false;
    }
}
