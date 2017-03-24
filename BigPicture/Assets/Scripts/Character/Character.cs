using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    /// <summary>
    /// 기본 능력치
    /// </summary>
    public StatusData Status { get; private set; }

    /// <summary>
    /// 추가 능력치(버프, 디버프)
    /// </summary>
    public StatusData AddStatus { get; set; }

    /// <summary>
    /// 전체 능력치
    /// </summary>
    public StatusData TotalStatus { get { return Status + AddStatus; } }

    public int StatusPoint { get; set; }

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        animator.SetFloat("Forward", v);
        animator.SetFloat("Strafe", h);
    }

    private IEnumerator Idle()
    {
        yield return null;
    }

    private IEnumerator Walk()
    {
        yield return null;
    }

    private IEnumerator Run()
    {
        yield return null;
    }

    private IEnumerator Battle()
    {
        yield return null;
    }

    private IEnumerator DIe()
    {
        yield return null;
    }
}