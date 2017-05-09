using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttack : AttackHandler {

    [SerializeField]
    private GameObject spell;

	// Use this for initialization
	void Start () {
        //Instantiate(spell, new Vector3(1,1,1), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Attack()
    {

    }

    IEnumerator SpellDelay(GameObject _spell)
    {
        yield return null;
    }
}
