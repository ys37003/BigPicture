using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsAttack : AttackElement {

    bool debuffAble = true;
    bool damageAndDebuffAble = true;
    ColliderAttack colliderAttack;
    private AI owner;
    public override void Init(AI _onwer, ColliderAttack _colliderAttack, GameObject nomalAttack = null, GameObject _poisoning = null, GameObject _bleeding = null)
    {
        colliderAttack = _colliderAttack;
        owner = _onwer;
    }

    public override void Attack(GameObject _ob)
    {

        switch (Random.Range(0, 5))
        {
            case 0:
                {
                    Debuff();
                }
                break;

            case 1:
                {
                    DamageAndDebuff();
                }
                break;
            case 2:
                {
                    Shock();
                }
                break;
            default:
                {
                    colliderAttack.SetDamageType(eDAMAGE_TYPE.PHYSICS);
                    owner.GetComponentInChildren<EffectHandle>().SetEffect(eEffect.PUNCH);
                }
                break;
        }
    }

    void Debuff()
    {
        owner.GetComponentInChildren<EffectHandle>().SetEffect(eEffect.SLASH);

        colliderAttack.SetDamageType(eDAMAGE_TYPE.DEBUFF);
    }

    void DamageAndDebuff()
    {
        owner.GetComponentInChildren<EffectHandle>().SetEffect(eEffect.SLASH);

        colliderAttack.SetDamageType(eDAMAGE_TYPE.DAMAGE_AND_DEBUFF);
    }

    void Shock()
    {
        owner.GetComponentInChildren<EffectHandle>().SetEffect(eEffect.SLASH);
        colliderAttack.SetDamageType(eDAMAGE_TYPE.SHOCK);
    }
}
