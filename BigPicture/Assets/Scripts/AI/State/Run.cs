﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run<entity_type> : State<entity_type> where entity_type : Ork
{
    private static Run<entity_type> instance;
    private Run()
    { }

    public static Run<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Run<entity_type>();
        }
        return instance;
    }

    public override void Excute(entity_type _monster)
    {
        _monster.Run();

        if (true == _monster.IsArrive())
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_IDLE, null);

        if (true == _monster.AttackAble())
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_ATTACK, null);
    }

    public override void Enter(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Run", true);
    }

    public override void Exit(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Run", false);
        _monster.NavAgent.Clear();
    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_monster"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    /// 
    public override bool OnMessage(entity_type _monster, Telegram _msg)
    {
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_IDLE:
                _monster.GetStateMachine().ChangeState(eSTATE.IDLE);
                return true;

            case (int)eMESSAGE_TYPE.TO_ATTACK:
                _monster.GetStateMachine().ChangeState(eSTATE.ATTACK);
                return true;
        }

        return false;
    }

}
