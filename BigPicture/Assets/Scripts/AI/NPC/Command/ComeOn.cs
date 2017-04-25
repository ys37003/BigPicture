using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeOn : State {

    private static ComeOn instance;
    Partner entity;

    private ComeOn()
    {

    }

    public static ComeOn Instance()
    {
        if (instance == null)
        {
            instance = new ComeOn();
        }

        return instance;
    }

    public void Excute(object _entity)
    {
        entity = (Partner)_entity;
    }

    public void Enter(object _entity)
    {
        entity = (Partner)_entity;
    }

    public void Exit(object _entity)
    {
        entity = (Partner)_entity;
    }

    /// <summary>
    /// Walk 상태에서 받은 메세지 처리
    /// </summary>
    /// <param name="_entity"></param>
    /// <param name="_msg"></param>
    /// <returns></returns>
    public bool OnMessage(object _entity, Telegram _msg)
    {
        entity = (Partner)_entity;
        switch (_msg.message)
        {
        }
        return false;
    }
}
