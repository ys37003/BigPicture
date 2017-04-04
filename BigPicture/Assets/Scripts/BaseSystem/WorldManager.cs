using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    private void Awake()
    {
        DataManager.Instance().monsterDataLoad();
        
    }

    // Update is called once per frame
    void Update () {
        StartCoroutine(MessageDispatcher.Instance.DispatchDelayedMessages());
    }
}
