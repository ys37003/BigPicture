public class Idle<entity_type> : State<entity_type> where entity_type : Ork
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
        
        //if(Clock.Instance.GetTime()  > _monster.clock + 5.0f)
        //    MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eChangeState.TO_PATROL, null);

        if(_monster.ToPatrol())
        {
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eChangeState.TO_PATROL, null);
        }
    }
    public override void Enter(entity_type _monster)
    {
        _monster.clock = Clock.Instance.GetTime();
    }
    public override void Exit(entity_type _monster)
    {

    }

    public override bool OnMessage(entity_type _monster, Telegram _msg)
    {
        switch(_msg.message)
        {
            case (int)eChangeState.TO_PATROL:
                _monster.GetStateMachine().ChangeState(eSTATE.PATROL);
                return true;
        }

        return false;
    }
}