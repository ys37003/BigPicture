using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : Singleton<CoroutineManager>
{
    public void CStartCoroutine(IEnumerator _function)
    {
        StartCoroutine(_function);
    }

    public void CStopCoroutine(IEnumerator _function)
    {
        CStopCoroutine(_function);
    }
}