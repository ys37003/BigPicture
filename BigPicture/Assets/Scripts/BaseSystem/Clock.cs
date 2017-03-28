using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock  : MonoBehaviour{

    private static Clock instance;

    public static Clock Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(Clock)) as Clock;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("Clock");
                instance = obj.AddComponent<Clock>() as Clock;
            }
            return instance;
        }
    }

    float currentTime = 0;
    void Update()
    {
        currentTime += Time.deltaTime;
    }

    /// <summary>
    /// 현재 시간 반환
    /// </summary>
    /// <returns></returns>
    public float GetTime()
    {
        return currentTime;
    }
}