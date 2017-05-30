using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttack {

    private AI owner;
    private GameObject spell;
    private Collider collider;
    private Color color;
    private float time;
    // Use this for initializatio
    public void Init(GameObject _go , AI _onwer)
    {
        spell = _go;
        color = spell.GetComponent<MeshRenderer>().material.color;
        collider = spell.GetComponent<BoxCollider>();
        collider.enabled = false;
        owner = _onwer;
    }


    public void Attack(Vector3 _pos)
    {
        _pos.y += 0.1f;
        spell.transform.position = _pos;
        spell.GetComponent<MeshRenderer>().enabled = true;
        color.a = 0.0f;
        spell.GetComponent<MeshRenderer>().material.color = color;
        //spell.GetComponent<Material>().color = color;
        time = Time.time;

    }

    public IEnumerator AttackDelay()
    {
        MessageDispatcher.Instance.DispatchMessage(0.5f, owner.ID, owner.EnemyHandle.GetEnemy(0).enemy.GetComponent<BaseGameEntity>().ID, (int)eMESSAGE_TYPE.AVOID_ATTACK,owner.transform.position);
        while (true)
        {
            if (time + 0.5f < Time.time)
            {
                color.a += 0.01f;
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
                collider.enabled = false;
                break;
            }
            yield return null;
        }
    }
}
