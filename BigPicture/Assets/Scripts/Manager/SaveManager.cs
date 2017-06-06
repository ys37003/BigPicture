using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    /// <summary>
    /// index == ePARTNER_NAME
    /// </summary>
    private List<int> likeabillityList = new List<int>();

    private void Awake()
    {
        for (ePARTNER_NAME name = ePARTNER_NAME.DONUT; name <= ePARTNER_NAME.CHOCKCHOCK; ++name)
        {
            likeabillityList.Add(0);
        }
    }

    public int GetLikeavillity(ePARTNER_NAME name)
    {
        return likeabillityList[(int)name];
    }

    public void AddLikeavillity(ePARTNER_NAME name, int point)
    {
        likeabillityList[(int)name] += point;
    }
}