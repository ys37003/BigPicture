using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUI : MonoBehaviour {

    StatusData buffStatus;
	// Use this for initialization
	void Start () {
        buffStatus = this.transform.GetComponent<BattleEntity>().AddStatus;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
