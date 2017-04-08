using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : BaseGameEntity
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

    /// <summary>
    /// 구르기 속도는 이동속도(MoveSpeed) %이다.
    /// ex) RollSpeed가 0.7이면 7(MoveSpeed * 0.7)의 속도로 구른다.
    /// </summary>
    public float RollSpeedRate = 0.7f;

    public eSTATE currentState { get; private set; }

    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    private void Awake()
    {
        EntityInit(eTYPE.PLAYER, eTRIBE_TYPE.NULL, eJOB_TYPE.DEALER);

        // 임시
        Init(new StatusData(1, 1, 1, 1, 1, 1, 1));

        StartCoroutine("UpdateState");

        colliderAttack.Init(eTYPE.PLAYER, animator, Status);
        foreach (AnimationTrigger trigger in animator.GetBehaviours<AnimationTrigger>())
        {
            trigger.ColliderAttack = colliderAttack;
        }
    }

    public void Init(StatusData status)
    {
        Status = status;
        AddStatus = new StatusData(0, 0, 0, 0, 0, 0, 0);
    }

    public void Move(Vector3 dir, float move)
    {
        float   turn      = Mathf.Atan2(dir.x, dir.z);
        float   turnSpeed = Mathf.Lerp(180, 360, dir.z);

        transform.Rotate(0, turn * Time.deltaTime * turnSpeed, 0);
        transform.Translate(dir * Time.deltaTime * TotalStatus.MoveSpeed * move);
    }

    public void Roll(Vector3 eulerAngles)
    {
        transform.eulerAngles = eulerAngles;
        transform.Translate(Vector3.forward * Time.deltaTime * TotalStatus.MoveSpeed * RollSpeedRate);
    }

    public void Battle(bool battle)
    {
        colliderAttack.SetActive(battle);
    }

    /// <summary>
    /// 현재 상태 갱신
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateState()
    {
        while(true)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.IsTag("Idle"))
            {
                float m = animator.GetFloat("Move");
                if (m < 0.7)
                {
                    currentState = eSTATE.IDLE;
                }
                else if (m < 1.4)
                {
                    currentState = eSTATE.WALK;
                }
                else
                {
                    currentState = eSTATE.RUN;
                }
            }
            else if (info.IsTag("Attack"))
            {
                currentState = eSTATE.ATTACK;
            }
            else if(info.IsTag("Roll"))
            {
                currentState = eSTATE.ROLLING;
            }
            else if(info.IsTag("BattleIdle"))
            {
                currentState = eSTATE.BATTLEIDLE;
            }
            else if(info.IsTag("Dead"))
            {
                currentState = eSTATE.DEAD;
            }
            else
            {
                currentState = eSTATE.NULL;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ColliderAttack ct = other.GetComponent<ColliderAttack>();

        if(ct != null && ct.EntitiType == eTYPE.MONSTER)
        {
            //데미지 계산 (물리공격력 + 마법공격력 - 방어력)
            Status.HP -= (ct.Power - TotalStatus.Armor);
        }
    }
}