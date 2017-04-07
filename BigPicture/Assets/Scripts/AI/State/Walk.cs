using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk<entity_type> : State<entity_type> where entity_type : Ork
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

    public void Excute(entity_type _monster)
    {
        _monster.Walk();
        Debug.DrawLine(_monster.transform.position, _monster.NavAgent.target, Color.red);
    }

    public void Enter(entity_type _monster)
    {
        _monster.SetClock(Time.time);
        _monster.SetTarget(MathAssist.Instance().RandomVector3(_monster.transform.position, 30.0f));
        MessageDispatcher.Instance.DispatchMessage((int)Random.Range(3,7), _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_IDLE, null);
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Walk", true);
    }

    public void Exit(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Walk", false);
        _monster.NavAgent.Clear();
    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_monster"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(entity_type _monster, Telegram _msg)
    {
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_IDLE:
                _monster.GetStateMachine().ChangeState(eSTATE.IDLE);
                return true;

            case (int)eMESSAGE_TYPE.FIND_ENEMY:
                _monster.GetStateMachine().ChangeState(eSTATE.RUN);
                _monster.SetTarget((Vector3)_msg.extraInfo);
                return true;
        }
        return false;
    }
}