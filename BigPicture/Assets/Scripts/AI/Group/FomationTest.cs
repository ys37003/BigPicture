using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FomationTest : MonoBehaviour {

    public GameObject Cube;
    public GameObject Forward;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Cube.transform.position = (Forward.transform.position +
                                  (-Forward.transform.forward * 3));

    }
}
