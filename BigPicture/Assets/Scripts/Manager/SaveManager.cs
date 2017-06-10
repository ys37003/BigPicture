using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    /// <summary>
    /// index == ePARTNER_NAME
    /// </summary>
    private Dictionary<ePARTNER_NAME, int> likeabillityDic = new Dictionary<ePARTNER_NAME, int>();
    private Dictionary<int, bool> talkRepeatDic = new Dictionary<int, bool>();

    private void Awake()
    {
        for (ePARTNER_NAME name = ePARTNER_NAME.QQ; name <= ePARTNER_NAME.CHOCKCHOCK; ++name)
        {
            likeabillityDic.Add(name, 0);
        }

        for (int i = 0; i < 10; ++i)
        {
            talkRepeatDic.Add(i, true);
        }
    }

    public int GetLikeavillity(ePARTNER_NAME name)
    {
        if (!likeabillityDic.ContainsKey(name))
            return 0;

        return likeabillityDic[name];
    }

    public void AddLikeavillity(ePARTNER_NAME name, int point)
    {
        if (likeabillityDic.ContainsKey(name))
            likeabillityDic[name] += point;
    }

    public bool IsTalkRepeat(int talkNumber)
    {
        if (!talkRepeatDic.ContainsKey(talkNumber))
            return false;

        return talkRepeatDic[talkNumber];
    }

    public void TalkRepeatEnd(int talkNumber)
    {
        if (!talkRepeatDic.ContainsKey(talkNumber))
        {
            talkRepeatDic.Add(talkNumber, false);
        }
        else
        {
            talkRepeatDic[talkNumber] = false;
        }
    }
}