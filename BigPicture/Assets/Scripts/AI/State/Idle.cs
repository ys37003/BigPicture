using UnityEngine;

public class Idle<entity_type> : State<entity_type> where entity_type : HoodSkull
{
    private static Idle<entity_type> instance;

    private Idle()
    {

    }

    public static Idle<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Idle<entity_type>();
        }

        return instance;
    }

    public void Excute(entity_type _monster)
    {
        _monster.Idle();
    }

    public void Enter(entity_type _monster)
    {
        //_monster.SetClock( Clock.Instance.GetTime());
        _monster.EnemyClear();
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Idle", true);
        //_monster.ToWalk(_monster);
        MessageDispatcher.Instance.DispatchMessage((int)Random.Range(7, 10), _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_WALK, null);
    }

    public void Exit(entity_type _monster)
    {
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Idle", false );
    }

    /// <summary>
    /// Idle상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_monster"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(entity_type _monster, Telegram _msg)
    {
        switch(_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_WALK:
                _monster.GetStateMachine().ChangeState(eSTATE.WALK);
                return true;

            case (int)eMESSAGE_TYPE.FIND_ENEMY:
                //_monster.GetStateMachine().ChangeState(eSTATE.RUN);
                //_monster.GetStateMachine().ChangeState(eSTATE.BATTLEIDLE);
                GameObject enemy = (GameObject)_msg.extraInfo;
                _monster.SetEnemy(enemy);
                _monster.GetStateMachine().ChangeState(eSTATE.SETFOMATION);
                //_monster.SetTarget(enemy.transform.position );
                return true;

            case (int)eMESSAGE_TYPE.FLLOWME:
                _monster.GetStateMachine().ChangeState(eSTATE.WALK);
                _monster.SetTarget(MathAssist.Instance().RandomVector3((Vector3)_msg.extraInfo,5.0f));
                return true;

            case (int)eMESSAGE_TYPE.SETFOMATION:
                _monster.GetStateMachine().ChangeState(eSTATE.SETFOMATION);
                return true;
        }

        return false;
    }
}