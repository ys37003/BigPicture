using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk<entity_type> : State<entity_type> where entity_type : Ork
{
    private static Walk<entity_type> instance;
    private Walk()
    { }

    public static Walk<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Walk<entity_type>();
        }
        return instance;
    }

    public override void Excute(entity_type _monster)
    {
        _monster.Walk();

        if (_monster.ToIdle())
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eChangeState.TO_IDLE, null);

    }

    public override void Enter(entity_type _monster)
    {
        _monster.clock = Clock.Instance.GetTime();
        AnimatorManager.Instance().SetAnimation(_monster.GetAnimator(), "Walk", true );
        _monster.GetNavAgent().target = MathAssist.Instance().RandomVector3(_monster.transform.position, 30.0f);
    }

    public override void Exit(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.GetAnimator(), "Walk", false );
        _monster.GetNavAgent().Clear();
    }

    public override bool OnMessage(entity_type _monster, Telegram _msg)
    {
        switch (_msg.message)
        {
            case (int)eChangeState.TO_IDLE:
                _monster.GetStateMachine().ChangeState(eSTATE.IDLE);
                return true;
        }

        return false;
    }

}
