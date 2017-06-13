using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : AttackElement {
    private Dragon owner;
    bool skillAble = true;
    ColliderAttack colliderAttack;
    private float time;

    private GameObject Breath;
    private GameObject Stamp;
    private GameObject Sumon;

    public override void Attack(GameObject _go)
    {
        switch (Random.Range(0, 5))
        {
            case 0:
                {
                    DragonBreath(_go);
                }
                break;
            case 1:
                {
                    
                }
                break;
            case 2:
                {
                    SummonMonster(_go);
                }
                break;

            default:
                {
                    FootStamp(_go);
                }
                break;

        }
    }

    void SummonMonster(GameObject _go)
    {
        if (false == skillAble)
            return;

        time = Time.time;
        skillAble = false;
        if (0 < owner.sumonList.groupList.Count)
        {
            Sumon.transform.position = owner.sumonList.groupList[0].transform.position;
            owner.sumonList.groupList[0].SetActive(true);
            owner.sumonList.groupList[0].transform.parent = null;
            owner.sumonList.groupList.RemoveAt(0);
            CoroutineManager.Instance.CStartCoroutine(AttackDelay(Sumon, 0f));
            MessageDispatcher.Instance.DispatchMessage(0, owner.ID, owner.ID, (int)eMESSAGE_TYPE.TO_SUMONMONSTER, null);

        }
    }

    void DragonBreath(GameObject _go)
    {
        if (false == skillAble)
            return;
        colliderAttack.SetDamageType(eDAMAGE_TYPE.BLEEDING);
        skillAble = false;
        CoroutineManager.Instance.CStartCoroutine(AttackDelay(Breath, 2.5f));
        MessageDispatcher.Instance.DispatchMessage(0, owner.ID, owner.ID, (int)eMESSAGE_TYPE.TO_DRAGONBREATH, null);
    }

    void FootStamp(GameObject _go)
    {
        if (false == skillAble)
            return;
        colliderAttack.SetDamageType(eDAMAGE_TYPE.SHOCK);
        skillAble = false;
        CoroutineManager.Instance.CStartCoroutine(AttackDelay(Stamp, 1.0f));
        MessageDispatcher.Instance.DispatchMessage(0, owner.ID, owner.ID, (int)eMESSAGE_TYPE.TO_FOOTSTAMP, null);
    }

    // Breat == skill1
    // Stamp == skill2
    // sumon == skill3
    public override void Init(AI _onwer, ColliderAttack _colliderAttack, GameObject _nomalAttack = null, GameObject _skill1 = null, GameObject _skill2 = null, GameObject _skill3 = null)
    {
        colliderAttack = _colliderAttack;
        owner = _onwer.GetComponent<Dragon>();
        Breath = _skill1;
        Stamp = _skill2;
        Sumon = _skill3;

        if (null != Breath)
        {
            Breath.SetActive(false);
        }
        if (null != Stamp)
        {
            Stamp.SetActive(false);
        }

        if (null != Sumon)
        {
            Sumon.SetActive(false);
        }
    }

    IEnumerator AttackDelay(GameObject _go, float _delay)
    {
        float oldTime = Time.time;
        float endTime = Time.time;
        while (true)
        {
            if (oldTime + _delay < Time.time)
            {
                _go.SetActive(true);
            }

            if (endTime + (2.0f + _delay) < Time.time)
            {
                _go.SetActive(false);
                skillAble = true;
                CoroutineManager.Instance.CStartCoroutine(SkillDelay());
                break;
            }
            yield return null;
        }
    }


    IEnumerator SkillDelay()
    {
        time = Time.time;
        while (true)
        {
            if (time + 5.0f < Time.time)
            {
                skillAble = true;
                break;
            }
            yield return null;
        }
    }
}
