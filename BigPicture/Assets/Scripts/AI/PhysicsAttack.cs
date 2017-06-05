using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsAttack : AttackElement {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void Attack(GameObject _ob)
    {
        Debug.Log("PhysicsAttack");
    }

    void Debuff()
    {

    }

    void DamageAndDebuff()
    {

    }
}
