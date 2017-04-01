using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State<entity_type>
{
    void Excute(entity_type _entity_type);
    void Enter(entity_type _entity_type);
    void Exit(entity_type _entity_type);
    bool OnMessage(entity_type _entity_type, Telegram _msg);
}