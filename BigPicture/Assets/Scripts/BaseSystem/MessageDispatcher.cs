using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDispatcher {


    private static MessageDispatcher instance;

    private MessageDispatcher()
    {
        //entityDic.Clear();
    }

    public static MessageDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MessageDispatcher();
            }

            return instance;
        }
    }


    Telegram telegram = new Telegram();

    public void DispatchMessage(float _delay , int _sender , int _receiver , int _msg , object _extraInfo)
    {
        telegram.Clear();
        telegram.sender = _sender;
        telegram.receiver = _receiver;
        telegram.message = _msg;
        telegram.extraInfo = _extraInfo;

        if(0 >= _delay)
        {
            DisCharge(_receiver, telegram);
        }
        else
        {
            //
            telegram.dispatchTime = Clock.Instance.GetTime() + _delay;

            //PriorityQ.insert(telegram);
        }
    }

    void DisCharge(int _receiver , Telegram _telegram)
    {
        BaseGameEntity receiver = EntityManager.Instance.IDToEntity(_receiver);
        receiver.HanleMessage(_telegram);
    }
}
