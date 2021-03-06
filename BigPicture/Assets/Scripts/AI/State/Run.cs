﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : State
{
    private static Run instance;
    AI entity;
    private Run()
    {

    }

    public static Run Instance()
    {
        if (instance == null)
        {
            instance = new Run();
        }

        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (AI)_entity;
        entity.Run();
        if (entity.AttackRange > entity.TargetDistance() || true == entity.IsArrive() )
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);
            return;
        }
       
        entity.SetTarget(entity.GetEnemyPosition());
    }

    public void Enter(object _entity)
    {
        entity = (AI)_entity;
        entity.DestinationCheck = Time.time;
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Run", true);
        entity.SetTarget(entity.GetEnemyPosition());
        entity.AddSpeed(4.0f);
    }

    public void Exit(object _entity)
    {
        entity = (AI)_entity;
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Run", false);
        entity.NavAgent.Clear();
        entity.AddSpeed(-4.0f);
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
            case (int)eMESSAGE_TYPE.TO_IDLE:
                entity.StateMachine.ChangeState(eSTATE.IDLE);
                return true;

            case (int)eMESSAGE_TYPE.TO_BATTLEIDLE:
                entity.StateMachine.ChangeState(eSTATE.BATTLEIDLE);
                return true;
        }

        return false;
    }
}