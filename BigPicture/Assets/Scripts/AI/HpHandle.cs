using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpHandle : MonoBehaviour {

    [SerializeField]
    AI owner;
	// Use this for initialization
	void Start () {
        owner = this.transform.GetComponent<AI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator HpCheck()
    {
        float checkTime = Time.time;
        while(true)
        {
            if(checkTime + 3.0f < Time.time )
            {
                checkTime = Time.time;
                Debug.Log("Current HP : " + owner.Data.StatusData.HP);
                if ( 30 > owner.Data.StatusData.HP )
                {
                    owner.Group.DispatchMessageGroup(0, owner.ID, (int)eJOB_TYPE.SUPPORT, (int)eMESSAGE_TYPE.PLESE_HEAL, null);
                }
            }
            yield return null;
        }
    }
}
