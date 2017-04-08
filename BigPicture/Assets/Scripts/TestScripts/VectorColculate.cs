using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VectorColculate : MonoBehaviour {

    public GameObject ob1;
    public GameObject ob2;
    public GameObject ob3;

    public float VectorSize_ob1;
    public float VectorSize_ob2;

    public Text fowordVector_ob1;
    public Text fowordVector_ob2;

    public Text innerVector;


    string foword_ob1 = "ob1 전방백터 : ";
    string foword_ob2 = "ob2 전방백터 : ";
    string inner = "두백터의 내적 : ";
    // Use this for initialization
    void Start () {

        fowordVector_ob1.text = foword_ob1;
        fowordVector_ob2.text = foword_ob2;
        innerVector.text = inner;
    }
	
	// Update is called once per frame
	void Update () {
        fowordVector_ob1.text = foword_ob1 + ob1.transform.forward;
        fowordVector_ob2.text = foword_ob2 + ob2.transform.forward;
        innerVector.text = inner;

        ob3.transform.position = (ob1.transform.position + ob1.transform.forward) + (ob2.transform.position + ob2.transform.forward);
    }
}
