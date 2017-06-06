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
                }
                break;
        }
    }

    void Debuff(GameObject _ob)
    {
        if (false == debuffAble)
            return;

        StatusData data = new StatusData(-5, -5, 0, 0, 0, 0, 0, 0);

        BattleEntity agent = _ob.GetComponent<BattleEntity>();
        MessageDispatcher.Instance.DispatchMessage(0, owner.ID, agent.ID, (int)eMESSAGE_TYPE.ADDSTATUS, data);
        debuffAble = false;

        CoroutineManager.Instance.CStartCoroutine(DebuffDelay());
    }

    void DamageAndDebuff(GameObject _ob)
    {
        if (false == damageAndDebuffAble)
            return;

        StatusData data = new StatusData(-2, -2, 0, 0, 0, 0, 0, 0);

        BattleEntity agent = _ob.GetComponent<BattleEntity>();

        MessageDispatcher.Instance.DispatchMessage(0, owner.ID, agent.ID, (int)eMESSAGE_TYPE.ADDSTATUS, data);

        damageAndDebuffAble = false;
        CoroutineManager.Instance.CStartCoroutine(DamageAndDebuffDelay());
    }

    IEnumerator DebuffDelay()
    {
        float oldTime = Time.time;
        while(true)
        {
            if(oldTime + 5.0f < Time.time)
            {
                debuffAble = true;
            }
            yield return null;
        }
    }

    IEnumerator DamageAndDebuffDelay()
    {
        float oldTime = Time.time;
        while (true)
        {
            if (oldTime + 5.0f < Time.time)
            {
                damageAndDebuffAble = true;
            }
            yield return null;
        }
    }
}
