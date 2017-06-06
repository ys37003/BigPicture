using System.Collections;
using UnityEngine;

public class Character : BattleEntity, ICharacter
{
    /// <summary>
    /// 기본 능력치
    /// </summary>
    public StatusData Status { get; set; }

    public int SkillPoint { get; set; }

    private eDAMAGE_TYPE damageType = eDAMAGE_TYPE.PHYSICS;
    public  eDAMAGE_TYPE DamageType
    {
                get { return damageType; }
        private set { damageType = value; }
    }

    /// <summary>
    /// 캐릭터의 현재 상태
    /// </summary>
    public eSTATE CurrentState { get; private set; }

    private Group group;
    public  Group Group
    {
                get { return group; }
        private set { group = value; }
    }

    /// <summary>
    /// 공격한 몬스터를 가져온다. 없을 수도 있음
    /// </summary>
    public GameObject Target { get { return colliderAttack == null ? colliderAttack.Target : null; } }

    [SerializeField] private Animator       animator       = null;
    [SerializeField] private ColliderAttack colliderAttack = null;

    /// <summary>
    /// 구르기 속도는 이동속도(MoveSpeed) %이다.
    /// ex) RollSpeed가 0.7이면 7(MoveSpeed * 0.7)의 속도로 구른다.
    /// </summary>
    [SerializeField]
    private float rollSpeedRate = 0.7f;

    private void Awake()
    {
        Group = GetComponentInParent<Group>();
        if (Group != null)
        {
            Group.Add(this);
        }
        else
        {
            Debug.Log("그룹이 없습니다.");
        }

        EntityInit(eENTITY_TYPE.PLAYER, eTRIBE_TYPE.HUMAN, eJOB_TYPE.DEALER , Group);

        // 임시
        Init(new StatusData(1, 0, 1, 1, 1, 1, 1, StatusData.MAX_HP));

        StartCoroutine("UpdateState");

        colliderAttack.Init(eTRIBE_TYPE.HUMAN, animator, Status , AddStatus , eDAMAGE_TYPE.PHYSICS);
        foreach (AnimationTrigger trigger in animator.GetBehaviours<AnimationTrigger>())
        {
            trigger.ColliderAttack = colliderAttack;
        }

        TeamManager.Instance.AddCharacter(this);
    }

    public void Init(StatusData status)
    {
        Status = status;
        AddStatus = new StatusData(0, 0, 0, 0, 0, 0, 0,0);
        SkillPoint = 10;
    }

    public void Move(Vector3 dir, float move)
    {
        float   turn      = Mathf.Atan2(dir.x, dir.z);
        float   turnSpeed = Mathf.Lerp(180, 360, dir.z);

        transform.Rotate(0, turn * Time.deltaTime * turnSpeed, 0);
        transform.Translate(dir * Time.deltaTime * Status.MoveSpeed * move);
    }

    public void Roll(Vector3 eulerAngles)
    {
        transform.eulerAngles = eulerAngles;
        transform.Translate(Vector3.forward * Time.deltaTime * Status.MoveSpeed * rollSpeedRate);
    }

    public void Battle(bool battle)
    {
        colliderAttack.SetActive(battle);

        if (battle)
        {
            StopCoroutine("ReoveryRatePerSecond");
        }
        else
        {
            StartCoroutine("ReoveryRatePerSecond");
        }
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
                    CurrentState = eSTATE.IDLE;
                }
                else if (m < 1.4)
                {
                    CurrentState = eSTATE.WALK;
                }
                else
                {
                    CurrentState = eSTATE.RUN;
                }
            }
            else if (info.IsTag("Attack"))
            {
                CurrentState = eSTATE.ATTACK;
            }
            else if(info.IsTag("Roll"))
            {
                CurrentState = eSTATE.ROLLING;
            }
            else if(info.IsTag("BattleIdle"))
            {
                CurrentState = eSTATE.BATTLEIDLE;
            }
            else if(info.IsTag("Dead"))
            {
                CurrentState = eSTATE.DIE;
            }
            else
            {
                CurrentState = eSTATE.NULL;
            }

            yield return null;
        }
    }

    /// <summary>
    /// 초당 HP 회복
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReoveryRatePerSecond()
    {
        float sTime = Time.time;
        int second = 1;

        while(true)
        {
            //if (sTime + second <= Time.time)
            //{
            //    Status.HP += Status.RecoveryRPS;
            //    sTime = Time.time;
            //}

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ColliderAttack ct = other.GetComponent<ColliderAttack>();

        if (ct != null && ct.TribeType != Tribe)
        {
            try // 물리공격
            {
                Group.DispatchMessageGroup(0, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, ct.GetComponentInParent<BattleEntity>().EntityGroup);
            }
            catch // 마법공격
            {
                Group.DispatchMessageGroup(0, this.ID, (int)eMESSAGE_TYPE.FIND_ENEMY, other.transform.parent.GetComponentInChildren<BattleEntity>().EntityGroup);
                
            }
            if (ct.StatusData.EvasionRate <= Random.Range(0, 100))
            {
                //Debug.Log(string.Format("{0}의 공격 회피", other.name));
            }

            //데미지 계산 (물리공격력 + 마법공격력 - 방어력)
            Status.HP -= (ct.Power - Status.Armor);
        }
    }

    public override void HanleMessage(Telegram _msg)
    {
        switch (_msg.message)
        {
            case (int)eMESSAGE_TYPE.ADDSTATUS:
                AddStatus = (StatusData)_msg.extraInfo;
                MessageDispatcher.Instance.DispatchMessage(2.0f, this.ID, this.ID, (int)eMESSAGE_TYPE.ADDSTATUS, new StatusData(0, 0, 0, 0, 0, 0, 0, 0));
                return;
        }
    }
}