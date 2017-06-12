using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEffect
{
    PUNCH,
    SLASH,
    EXPLOSION,
    BLEEDING,
    POISONING
}

public class EffectPool : Singleton<EffectPool> {

    [SerializeField]
    List<GameObject> PUNCH = new List<GameObject>();
    [SerializeField]
    List<GameObject> SLASH = new List<GameObject>();
    [SerializeField]
    List<GameObject> EXPLOSION = new List<GameObject>();
    [SerializeField]
    List<GameObject> BLEEDING = new List<GameObject>();
    [SerializeField]
    List<GameObject> POISONING = new List<GameObject>();

    // Use this for initialization
    void Start () {
        Transform effect = this.transform.Find("PUNCH");
		for(int i = 0; i < effect.childCount; ++ i )
        {
            effect.GetChild(i).gameObject.SetActive(false);
            PUNCH.Add(effect.GetChild(i).gameObject);

        }

        effect = this.transform.Find("SLASH");
        for (int i = 0; i < effect.childCount; ++i)
        {
            effect.GetChild(i).gameObject.SetActive(false);
            SLASH.Add(effect.GetChild(i).gameObject);
        }

        effect = this.transform.Find("EXPLOSION");
        for (int i = 0; i < effect.childCount; ++i)
        {
            effect.GetChild(i).gameObject.SetActive(false);
            EXPLOSION.Add(effect.GetChild(i).gameObject);
        }

        effect = this.transform.Find("BLEEDING");
        for (int i = 0; i < effect.childCount; ++i)
        {
            effect.GetChild(i).gameObject.SetActive(false);
            BLEEDING.Add(effect.GetChild(i).gameObject);
        }

        effect = this.transform.Find("POISONING");
        for (int i = 0; i < effect.childCount; ++i)
        {
            effect.GetChild(i).gameObject.SetActive(false);
            POISONING.Add(effect.GetChild(i).gameObject);
        }
    }
	
	public GameObject Pop(eEffect _value)
    {
        GameObject dummy = null;

        switch(_value)
        {
            case eEffect.PUNCH:
                {
                    dummy = PUNCH[0];
                    PUNCH.RemoveAt(0);
                }
                break;
            case eEffect.SLASH:
                {
                    dummy = SLASH[0];
                    SLASH.RemoveAt(0);
                }
                break;
            case eEffect.EXPLOSION:
                {
                    dummy = EXPLOSION[0];
                    EXPLOSION.RemoveAt(0);
                }
                break;
            case eEffect.BLEEDING:
                {
                    dummy = BLEEDING[0];
                    BLEEDING.RemoveAt(0);
                }
                break;
            case eEffect.POISONING:
                {
                    dummy = POISONING[0];
                    POISONING.RemoveAt(0);
                }
                break;
        }
        return dummy;
    }

    public void Push(GameObject _go)
    {
        if ("PUNCH" == _go.transform.parent.name)
        {
            PUNCH.Add(_go);  
        }
        else if ("SLASH" == _go.transform.parent.name)
        {
            SLASH.Add(_go);
        }
        else if ("EXPLOSION" == _go.transform.parent.name)
        {
            EXPLOSION.Add(_go);
        }
        else if ("BLEEDING" == _go.transform.parent.name)
        {
            BLEEDING.Add(_go);
        }
        else if ("POISONING" == _go.transform.parent.name)
        {
            POISONING.Add(_go);
        }
    }
}
