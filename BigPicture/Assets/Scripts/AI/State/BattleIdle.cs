using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleIdle<entity_type> : State<entity_type> where entity_type : HoodSkull
{
    private static BattleIdle<entity_type> instance;

    private BattleIdle()
    {

    }

    public static BattleIdle<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new BattleIdle<entity_type>();
        }

        return instance;
    }

    public void Excute(entity_type _monster)
    {
        _monster.BattleIdle();

        //if (true == _monster.AttackAble)
        //{
        //    MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_ATTACK, null);
        //    return;
        //}

        //if (false == _moanster.ToBattleIdle())
        //{
        //    MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_IDLE, null);
        //    _monster.NavAgent.Clear();
        //    _monster.EnemyClear();
        //    return;
        //}
    }

    public void Enter(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "BattleIdle", true);
    }

    public void Exit(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "BattleIdle", false);
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
            case (int)eMESSAGE_TYPE.TO_ATTACK:
                _monster.GetStateMachine().ChangeState(eSTATE.ATTACK);
                return true;
            case (int)eMESSAGE_TYPE.TO_IDLE:
                _monster.GetStateMachine().ChangeState(eSTATE.IDLE);
                return true;
            case (int)eMESSAGE_TYPE.TO_ROLLING:
                _monster.GetStateMachine().ChangeState(eSTATE.ROLLING);
                return true;

            case (int)eMESSAGE_TYPE.SETFOMATION:
                Vector3 fomation = (Vector3)_msg.extraInfo;
                _monster.SetTarget(fomation);
                _monster.NavAgent.StartCoroutine(_monster.NavAgent.MoveToTarget());
                return true;
        }

        return false;
    }
}