using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController
{
    private Partner owner;
    public CommandController(Partner _owner)
    {
        owner = _owner;
    }

    public bool HandleMessgae(Telegram _msg)
    {
        switch (_msg.message)
        {
            case (int)eCommandType.COME_ON:
                Debug.Log("ggggggggggggggggggggggggggg");
                return true;
        }
        return false;
    }
}
