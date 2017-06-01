using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandle : MonoBehaviour {

    bool effectAble = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(this.tag != other.tag)
        {
            if (true == effectAble)
            {
                GameObject effect = EffectPool.Instance.Pop();
                StartCoroutine(effect.GetComponent<SelfDestruct>().LifeTime());

                effect.transform.position = this.transform.position + this.transform.forward ;

                effect.SetActive(true);
                effectAble = false;
                StartCoroutine(Deley());
            }
        }
    }

    IEnumerator Deley()
    {
        float oldTime = Time.time;
        while(true)
        {
            if(oldTime + 0.5f < Time.time)
            {
                effectAble = true;
                break;
            }
            yield return null;
        }
    }
}
