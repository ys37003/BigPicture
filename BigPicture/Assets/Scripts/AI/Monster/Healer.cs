using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour {
    AI injured;
    bool healAlbe = true;

    [SerializeField]
    public AI owner;
    [SerializeField]
    GameObject effect;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetInjured(AI _entity)
    {
        injured = _entity;
    }

    public AI GetInjured()
    {
        return injured;
    }

    public void Heal()
    {
        if (false == healAlbe)
            return;

        StartCoroutine(Healing());
    }

    IEnumerator Healing()
    {
        float oldTime = Time.time;
        float endTime = Time.time;
        injured.buffUI.AddBuff(eBuff.Heal);
        effect.SetActive(true);
        healAlbe = false;
        while (true)
        {
            effect.transform.position = injured.transform.position;
            if (endTime + 5.0f < Time.time)
            {
                healAlbe = true;
                break;
            }

            if (oldTime + 1.0f < Time.time)
            {
                oldTime = Time.time;
                injured.Data.StatusData.HP += 5;
            }
            yield return null;
        }
        effect.SetActive(false);
        injured.buffUI.RemoveBuff(eBuff.Heal);
    }
}
