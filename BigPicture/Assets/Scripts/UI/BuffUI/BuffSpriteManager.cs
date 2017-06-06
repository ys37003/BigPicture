using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpriteManager : Singleton<BuffSpriteManager> {

    [SerializeField]
    Dictionary<string, Sprite> buffSpriteDIc = new Dictionary<string, Sprite>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
