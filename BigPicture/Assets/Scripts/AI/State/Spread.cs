using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread<entity_type> : State<entity_type> where entity_type : AI
{
    private static Spread<entity_type> instance;

    private Spread()
    {

    }

    public static Spread<entity_type> Instance()
    {
        if (instance == null)
        {
            instance = new Spread<entity_type>();
        }

        return instance;
    }

    public void Excute(entity_type _entity)
    {

    }

    public void Enter(entity_type _entity)
    {

    }

    public void Exit(entity_type _entity)
    {

    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(entity_type _entity, Telegram _msg)
    {
        switch (_msg.message)
        {
        }
        return false;
    }
}
