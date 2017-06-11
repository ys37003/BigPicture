using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttack : AttackElement{

    private AI owner;
    private GameObject spell;
    private Collider collider;
    private Color color;
    private float time;
    // Use this for initializatio
    public override void Init(AI _onwer , GameObject _go = null)
    {
        spell = _go;
        color = spell.GetComponent<MeshRenderer>().material.color;
        collider = spell.GetComponent<BoxCollider>();
        collider.enabled = false;
        owner = _onwer;
    }

    public override void Attack(GameObject _go)
    {
        spell.transform.position = _go.transform.position;
        spell.GetComponent<MeshRenderer>().enabled = true;
        color.a = 0.0f;
        spell.GetComponent<MeshRenderer>().material.color = color;
        time = Time.time;
        CoroutineManager.Instance.CStartCoroutine(AttackDelay());
    }

    void NomalAttack()
    {

    }

    void Healing()
    {

    }

    void Bleeding()
    {

    }

    void Poisoning()
    {

    }
    IEnumerator AttackDelay()
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
                    CoroutineManager.Instance.CStartCoroutine(ColliderDelay());
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
