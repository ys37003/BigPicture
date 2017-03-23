using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol<entity_type> : State<entity_type> where entity_type : Ork
{

    private static Patrol<entity_type> instance;
    private Patrol()
    { }

    public static Patrol<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Patrol<entity_type>();
        }
        return instance;
    }

    public override void Excute(entity_type _monster)
    {
        _monster.Patrol();

        if(_monster.ToIdle())
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eChangeState.TO_IDLE, null);

    }
    public override void Enter(entity_type _monster)
    {
        
    }
    public override void Exit(entity_type _monster)
    {

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
