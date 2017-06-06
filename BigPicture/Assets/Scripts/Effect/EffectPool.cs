using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : Singleton<EffectPool> {

    [SerializeField]
    List<GameObject> effect1_Pool = new List<GameObject>();
    [SerializeField]
    List<GameObject> effect2_Pool = new List<GameObject>();
    // Use this for initialization
    void Start () {

        Transform effect = this.transform.Find("Effect1");
		for(int i = 0; i < effect.childCount; ++ i )
        {
            effect.GetChild(i).gameObject.SetActive(false);
            effect1_Pool.Add(effect.GetChild(i).gameObject);

        }

        effect = this.transform.Find("Effect2");
        for (int i = 0; i < effect.childCount; ++i)
        {
            effect.GetChild(i).gameObject.SetActive(false);
            effect2_Pool.Add(effect.GetChild(i).gameObject);
        }
    }
	
	public GameObject Pop(int _value)
    {
        GameObject dummy = null;


        switch(_value)
        {
            case 0:
                {
                    dummy = effect1_Pool[0];
                    effect1_Pool.RemoveAt(0);
                }
                break;
            case 1:
                {
                    dummy = effect2_Pool[0];
                    effect2_Pool.RemoveAt(0);
                }
                break;
        }
        return dummy;
    }

    public void Push(GameObject _go)
    {
        if ("Effect1" == _go.transform.parent.name)
        {
            effect1_Pool.Add(_go);  
        }
        else if ("Effect2" == _go.transform.parent.name)
        {
            effect2_Pool.Add(_go);
        }
    }
}
