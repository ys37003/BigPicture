using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Node
{
    Vector3 position;
    int value;
}

public class BattleAreaManager : MonoBehaviour {

    Node[,] humanArea= new Node[9,9];
    Node[,] MonsterArea = new Node[9,9];
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
