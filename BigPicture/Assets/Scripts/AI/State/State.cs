using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<entity_type> {

    public virtual void Excute(entity_type _entity_type)
    {

    }
    public virtual void Enter(entity_type _entity_type)
    {

    }
    public virtual void Exit(entity_type _entity_type)
    {

    }

    public virtual bool OnMessage(entity_type _entity_type , Telegram _msg)
    {
        return false;
    }
}
