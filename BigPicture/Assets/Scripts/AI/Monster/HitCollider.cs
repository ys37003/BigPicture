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
        if (eSTATE.HIT == ai.GetCurrentState() || eSTATE.DIE == ai.GetCurrentState())
            return;

        ColliderAttack ct = other.GetComponent<ColliderAttack>();
        
        if (ct != null && ct.TribeType != entity.Tribe)
        {
            if (false == ai.EnemyCheck())
            {
                ai.Group.EnemyGroup = other.GetComponentInParent<AI>().Group;
                ai.SetEnemy(ct.GetComponentInParent<BaseGameEntity>().gameObject);
                ai.Group.DispatchMessageGroup(0, ai.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, ai.Group.EnemyGroup );
            }

            MessageDispatcher.Instance.DispatchMessage(0, entity.ID, entity.ID, (int)eMESSAGE_TYPE.TO_HIT, null);
            //데미지 계산 (물리공격력 + 마법공격력 - 방어력)
            ai.Data.StatusData.HP -= ct.Power - ai.Data.StatusData.Armor;

            if (ai.GetStatus().EvasionRate <= Random.Range(0, 100))
            {
                Debug.Log(other.name + "의 공격 회피");
            }
            //else
            //{
            //    MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.TO_HIT, null);
            //    //데미지 계산 (물리공격력 + 마법공격력 - 방어력)
            //    this.Data.StatusData.HP -= (ct.Power - this.GetTotalStatus().Armor);
            //    //this.Data.StatusData.HP -= (ct.Power);
            //    //this.Data.StatusData.HP -= 50.0f;
            //    if (true == this.Die())
            //        MessageDispatcher.Instance.DispatchMessage(0, this.ID, this.ID, (int)eMESSAGE_TYPE.TO_DIE, null);

            //    Debug.Log("Hit");
            //}
        }

    }
}
