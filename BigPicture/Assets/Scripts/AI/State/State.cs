using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State
{
    void Excute(object _entity_type);
    void Enter(object _entity_type);
    void Exit(object _entity_type);
    bool OnMessage(object _entity_type, Telegram _msg);
}