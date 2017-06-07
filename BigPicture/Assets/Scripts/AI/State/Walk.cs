using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State 
{
    private static Walk instance;
    AI entity;
    private Walk()
    {

    }

    public static Walk Instance()
    {
        if (instance == null)
        {
            instance = new Walk();
        }

        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (AI)_entity;
        entity.Walk();
       
        if (true == entity.IsArrive())
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_IDLE, null);
        }
    }

    public void Enter(object _entity)
    {
        entity = (AI)_entity;
        entity.DestinationCheck = Time.time;
        entity.HUDUI.SetEmotion(eEmotion.Walk);
        entity.SetTarget(entity.SetDestination(entity, entity.EntityGroup));
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Walk", true);
    }

    public void Exit(object _entity)
    {
        entity = (AI)_entity;
        entity.NavAgent.Clear();
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Walk", false);
    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(object _entity, Telegram _msg)
    {
        entity = (AI)_entity;
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_IDLE:
                entity.StateMachine.ChangeState(eSTATE.IDLE);
                return true;

            case (int)eMESSAGE_TYPE.FLLOW_ME:
                entity.SetTarget(MathAssist.Instance().RandomVector3((Vector3)_msg.extraInfo, 5.0f));
                return true;

            case (int)eMESSAGE_TYPE.SET_FOMATION:
                entity.StateMachine.ChangeState(eSTATE.SETFOMATION);
                return true;

            case (int)eMESSAGE_TYPE.FIND_ENEMY:
                entity.EntityGroup.EnemyGroup = (Group)_msg.extraInfo;
                if (null != entity.EntityGroup.EnemyGroup)
                {
                    entity.SetEnemy(entity.EntityGroup.EnemyGroup);
                    CoroutineManager.Instance.CStartCoroutine(entity.EnemyHandle.SortEnemy());
                    entity.StateMachine.ChangeState(eSTATE.SETFOMATION);
                }
                entity.StartBattle();
                return true;
        }
        return false;
    }
}