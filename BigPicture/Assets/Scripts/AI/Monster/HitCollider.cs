using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

    [SerializeField]
    private BaseGameEntity entity;
    [SerializeField]
    private AI ai;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init(BaseGameEntity _entity , AI _ai)
    {
        entity = _entity;
        ai = _ai;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (null == entity || null == ai)
            return;

        if (eSTATE.HIT == ai.GetCurrentState() || eSTATE.DIE == ai.GetCurrentState() )
        {
            return;
        }

        ColliderAttack ct = other.GetComponent<ColliderAttack>();
        
        if (ct != null && ct.TribeType != entity.Tribe)
        {

            try // 물리공격
            {
                ai.Group.DispatchMessageGroup(0, ai.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, ct.GetComponentInParent<AI>().Group);
            }
            catch // 마법공격
            {
                ai.Group.DispatchMessageGroup(0, ai.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, other.transform.parent.GetComponentInChildren<AI>().Group);

            }


            CEnemy cEnemy;

            cEnemy = ai.EnemyHandle.GetEnemy(ct.GetComponentInParent<BaseGameEntity>().gameObject);

            if (null == cEnemy)
                cEnemy = ai.EnemyHandle.GetEnemy(ct.transform.parent.GetComponentInChildren<BaseGameEntity>().gameObject);



            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_HIT, ct.GetDamageType() );

            //if (ai.GetStatus().EvasionRate <= Random.Range(0, 100))
            //{
            //    //Debug.Log(other.name + "의 공격 회피");
            //    return;
            //}

            //데미지 계산 (물리공격력 + 마법공격력 - 방어력)
            if (0 < (ct.Power - ai.Data.StatusData.Armor))
            {
               ai.Data.StatusData.HP -= ct.Power - ai.Data.StatusData.Armor;
               cEnemy.damage += (ct.Power - ai.Data.StatusData.Armor);
            }
            else
            {
                ai.Data.StatusData.HP -= 1.0f;
                cEnemy.damage += 1.0f;
            }
        }
        
    }
}
