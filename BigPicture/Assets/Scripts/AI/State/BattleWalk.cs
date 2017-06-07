using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWalk : State
{

    private static BattleWalk instance;
    AI entity;
    private BattleWalk()
    {

    }

    public static BattleWalk Instance()
    {
        if (instance == null)
        {
            instance = new BattleWalk();
        }

        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (AI)_entity;
        entity.BattleWalk();
        //entity.SetEnemy(entity.Group.NearestEnemy(entity.transform.position));
        //entity.SetTarget(entity.EnemyList[0].transform.position);
        if (true == entity.Approach(entity.TargetDistance()))
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_RUN, null);
        }
    }

    public void Enter(object _entity)
    {
        entity = (AI)_entity;
        entity.HUDUI.SetEmotion(eEmotion.Walk);
        //entity.SetTarget(entity.EnemyList[0].transform.position);
        AnimatorManager.Instance().SetAnimation(entity.Animator, "BattleWalk", true);
    }

    public void Exit(object _entity)
    {
        entity = (AI)_entity;
        AnimatorManager.Instance().SetAnimation(entity.Animator, "BattleWalk", false);
    }

    /// <summary>
    /// Idle상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(object _entity, Telegram _msg)
    {
        entity = (AI)_entity;
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_RUN:
                entity.StateMachine.ChangeState(eSTATE.RUN);
                return true;
        }

        return false;
    }
}
