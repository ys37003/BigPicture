using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAttack : MonoBehaviour
{
    public eTYPE        EType       { get; private set; }
    public Animator     Animator    { get; private set; }
    public StatusData   StatusData  { get; private set; }

    [SerializeField]
    private Collider coll = null;

    private void Awake()
    {
        if (coll == null)
            coll = GetComponent<Collider>();
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
}