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


        ColliderAttack ct = other.GetComponent<ColliderAttack>();
        if (null == ct)
        {
            return;
        }

        if (ct != null && ct.TribeType != entity.Tribe)
        {
            GetDamage(ct);
        }
    }

    void DamageTypeHandle(ColliderAttack _colliderAttack)
    {
        Debug.Log(_colliderAttack.GetDamageType());
        switch(_colliderAttack.GetDamageType())
        {
            case eDAMAGE_TYPE.PHYSICS:
                Vector3 direction = ai.transform.position - (ai.transform.forward / 1.5f);
                ai.SetTarget(direction);
                break;
            case eDAMAGE_TYPE.BLEEDING:
                CoroutineManager.Instance.CStartCoroutine(ConditionManager.Instance.BleedingDelay(ai));
                break;
            case eDAMAGE_TYPE.POISONING:
                CoroutineManager.Instance.CStartCoroutine(ConditionManager.Instance.PoisionDelay(ai));
                break;
            case eDAMAGE_TYPE.DEBUFF:
                CoroutineManager.Instance.CStartCoroutine(ConditionManager.Instance.DebuffDelay(ai));
                break;
            case eDAMAGE_TYPE.DAMAGE_AND_DEBUFF:
                CoroutineManager.Instance.CStartCoroutine(ConditionManager.Instance.DamageAndDebuffDelay(ai));
                break;
            case eDAMAGE_TYPE.SHOCK:
                MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_SHOCK, null );
                break;
        }
    }

    public void GetDamage(ColliderAttack ct)
    {
        if (eSTATE.HIT == ai.GetCurrentState() || eSTATE.DIE == ai.GetCurrentState() || eSTATE.SHOCK == ai.GetCurrentState())// || eSTATE.ATTACK != ct.GetComponentInParent<AI>().GetCurrentState())
        {
            return;
        }

        ai.EntityGroup.DispatchMessageGroup(0, ai.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, ct.owner.EntityGroup);

        CEnemy cEnemy;

        cEnemy = ai.EnemyHandle.GetEnemy(ct.GetComponentInParent<BaseGameEntity>().gameObject);

        if (null == cEnemy)
            cEnemy = ai.EnemyHandle.GetEnemy(ct.transform.parent.GetComponentInChildren<BaseGameEntity>().gameObject);

        MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_HIT, ct.GetDamageType());

        DamageTypeHandle(ct);

        if (0 < (ct.Power - (ai.Data.StatusData.Armor + ai.AddStatus.Armor)))
        {
            ai.Data.StatusData.HP -= ct.Power - (ai.Data.StatusData.Armor + ai.AddStatus.Armor);
            cEnemy.damage += (ct.Power - (ai.Data.StatusData.Armor + ai.AddStatus.Armor));
        }
        else
        {
            ai.Data.StatusData.HP -= 1.0f;
            cEnemy.damage += 1.0f;
        }
    }
}
