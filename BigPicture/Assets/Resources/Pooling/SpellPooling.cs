using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPooling : Singleton<SpellPooling>
{
    [SerializeField]
    private List<GameObject> spellList = new List<GameObject>();
	// Use this for initialization
	void Start () {
		for(int i = 0; i < this.transform.childCount; ++i)
        {
            spellList.Add(this.transform.GetChild(i).gameObject);
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
	}

    public GameObject Pop()
    {
        GameObject dummy = spellList[spellList.Count - 1];

        dummy.SetActive(true);

        return dummy;
    }

    public void Push(GameObject _spell)
    {
        spellList.Add(_spell);
    }

}
