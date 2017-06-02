using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : Singleton<EffectPool> {

    [SerializeField]
    List<GameObject> effectPool = new List<GameObject>();
	// Use this for initialization
	void Start () {
		for(int i = 0; i <this.transform.childCount; ++ i )
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
            effectPool.Add(this.transform.GetChild(i).gameObject);

        }
	}
	
	public GameObject Pop()
    {
        GameObject dummy = effectPool[0];

        effectPool.RemoveAt(0);
        return dummy;
    }

    public void Push(GameObject _go)
    {
        effectPool.Add(_go);
    }
}
