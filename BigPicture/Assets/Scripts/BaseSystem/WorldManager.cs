using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : Singleton<WorldManager> {

    public bool pause { get; set; }

    private void Awake()
    {
        pause = false;
        DataManager.Instance().DataLoad();
        StartCoroutine(MessageDispatcher.Instance.DispatchDelayedMessages());
    }

    // Update is called once per frame
    void Update () {
        
    }

    public void SetPause(bool _value)
    {
        //foreach(BaseGameEntity entity in EntityManager.Instance.GetEntityList().Values)
        //{

        //}
    }
}
