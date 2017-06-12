using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttack : AttackElement{

    private AI owner;
    private GameObject spell;
    private Collider collider;
    private Color color;
    private float time;
    ColliderAttack colliderAttack;
    // Use this for initializatio
    public override void Init(AI _onwer, ColliderAttack _colliderAttack, GameObject _go = null)
    {
        colliderAttack = _colliderAttack;
        spell = _go;
        color = spell.GetComponent<MeshRenderer>().material.color;
        collider = spell.GetComponent<BoxCollider>();
        collider.enabled = false;
        owner = _onwer;
    }

    public override void Attack(GameObject _go)
    {
        switch (Random.Range(0, 5))
        {
            case 0:
                {
                    if(eJOB_TYPE.FORWARD == owner.Job)
                    {
                        Bleeding(_go);
                    }

                    if (eJOB_TYPE.SUPPORT == owner.Job)
                    {
                        Poisoning(_go);
                    }

                }
                break;

            default:
                {
                    if (eJOB_TYPE.FORWARD == owner.Job)
                    {
                        Bleeding(_go);
                    }

                    if (eJOB_TYPE.SUPPORT == owner.Job)
                    {
                        Poisoning(_go);
                    }
                    //NomalAttack(_go);
                }
                break;
        }
    }

    void NomalAttack(GameObject _go)
    {
        spell.GetComponent<EffectHandle>().SetEffect(eEffect.EXPLOSION);
        spell.transform.position = _go.transform.position;
        colliderAttack.SetDamageType(eDAMAGE_TYPE.SPELL);
        try
        {
            _go.GetComponentInChildren<HitCollider>().GetDamage(colliderAttack);
        }
        catch
        {
            _go.GetComponent<Character>().GetDamage(colliderAttack);
        }
    }

    void Bleeding(GameObject _go)
    {
        AI entity = _go.GetComponent<AI>();

        spell.GetComponent<EffectHandle>().SetEffect(eEffect.BLEEDING);
        spell.transform.position = _go.transform.position;
        spell.GetComponent<MeshRenderer>().enabled = true;
        color.a = 0.0f;
        spell.GetComponent<MeshRenderer>().material.color = color;
        time = Time.time;
        colliderAttack.SetDamageType(eDAMAGE_TYPE.BLEEDING);
        CoroutineManager.Instance.CStartCoroutine(AttackDelay());
    }

    void Poisoning(GameObject _go)
    {
        //if (false == skillAble)
        //    return;

        AI entity = _go.GetComponent<AI>();

        spell.GetComponent<EffectHandle>().SetEffect(eEffect.POISONING);
        spell.transform.position = _go.transform.position;
        spell.GetComponent<MeshRenderer>().enabled = true;
        color.a = 0.0f;
        spell.GetComponent<MeshRenderer>().material.color = color;
        time = Time.time;
        colliderAttack.SetDamageType(eDAMAGE_TYPE.POISONING);
        CoroutineManager.Instance.CStartCoroutine(AttackDelay());
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
