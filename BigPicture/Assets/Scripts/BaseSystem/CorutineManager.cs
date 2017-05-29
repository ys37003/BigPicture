using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : Singleton<CoroutineManager>
{
    public void StartCorutine(IEnumerator _function)
    {
        StartCoroutine(_function);
    }

    public void StopCoroutine(IEnumerator _function)
    {
        StopCoroutine(_function);
    }
}