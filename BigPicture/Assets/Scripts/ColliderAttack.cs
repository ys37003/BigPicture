using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAttack : MonoBehaviour
{
    public eTYPE                EType       { get; private set; }
    public Animator             Animator    { get; private set; }
    public AnimatorStateInfo    StateInfo   { get; private set; }
    public StatusData           StatusData  { get; private set; }

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

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Init(eTYPE type, Animator animator, StatusData stat)
    {
        EType      = type;
        Animator   = animator;
        StatusData = stat;
    }

    public void AttackStart(AnimatorStateInfo info)
    {
        StateInfo = info;
        SetCollider(true);
        StopCoroutine("Attack");
        StartCoroutine("Attack");
    }

    public int GetDamage()
    {
        return StatusData.Strength;
    }

    private IEnumerator Attack()
    {
        while (Animator.GetNextAnimatorStateInfo(0).fullPathHash == StateInfo.fullPathHash)
            yield return null;

        while (Animator.GetCurrentAnimatorStateInfo(0).fullPathHash == StateInfo.fullPathHash)
            yield return null;

        SetCollider(false);
    }
}