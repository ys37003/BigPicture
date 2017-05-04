using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public GameObject cube;
    Vector3 test;
	// Use this for initialization
	void Start () {
        test = this.transform.position - cube.transform.position;


    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position += test;

    }
}
