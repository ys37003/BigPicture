using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleIdle<entity_type> : State<entity_type> where entity_type : Ork
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
    }

    public void Enter(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Idle", true);
    }

    public void Exit(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Idle", false);
        //_monster.NavAgent.Clear();
        //_monster.EnemyClear();
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

            case (int)eMESSAGE_TYPE.ATTACKABLE:
                _monster.AttackAble = true;
                return true;

            case (int)eMESSAGE_TYPE.TO_IDLE:
                _monster.GetStateMachine().ChangeState(eSTATE.IDLE);
                return true;
            case (int)eMESSAGE_TYPE.TO_ROLLING:
                _monster.GetStateMachine().ChangeState(eSTATE.ROLLING);
                return true;
        }

        return false;
    }
}