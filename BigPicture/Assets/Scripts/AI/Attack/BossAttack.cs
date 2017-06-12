using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : AttackElement {
    private AI owner;
    bool skillAble = true;
    ColliderAttack colliderAttack;
    public override void Attack(GameObject _go)
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                {
                    DragonBreath(_go);
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

    }

    void DragonBreath(GameObject _go)
    {
        Debug.Log("DragonBreath");
        colliderAttack.SetDamageType(eDAMAGE_TYPE.BLEEDING);
        MessageDispatcher.Instance.DispatchMessage(0, owner.ID, owner.ID, (int)eMESSAGE_TYPE.TO_DRAGONBREATH, null);
    }

    void FootStamp(GameObject _go)
    {
        Debug.Log("FootStamp");
        colliderAttack.SetDamageType(eDAMAGE_TYPE.SHOCK);
        MessageDispatcher.Instance.DispatchMessage(0, owner.ID, owner.ID, (int)eMESSAGE_TYPE.TO_FOOTSTAMP, null);
    }
    public override void Init(AI _onwer, ColliderAttack _colliderAttack, GameObject nomalAttack = null, GameObject _poisoning = null, GameObject _bleeding = null)
    {
        colliderAttack = _colliderAttack;
        owner = _onwer;
    }

}
