using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttack : AttackElement{

    private AI owner;
    private GameObject nomalAttack;
    private GameObject poisoning;
    private GameObject bleeding;
    private Collider collider;
    private Color color;
    private float time;
    ColliderAttack colliderAttack;
    // Use this for initializatio
    // poision == skill1
    // bleeding == skill2
    public override void Init(AI _onwer, ColliderAttack _colliderAttack, GameObject _nomalAttack = null, GameObject _skill1 = null, GameObject _skill2 = null, GameObject _skill3 = null)
    {
        colliderAttack = _colliderAttack;
        nomalAttack = _nomalAttack;
        poisoning = _skill1;
        bleeding = _skill2;
        collider = nomalAttack.GetComponent<BoxCollider>();

        if (null != poisoning)
        {
            poisoning.SetActive(false);
        }
        if (null != bleeding)
        {
            bleeding.SetActive(false);
        }
        collider.enabled = false;
        owner = _onwer;
    }

    public override void Attack(GameObject _go)
    {
        switch (Random.Range(0, 3))
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
                    NomalAttack(_go);
                }
                break;
        }
    }

    void NomalAttack(GameObject _go)
    {
        CoroutineManager.Instance.CStartCoroutine(EffectDeley(2.0f, _go));
    }

    void Bleeding(GameObject _go)
    {
        bleeding.transform.position = _go.transform.position;
        colliderAttack.SetDamageType(eDAMAGE_TYPE.BLEEDING);
        time = Time.time;
        CoroutineManager.Instance.CStartCoroutine(AttackDelay(bleeding,1.5f));
    }

    void Poisoning(GameObject _go)
    {
        //if (false == skillAble)
        //    return;
        MessageDispatcher.Instance.DispatchMessage(0.5f, owner.ID, owner.EnemyHandle.GetEnemy(0).enemy.GetComponent<BaseGameEntity>().ID, (int)eMESSAGE_TYPE.AVOID_ATTACK, owner.transform.position);
        poisoning.transform.position = _go.transform.position;
        colliderAttack.SetDamageType(eDAMAGE_TYPE.POISONING);
        poisoning.SetActive(true);
        time = Time.time;
        CoroutineManager.Instance.CStartCoroutine(AttackDelay(poisoning,0));
    }

    IEnumerator EffectDeley(float _delay,  GameObject _go)
    {
        float oldTime = Time.time;
        while (true)
        {
            if (oldTime + _delay < Time.time)
            {
                nomalAttack.GetComponent<EffectHandle>().SetEffect(eEffect.EXPLOSION);
                nomalAttack.transform.position = _go.transform.position;

                nomalAttack.GetComponent<EffectHandle>().ActEffect();

                colliderAttack.SetDamageType(eDAMAGE_TYPE.SPELL);
                try
                {
                    _go.GetComponentInChildren<HitCollider>().GetDamage(colliderAttack);
                }
                catch
                {
                    _go.GetComponent<Character>().GetDamage(colliderAttack);
                }
                break;
            }
            yield return null;
        }
    }

    IEnumerator AttackDelay(GameObject _go,float _delay)
    {
        float oldTime = Time.time;

        while (true)
        {
            if(oldTime + _delay < Time.time)
            {
                _go.SetActive(true);
            }

            if (time + (2.0f + _delay) < Time.time)
            {
                _go.SetActive(false);
                break;
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
