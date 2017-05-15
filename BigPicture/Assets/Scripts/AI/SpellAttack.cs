using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttack {

    private GameObject spell;
    private ColliderAttack colliderAttack;
    private Collider collider;
    private Color color;
    private float time;
    // Use this for initializatio
    public void Init(GameObject _go)
    {
        spell = _go;
        color = spell.GetComponent<MeshRenderer>().material.color;
        collider = spell.GetComponent<BoxCollider>();
        collider.enabled = false;
    }


    public void Attack(Vector3 _pos)
    {
        _pos.y += .1f;
        spell.transform.position = _pos;
        spell.GetComponent<MeshRenderer>().enabled = true;
        color.a = 0.0f;
        spell.GetComponent<MeshRenderer>().material.color = color;
        spell.isStatic = true;
        //spell.GetComponent<Material>().color = color;
        time = Time.time;
    }

    public IEnumerator AttackDelay()
    {
        while (true)
        {
            if (time + 1.0f < Time.time)
            {
                color.a += 0.005f;
                spell.GetComponent<MeshRenderer>().material.color = color;

                if (1.0f < color.a)
                {
                    collider.enabled = true;
                    spell.GetComponent<MeshRenderer>().enabled = false;
                    CoroutineManager.Instance.StartCorutine(ColliderDelay());
                    break;
                }
            }
            yield return null;
        }
    }

    IEnumerator ColliderDelay()
    {
        time = Time.time;
        while(true)
        {
            if (time + 0.1f < Time.time)
            {
                spell.isStatic = false;
                collider.enabled = false;
                break;
            }
            yield return null;
        }
    }
}
