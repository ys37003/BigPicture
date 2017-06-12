using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : AttackElement {
    private AI owner;
    bool skillAble = true;

    public override void Attack(GameObject _pos)
    {
        switch (Random.Range(0, 5))
        {
            case 0:
                {
                   

                }
                break;

            default:
                {
                }
                break;
        }
    }

    void SummonMonster(GameObject _go)
    {

    }

    void DragonBreath(GameObject _go)
    {

    }

    void FootStamp(GameObject _go)
    {

    }
    public override void Init(AI _onwer, GameObject _go = null)
    {
        owner = _onwer;
    }

}
