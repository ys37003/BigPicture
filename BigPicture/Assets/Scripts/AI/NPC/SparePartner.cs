using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparePartner : MonoBehaviour{

    public ePARTNER_NAME name;

    [SerializeField]
    private BoxCollider colEyeSight;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    [SerializeField]
    private GameObject playerGroup;

    [SerializeField]
    private BaseGameEntity player;

    [SerializeField]
    private eJOB_TYPE _job;
    // Use this for initialization
    void Start () {
        name = ePARTNER_NAME.DONUT;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Human" && other.GetComponent<Character>() != null)
        {
            this.transform.LookAt(other.transform);
            if(Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("hi");
                ReCruitManager.Instance.StartRequist(this);
            }
        }
    }

    public void SetComponent()
    {
        this.transform.SetParent(playerGroup.transform);
        this.gameObject.AddComponent<Partner>();
        this.gameObject.GetComponent<Partner>().Init(colEyeSight, colliderAttack , player , _job);
        this.gameObject.GetComponentInChildren<HitCollider>().Init(this.gameObject.GetComponent<Partner>(), this.gameObject.GetComponent<Partner>());
        Destroy(this);
    }

}
