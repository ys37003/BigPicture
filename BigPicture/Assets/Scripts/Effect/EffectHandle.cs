using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandle : MonoBehaviour {

    bool effectAble = true;
    AI owner;
	// Use this for initialization
	void Start () {
        owner = this.transform.GetComponentInParent<AI>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(this.tag != other.tag)
        {
            if (true == effectAble && eSTATE.ATTACK == owner.GetCurrentState())
            {
                GameObject effect = EffectPool.Instance.Pop(0);
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

    public void NomalAttack()
    {
        if (true == effectAble && eSTATE.ATTACK == owner.GetCurrentState())
        {
            GameObject effect = EffectPool.Instance.Pop(0);
            StartCoroutine(effect.GetComponent<SelfDestruct>().LifeTime());

            effect.transform.position = this.transform.position + this.transform.forward;

            effect.SetActive(true);
            effectAble = false;
            StartCoroutine(Deley());
        }
    }
}
