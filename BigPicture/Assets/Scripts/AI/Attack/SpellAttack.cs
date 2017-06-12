using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttack : AttackElement{

    private AI owner;
    private GameObject spell;
    private Collider collider;
    private Color color;
    private float time;

    bool skillAble = true;
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
        switch (Random.Range(0, 5))
        {
            case 0:
                {
                    if(eDAMAGE_TYPE.BLEEDING ==  spell.GetComponent<ColliderAttack>().GetDamageType())
                    {
                        Bleeding(_go);
                    }

                    if (eDAMAGE_TYPE.POISONING == spell.GetComponent<ColliderAttack>().GetDamageType())
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
        spell.GetComponent<EffectHandle>().SetEffect(eEffect.EXPLOSION);
        spell.transform.position = _go.transform.position;
        color.a = 0.0f;
        spell.GetComponent<MeshRenderer>().material.color = color;
        time = Time.time;
        CoroutineManager.Instance.CStartCoroutine(AttackDelay());
    }

    void Bleeding(GameObject _go)
    {
        if (false == skillAble)
            return;

        AI entity = _go.GetComponent<AI>();

        spell.GetComponent<EffectHandle>().SetEffect(eEffect.BLEEDING);
        spell.transform.position = _go.transform.position;
        spell.GetComponent<MeshRenderer>().enabled = true;
        color.a = 0.0f;
        spell.GetComponent<MeshRenderer>().material.color = color;
        time = Time.time;
        CoroutineManager.Instance.CStartCoroutine(AttackDelay());
        CoroutineManager.Instance.CStartCoroutine(BleedingDelay(entity));
    }

    void Poisoning(GameObject _go)
    {
        if (false == skillAble)
            return;

        AI entity = _go.GetComponent<AI>();

        spell.GetComponent<EffectHandle>().SetEffect(eEffect.POISONING);
        spell.transform.position = _go.transform.position;
        spell.GetComponent<MeshRenderer>().enabled = true;
        color.a = 0.0f;
        spell.GetComponent<MeshRenderer>().material.color = color;
        time = Time.time;
        CoroutineManager.Instance.CStartCoroutine(AttackDelay());
        CoroutineManager.Instance.CStartCoroutine(PoisionDelay(entity));
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

    IEnumerator SkillDelay()
    {
        float oldTime = Time.time;
        while (true)
        {
            if (oldTime + 5.0f < Time.time)
            {
                skillAble = true;
                break;
            }
            yield return null;
        }
    }

    IEnumerator BleedingDelay(AI _ai)
    {
        float oldTime = Time.time;
        float endTIme = Time.time;
        _ai.buffUI.AddBuff(eBuff.Bleeding);
        while (true)
        {
            if (endTIme + 5.0f < Time.time)
            {
                break;
            }

            if (oldTime + 1.0f < Time.time)
            {
                oldTime = Time.time;
                _ai.Data.StatusData.HP -= 5;
            }
            yield return null;
        }
        _ai.buffUI.RemoveBuff(eBuff.Bleeding);
        skillAble = true;
    }

    IEnumerator PoisionDelay(AI _ai)
    {
        float oldTime = Time.time;
        float endTIme = Time.time;
        _ai.AddStatus = new StatusData(0, 0, -5, 0, 0, 0, 0, 0);
        _ai.buffUI.AddBuff(eBuff.SpeedDown);
        _ai.buffUI.AddBuff(eBuff.Poisoning);
        while (true)
        {
            if (endTIme + 5.0f < Time.time)
            {
                break;
            }

            if (oldTime + 1.0f < Time.time)
            {
                oldTime = Time.time;
                _ai.Data.StatusData.HP -= 3;
            }
            yield return null;
        }
        skillAble = true;
        _ai.AddStatus = new StatusData(0, 0, 0, 0, 0, 0, 0, 0);
        _ai.buffUI.RemoveBuff(eBuff.SpeedDown);
        _ai.buffUI.RemoveBuff(eBuff.Poisoning);
    }
}
