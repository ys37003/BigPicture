using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk<entity_type> : State<entity_type> where entity_type : HoodSkull
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
       

        //if( true == _monster.IsArrive())
        //    _monster.SetTarget(MathAssist.Instance().RandomVector3(_monster.GetGroup().GetGroupCenter(), 5.0f));
        if (true == _monster.IsArrive())
        {
            //MessageDispatcher.Instance.DeleteMessage(_monster.ID, (int)eMESSAGE_TYPE.TO_IDLE);
            MessageDispatcher.Instance.DispatchMessage(0, _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_IDLE, null);
        }
    }

    public void Enter(entity_type _monster)
    {
        BaseGameEntity foword = _monster.GetGroup().JobToEntity(eJOB_TYPE.FORWARD);
        _monster.SetTarget(_monster.SetDestination(_monster, _monster.GetGroup(),foword.transform.position));
        //_monster.SetTarget( MathAssist.Instance().RandomVector3(_monster.GetGroup().GetGroupCenter(), 5.0f));
        //MessageDispatcher.Instance.DispatchMessage((int)Random.Range(7,10), _monster.ID, _monster.ID, (int)eMESSAGE_TYPE.TO_IDLE, null);
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Walk", true);
    }

    public void Exit(entity_type _monster)
    {
        _monster.NavAgent.Clear();
        AnimatorManager.Instance().SetAnimation(_monster.Animator, "Walk", false);
        
        //_monster.NavAgent.StopCoroutine(_monster.NavAgent.MoveToTarget());
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
                //_monster.GetStateMachine().ChangeState(eSTATE.RUN);
                //_monster.GetStateMachine().ChangeState(eSTATE.BATTLEIDLE);
                GameObject enemy = (GameObject)_msg.extraInfo;
                _monster.SetEnemy(enemy);
                _monster.GetStateMachine().ChangeState(eSTATE.SETFOMATION);
                //_monster.SetTarget(enemy.transform.position);
                return true;

            case (int)eMESSAGE_TYPE.FLLOW_ME:
                _monster.SetTarget(MathAssist.Instance().RandomVector3((Vector3)_msg.extraInfo, 5.0f));
                return true;

            case (int)eMESSAGE_TYPE.SET_FOMATION:
                _monster.GetStateMachine().ChangeState(eSTATE.SETFOMATION);
                return true;
        }
        return false;
    }
}