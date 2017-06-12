using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsAttack : AttackElement {

    bool debuffAble = true;
    bool damageAndDebuffAble = true;
    private AI owner;
    public override void Init(AI _onwer , GameObject _go = null)
    {
        owner = _onwer;
    }

    public override void Attack(GameObject _ob)
    {

        switch (Random.Range(0, 5))
        {
            case 0:
                {
                    Debuff(_ob);
                }
                break;

            case 1:
                {
                    DamageAndDebuff(_ob);
                }
                break;

            default:
                {
                    owner.GetComponentInChildren<EffectHandle>().SetEffect(eEffect.PUNCH);
                }
                break;
        }
    }

    void Debuff(GameObject _go)
    {
        if (false == debuffAble)
            return;

        AI entity = _go.GetComponent<AI>();

        Debug.Log("Debuff");

        owner.GetComponentInChildren<EffectHandle>().SetEffect(eEffect.SLASH);
        StatusData data = new StatusData(-5, 0, -5, 0, 0, 0, 0, 0);
        entity.buffUI.AddBuff(eBuff.PowerDown);
        entity.buffUI.AddBuff(eBuff.SpeedDown);
        BattleEntity agent = _go.GetComponent<BattleEntity>();
        debuffAble = false;

        CoroutineManager.Instance.CStartCoroutine(DebuffDelay(entity));
    }

    void DamageAndDebuff(GameObject _go)
    {
        if (false == damageAndDebuffAble)
            return;

        AI entity = _go.GetComponent<AI>();

        owner.GetComponentInChildren<EffectHandle>().SetEffect(eEffect.SLASH);
        StatusData data = new StatusData(-2, 0, -2, 0, 0, 0, 0, 0);
        entity.buffUI.AddBuff(eBuff.PowerDown);
        entity.buffUI.AddBuff(eBuff.SpeedDown);
        BattleEntity agent = _go.GetComponent<BattleEntity>();

        damageAndDebuffAble = false;
        CoroutineManager.Instance.CStartCoroutine(DamageAndDebuffDelay(entity));
    }

    IEnumerator DebuffDelay(AI _ai)
    {
        float oldTime = Time.time;
        while(true)
        {
            if(oldTime + 2.0f < Time.time)
            {
                debuffAble = true;
                _ai.buffUI.RemoveBuff(eBuff.PowerDown);
                _ai.buffUI.RemoveBuff(eBuff.SpeedDown);
                break;
            }
            yield return null;
        }
    }

    IEnumerator DamageAndDebuffDelay(AI _ai)
    {
        float oldTime = Time.time;
        while (true)
        {
            if (oldTime + 2.0f < Time.time)
            {
                damageAndDebuffAble = true;
                _ai.buffUI.RemoveBuff(eBuff.PowerDown);
                _ai.buffUI.RemoveBuff(eBuff.SpeedDown);
                break;
            }
            yield return null;
        }
    }
}
