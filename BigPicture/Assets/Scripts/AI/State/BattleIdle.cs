using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleIdle<entity_type> : State<entity_type> where entity_type : Ork
{
    private static BattleIdle<entity_type> instance;
    private BattleIdle()
    { }

    public static BattleIdle<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new BattleIdle<entity_type>();
        }
        return instance;
    }

    public override void Excute(entity_type _monster)
    {
       
    }
    public override void Enter(entity_type _monster)
    {
       
    }
    public override void Exit(entity_type _monster)
    {
    }

    /// <summary>
    /// Idle상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_monster"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public override bool OnMessage(entity_type _monster, Telegram _msg)
    {
        switch (_msg.message)
        {
           
        }

        return false;
    }
}
