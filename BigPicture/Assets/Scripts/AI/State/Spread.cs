using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : State
{
    private static Spread instance;
    Partner entity;
    private Spread()
    {

    }

    public static Spread Instance()
    {
        if (instance == null)
        {
            instance = new Spread();
        }

        return instance;
    }

    public void Excute(object _entity)
    {

    }

    public void Enter(object _entity)
    {

    }

    public void Exit(object _entity)
    {

    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(object _entity, Telegram _msg)
    {
        switch (_msg.message)
        {
        }
        return false;
    }
}
