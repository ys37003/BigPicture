using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandle : MonoBehaviour {

    bool effectAble = true;
    AI owner;

    private eEffect effect = eEffect.PUNCH;

	// Use this for initialization
	void Start () {
        effect = eEffect.PUNCH;
        owner = this.transform.GetComponentInParent<AI>();

        if (null == owner)
            owner = this.transform.parent.GetComponentInChildren<AI>();

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEffect(eEffect _effect)
    {
        effect = _effect;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.tag != other.tag)
        {
            if (true == effectAble )
            {
                GameObject goEffect = EffectPool.Instance.Pop(effect);

                StartCoroutine(goEffect.GetComponent<SelfDestruct>().LifeTime());

                goEffect.transform.position = this.transform.position + this.transform.forward ;

                goEffect.SetActive(true);
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
