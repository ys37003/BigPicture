using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorutineManager : Singleton<CorutineManager>
{
    public void StartCorutine(IEnumerator _function)
    {
        StartCoroutine(_function);
    }
}