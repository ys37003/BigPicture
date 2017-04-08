using System.Collections;
using UnityEngine;

public class ColliderAttack : MonoBehaviour
{
    public eENTITY_TYPE         EntitiType  { get; private set; }
    public StatusData           StatusData  { get; private set; }

    private Animator            animator;
    private AnimatorStateInfo   stateInfo;

    [SerializeField]
    private Collider coll = null;

    public float Power { get { return StatusData.PhysicsPower + StatusData.SpellPower; } }

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

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Init(eENTITY_TYPE type, Animator animator, StatusData stat)
    {
        EntitiType    = type;
        this.animator = animator;
        StatusData    = stat;
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
}