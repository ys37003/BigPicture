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
            case (int)eMESSAGE_TYPE.COMMAND_COME_ON:
                owner.StateMachine.ChangeState(eSTATE.COME_ON);
                return true;
            case (int)eMESSAGE_TYPE.COMMAND_FOCUSING:
                Group emenyGroup = (Group)_msg.extraInfo;
                owner.SetEnemy(emenyGroup);
                return true;
        }
        return false;
    }
}
