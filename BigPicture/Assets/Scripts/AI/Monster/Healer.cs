using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour {
    AI injured;
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

    public void Heal(float _value)
    {
        injured.Data.StatusData.HP += _value;
    }
}
