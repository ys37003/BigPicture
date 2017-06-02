using System.Collections;
using UnityEngine;

public class ColliderAttack : MonoBehaviour
{
    public eTRIBE_TYPE          TribeType    { get; private set; }
    public StatusData           StatusData   { get; private set; }

    private Animator            animator;
    private AnimatorStateInfo   stateInfo;
    private eDAMAGE_TYPE damageType;
    public float Power { get { return StatusData.PhysicsPower + StatusData.SpellPower; } }

    private GameObject target;
    public  GameObject Target
    {
                get { return target; }
        private set { target = value; }
    }

    [SerializeField]
    private Collider coll = null;

    private void Awake()
    {
        if (coll == null)
            coll = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        SetCollider(false);
    }

    public void SetCollider(bool enabled)
    {
        coll.enabled = enabled;
    }

    public eDAMAGE_TYPE GetDamageType()
    {
        return damageType;
    }
    

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Init(eTRIBE_TYPE type, Animator animator, StatusData stat , eDAMAGE_TYPE _damageType)
    {
        TribeType     = type;
        this.animator = animator;
        StatusData    = stat;
        damageType = _damageType;
    }

    public void AttackStart(AnimatorStateInfo info)
    {
        stateInfo = info;
        StopCoroutine("Attack");
        StartCoroutine("Attack");
    }

    /// <summary>
    /// 공격시 콜라이더를 켜줌
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        SetCollider(false);
        yield return null;
        SetCollider(true);

        while (animator.GetNextAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash)
            yield return null;

        while (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash)
            yield return null;

        SetCollider(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == this.tag)
        {
            Target = other.gameObject;
        }
    }
}