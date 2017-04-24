using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk<entity_type> : State<entity_type> where entity_type : AI
{
    private static Walk<entity_type> instance;

    private Walk()
    {

    }

    public static Walk<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Walk<entity_type>();
        }

        return instance;
    }

    public void Excute(entity_type _entity)
    {
        _entity.Walk();
       
        if (true == _entity.IsArrive())
        {
            MessageDispatcher.Instance.DispatchMessage(0, _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_IDLE, null);
        }
    }

    public void Enter(entity_type _entity)
    {
        BaseGameEntity foword = _entity.Group.JobToEntity(eJOB_TYPE.FORWARD);
        _entity.SetTarget(_entity.SetDestination(_entity, _entity.Group));
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "Walk", true);
    }

    public void Exit(entity_type _entity)
    {
        _entity.NavAgent.Clear();
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "Walk", false);
    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(entity_type _entity, Telegram _msg)
    {
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_IDLE:
                _entity.StateMachine.ChangeState(eSTATE.IDLE);
                return true;

            case (int)eMESSAGE_TYPE.FIND_ENEMY:
                GameObject enemy = (GameObject)_msg.extraInfo;
                _entity.SetEnemy(enemy);
                _entity.StateMachine.ChangeState(eSTATE.SETFOMATION);
                return true;

            case (int)eMESSAGE_TYPE.FLLOW_ME:
                _entity.SetTarget(MathAssist.Instance().RandomVector3((Vector3)_msg.extraInfo, 5.0f));
                return true;

            case (int)eMESSAGE_TYPE.SET_FOMATION:
                _entity.StateMachine.ChangeState(eSTATE.SETFOMATION);
                return true;
        }
        return false;
    }
}