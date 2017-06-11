using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleIdle : State
{
    private static BattleIdle instance;
    AI entity;
    private BattleIdle()
    {

    }

    public static BattleIdle Instance()
    {
        if (instance == null)
        {
            instance = new BattleIdle();
        }

        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (AI)_entity;

        entity.BattleIdle();

        if (null == entity.EntityGroup.EnemyGroup || false == entity.EntityGroup.EnemyGroup.BattleAble())
        {
            entity.EntityGroup.EnemyGroup = null;
            entity.EnemyHandle.Clear();
            entity.EndBattle();
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_IDLE, null);
            return;
        }

        if (entity.AttackRange < entity.TargetDistance())
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_RUN, null);
            return;
        }


        if (true == entity.AttackAble)
        {
            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_ATTACK, null);
            return;
        }

    }

    public void Enter(object _entity)
    {
        entity = (AI)_entity;
        entity.HUDUI.SetEmotion(eEmotion.Angry);
        if (entity.AttackRange - 1 >
                 Vector3.Distance(entity.transform.position,
                 entity.GetEnemyPosition()))
        {
            if (0 == Random.Range(0, 5))
                MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_ESCAPE, null);

            return;
        }
        AnimatorManager.Instance().SetAnimation(entity.Animator, "BattleIdle", true);
    }

    public void Exit(object _entity)
    {
        entity = (AI)_entity;
        AnimatorManager.Instance().SetAnimation(entity.Animator, "BattleIdle", false);
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
            case (int)eMESSAGE_TYPE.TO_ATTACK:
                entity.StateMachine.ChangeState(eSTATE.ATTACK);
                return true;
            case (int)eMESSAGE_TYPE.TO_IDLE:
                entity.StateMachine.ChangeState(eSTATE.IDLE);
                return true;

            //case (int)eMESSAGE_TYPE.SET_FOMATION:
            //    Vector3 fomation = (Vector3)_msg.extraInfo;
            //    entity.SetTarget(fomation);
            //    return true;

            case (int)eMESSAGE_TYPE.TO_RUN:
                {
                    entity.StateMachine.ChangeState(eSTATE.RUN);

                }
                return true;

            case (int)eMESSAGE_TYPE.TO_ESCAPE:
                {
                    entity.DestinationCheck = Time.time;
                    Vector3 destination = entity.EscapePosition(entity.EntityGroup.NearestEnemy(entity.transform.position));
                    entity.SetTarget(destination);
                    entity.StateMachine.ChangeState(eSTATE.ESCAPE);

                }
                return true;

            case (int)eMESSAGE_TYPE.AVOID_ATTACK:
                {
                    Vector3 destination;
                    entity.DestinationCheck = Time.time;
                    Vector3 attackPos = (Vector3)_msg.extraInfo;
                    // 1/3확률로 공격 회피
                    if (0 == Random.Range(0, 5))
                    {
                        destination = entity.EscapePosition(attackPos);
                        entity.SetTarget(destination);
                        entity.StateMachine.ChangeState(eSTATE.ESCAPE);
                    }

                }
                return true;

            case (int)eMESSAGE_TYPE.PLESE_HEAL:
                {
                    BaseGameEntity hurtEntity = EntityManager.Instance.IDToEntity(_msg.sender);
                    AI hurtAI = hurtEntity.gameObject.GetComponent<AI>();

                    entity.GetComponent<Healer>().SetInjured(hurtAI);
                    entity.StateMachine.ChangeState(eSTATE.HEAL);
                }
                return true;
        }

        return false;
    }
}