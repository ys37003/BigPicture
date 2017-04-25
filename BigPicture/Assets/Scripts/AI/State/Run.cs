using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run<entity_type> : State<entity_type> where entity_type : AI
{
    private static Run<entity_type> instance;

    private Run()
    {

    }

    public static Run<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Run<entity_type>();
        }

        return instance;
    }

    public void Excute(entity_type _entity)
    {
        _entity.Run();

        if(true == _entity.IsArrive())
        {
            MessageDispatcher.Instance.DispatchMessage(0, _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);
            return;
        }

        if(_entity.AttackRange > _entity.TargetDistance())
        {
            MessageDispatcher.Instance.DispatchMessage(0, _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_BATTLEIDLE, null);
            return;
        }

    }

    public void Enter(entity_type _entity)
    {
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "Run", true);
        _entity.SetTarget(_entity.GetEnemyPosition());
        _entity.NavAgent.SetSpeed(3.5f);
    }

    public void Exit(entity_type _entity)
    {
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "Run", false);
        _entity.NavAgent.Clear();
        _entity.NavAgent.SetSpeed(2.0f);

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
            case (int)eMESSAGE_TYPE.TO_IDLE:
                _entity.StateMachine.ChangeState(eSTATE.IDLE);
                return true;

            case (int)eMESSAGE_TYPE.TO_BATTLEIDLE:
                _entity.StateMachine.ChangeState(eSTATE.BATTLEIDLE);
                return true;
        }

        return false;
    }
}