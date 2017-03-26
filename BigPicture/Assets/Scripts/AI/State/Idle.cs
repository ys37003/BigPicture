﻿public class Idle<entity_type> : State<entity_type> where entity_type : Ork
{
    private static Idle<entity_type> instance;
    private Idle()
    { }

    public static Idle<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Idle<entity_type>();
        }
        return instance;
    }

    public override void Excute(entity_type _monster)
    {
        _monster.Idle();

        if(_monster.ToWalk())
        { 
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eChangeState.TO_WALK, null);
        }
    }
    public override void Enter(entity_type _monster)
    {
        _monster.SetClock( Clock.Instance.GetTime());
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Idle", true);

    }
    public override void Exit(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Idle", false );
    }

    /// <summary>
    /// Idle상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_monster"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public override bool OnMessage(entity_type _monster, Telegram _msg)
    {
        switch(_msg.message)
        {
            case (int)eChangeState.TO_WALK:
                _monster.GetStateMachine().ChangeState(eSTATE.WALK);
                return true;
        }

        return false;
    }
}