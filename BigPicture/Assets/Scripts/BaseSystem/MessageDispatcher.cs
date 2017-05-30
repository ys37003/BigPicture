using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDispatcher
{
    private static MessageDispatcher instance;
    private List<Telegram> telegramList = new List<Telegram>();
    private MessageDispatcher()
    {
        
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
        private set { }
    }

    public void DispatchMessage(float _delay, int _sender, int _receiver, int _msg, object _extraInfo)
    {
        Telegram telegram = new Telegram();
        telegram.Clear();
        telegram.sender     = _sender;
        telegram.receiver   = _receiver;
        telegram.message    = _msg;
        telegram.extraInfo  = _extraInfo;

        if (0 >= _delay)
        {
            DisCharge(_receiver, telegram);
        }
        else
        {
            telegram.dispatchTime = Time.time + _delay;
      
            telegramList.Add(telegram);
            //delayMessageSD.Add(telegram.dispatchTime, telegram);
            //delayList.Add(telegram.dispatchTime);
        }
    }

    void DisCharge(int _receiver, Telegram _telegram)
    {
        BaseGameEntity receiver = EntityManager.Instance.IDToEntity(_receiver);
        receiver.HanleMessage(_telegram);
    }

    public void DeleteMessage(int _id, int _message)
    {
        for (int i = 0; i < telegramList.Count; ++i)
        {
            if(_id == telegramList[i].sender && _message == telegramList[i].message)
            {
                telegramList.RemoveAt(i);

            }
        }
    }

    public IEnumerator DispatchDelayedMessages()
    {
        while (true)
        {
            for (int i = 0; i < telegramList.Count; ++i)
            {
                if (telegramList[i].dispatchTime < Time.time && telegramList[i].dispatchTime > 0)
                {
                    Telegram telegram = telegramList[i];

                    DisCharge(telegram.receiver, telegram);
                    telegramList.RemoveAt(i);
                }
            }
            yield return null;
        }
    }

    void Sort()
    {
        telegramList.Sort(delegate (Telegram A, Telegram B)
        {
            if (A.dispatchTime > B.dispatchTime) return 1;
            else if (A.dispatchTime < B.dispatchTime) return -1;
            return 0;
        });
    }
}
