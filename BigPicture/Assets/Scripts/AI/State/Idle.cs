using UnityEngine;

public class Idle<entity_type> : State<entity_type> where entity_type : AI
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

    public void Excute(entity_type _entity)
    {
        _entity.Idle();
    }

    public void Enter(entity_type _entity)
    {
        _entity.EnemyClear();
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "Idle", true);
        MessageDispatcher.Instance.DispatchMessage((int)Random.Range(7, 10), _entity.ID, _entity.ID, (int)eMESSAGE_TYPE.TO_WALK, null);
    }

    public void Exit(entity_type _entity)
    {
        AnimatorManager.Instance().SetAnimation(_entity.Animator, "Idle", false );
    }

    /// <summary>
    /// Idle상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(entity_type _entity, Telegram _msg)
    {
        switch(_msg.message)
        {
            case (int)eMESSAGE_TYPE.TO_WALK:
                _entity.StateMachine.ChangeState(eSTATE.WALK);
                return true;

            case (int)eMESSAGE_TYPE.FIND_ENEMY:
                GameObject enemy = (GameObject)_msg.extraInfo;
                _entity.SetEnemy(enemy);
                _entity.StateMachine.ChangeState(eSTATE.SETFOMATION);
                return true;

            case (int)eMESSAGE_TYPE.FLLOW_ME:
                _entity.StateMachine.ChangeState(eSTATE.WALK);
                _entity.SetTarget(MathAssist.Instance().RandomVector3((Vector3)_msg.extraInfo,5.0f));
                return true;

            case (int)eMESSAGE_TYPE.SET_FOMATION:
                _entity.StateMachine.ChangeState(eSTATE.SETFOMATION);
                return true;
        }

        return false;
    }
}