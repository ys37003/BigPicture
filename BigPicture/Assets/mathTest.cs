using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mathTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 fwd = transform.TransformDirection(Vector3.down);


        //RaycastHit hit;
        ////Ray ray = fwd;
        //if (Physics.Raycast(ray, out hit))
        //    if (hit.collider != null)
        //        hit.collider.enabled = false;

        //if (Physics.Raycast(transform.position, fwd, ))
        //    print("There is something in front of the object!");

        //Debug.DrawRay(transform.position, fwd, Color.red );
    }
}
