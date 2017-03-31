using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDispatcher : MonoBehaviour {

    private static MessageDispatcher instance;
    SortedDictionary<float, Telegram> delayMessageSD = new SortedDictionary<float, Telegram>();
    List<float> removeList = new List<float>();
    public static MessageDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(MessageDispatcher)) as MessageDispatcher;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("MessageDispatcher");
                instance = obj.AddComponent<MessageDispatcher>() as MessageDispatcher;
            }

            return instance;
        }
    }

    private void Update()
    {
        StartCoroutine("DispatchDelayedMessages");
    }

    public void DispatchMessage(float _delay , int _sender , int _receiver , int _msg , object _extraInfo)
    {
        Telegram telegram = new Telegram();
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
            telegram.dispatchTime = Time.time + _delay;
            delayMessageSD.Add(telegram.dispatchTime, telegram);
        }
    }

    void DisCharge(int _receiver , Telegram _telegram)
    {
        BaseGameEntity receiver = EntityManager.Instance.IDToEntity(_receiver);
        receiver.HanleMessage(_telegram);
    }

    public IEnumerator DispatchDelayedMessages()
    {
        foreach (KeyValuePair<float, Telegram> iter in delayMessageSD)
        {
            if ( iter.Value.dispatchTime < Time.time && iter.Value.dispatchTime > 0 )
            {
                DisCharge(iter.Value.receiver, iter.Value);
                removeList.Add(iter.Key);
            }
        }

        foreach (float iter in removeList)
        {
            delayMessageSD.Remove(iter);
        }
        yield return null;
    }
}
