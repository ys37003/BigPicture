using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    private void Awake()
    {
        DataManager.Instance().DataLoad();
        StartCoroutine(MessageDispatcher.Instance.DispatchDelayedMessages());
    }

    // Update is called once per frame
    void Update () {
        
    }
}
