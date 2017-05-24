using UnityEngine;

public class Idle : State
{
    private static Idle instance;
    AI entity;

    private Idle()
    {

    }

    public static Idle Instance()
    {
        if (instance == null)
        {
            instance = new Idle();
        }
        return instance;
    }


    public void Excute(object _entity)
    {
        entity = (AI)_entity;
        entity.Idle();
    }

    public void Enter(object _entity)
    {
        entity = (AI)_entity;

        entity.EnemyClear();
        AnimatorManager.Instance().SetAnimation(entity.Animator, "Idle", true);
        MessageDispatcher.Instance.DispatchMessage((int)Random.Range(7, 10), entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_WALK, null);
    }

    public void Exit(object _entity)
    {
        entity = (AI)_entity;

        AnimatorManager.Instance().SetAnimation(entity.Animator, "Idle", false );
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
            case (int)eMESSAGE_TYPE.TO_WALK:
                entity.StateMachine.ChangeState(eSTATE.WALK);
                return true;

            case (int)eMESSAGE_TYPE.FLLOW_ME:
                entity.StateMachine.ChangeState(eSTATE.WALK);
                entity.SetTarget(MathAssist.Instance().RandomVector3((Vector3)_msg.extraInfo,5.0f));
                return true;

            case (int)eMESSAGE_TYPE.SET_FOMATION:
                entity.StateMachine.ChangeState(eSTATE.SETFOMATION);
                return true;

            case (int)eMESSAGE_TYPE.FIND_ENEMY:
                entity.Group.EnemyGroup = (Group)_msg.extraInfo;
                if (null != entity.Group.EnemyGroup)
                {
                    entity.SetEnemy(entity.Group.NearestEnemy(entity.transform.position));
                    entity.StateMachine.ChangeState(eSTATE.SETFOMATION);
                }
                return true;
        }
        return false;
    }
}