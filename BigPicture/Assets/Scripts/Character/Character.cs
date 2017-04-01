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

    public float MoveSpeed = 10;

    /// <summary>
    /// 구르기 속도는 이동속도(MoveSpeed) %이다.
    /// ex) RollSpeed가 0.7이면 7(MoveSpeed * 0.7)의 속도로 구른다.
    /// </summary>
    public float RollSpeedRate = 0.7f;
    public float CameraTurnSpeed = 30;
    public float WaitTime = 5.0f;

    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private Transform followedCamera = null;

    [SerializeField]
    private ColliderAttack colliderAttack = null;

    private bool isRun = false;

    public eTYPE currentType { get; private set; }

    private void Awake()
    {
        EntityInit(eTYPE.PLAYER, eTRIBE_TYPE.Ork, eJOB_TYPE.DEALER);

        // 임시
        Init(new StatusData(1, 1, 1, 1, 1, 1, 1));

        StartCoroutine("Move");
        StartCoroutine("RunCheck");
        StartCoroutine("Battle");
        StartCoroutine("Attack");
        StartCoroutine("CameraRotation");

        foreach (AnimationTrigger trigger in animator.GetBehaviours<AnimationTrigger>())
        {
            trigger.ColliderAttack = colliderAttack;
        }
    }

    public void Init(StatusData status)
    {
        Status = status;
    }

    private IEnumerator Move()
    {
        float h = 0;
        float v = 0;
        float move = 0;

        Vector3 forward = Vector3.Scale(followedCamera.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = followedCamera.right;
        Vector3 dir = Vector3.zero;

        float turn = 0;
        float turnSpeed = 0;

        while (true)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            // 이동 입력이 있으면 1, 달리기(쉬프트) 입력이 있으면 x2
            move = Mathf.Abs(h) > Mathf.Abs(v) ? Mathf.Abs(h) : Mathf.Abs(v);
            move += isRun ? move : 0;

            animator.SetFloat("Move", move);

            if(!IsMove())
            {
                // 카메라의 정면을 기준으로 캐릭터 방향 설정
                forward = Vector3.Scale(followedCamera.forward, new Vector3(1, 0, 1)).normalized;
                right = followedCamera.right;
            }
            else
            {
                dir = transform.InverseTransformDirection(v * forward + h * right);
                turn = Mathf.Atan2(dir.x, dir.z);
                turnSpeed = Mathf.Lerp(180, 360, dir.z);

                if(!IsAttack())
                {
                    transform.Rotate(0, turn * Time.deltaTime * turnSpeed, 0);
                }

                transform.Translate(dir * Time.deltaTime * MoveSpeed * move);
            }

            // 회피(C)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yield return Roll();
            }

            yield return null;
        }
    }

    private IEnumerator RunCheck()
    {
        float[] beforeInputTime = new float[4];
        Action<KeyCode, int> inputCheck = (KeyCode key, int i) =>
        {
            // 방향키를 두번 연속 누를때 달리기 상태로 전환
            if (Input.GetKeyDown(key))
            {
                if (beforeInputTime[i] == 0)
                {
                    beforeInputTime[i] = Time.time;
                }
                else if (Time.time - beforeInputTime[i] < 0.2f)
                {
                    isRun = true;
                }
                else
                {
                    beforeInputTime[i] = Time.time;
                }
            }
        };

        while (true)
        {
            inputCheck(KeyCode.W, 0);
            inputCheck(KeyCode.A, 1);
            inputCheck(KeyCode.S, 2);
            inputCheck(KeyCode.D, 3);

            if (isRun)
            {
                if (!IsMove())
                {
                    isRun = false;
                    for (int i = 0; i < 4; ++i)
                    {
                        beforeInputTime[i] = 0;
                    }
                }
            }

            yield return null;
        }
    }

    private IEnumerator Roll()
    {
        animator.SetBool("Roll", true);

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
            yield return null;

        Vector3 vec = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            vec += Vector3.forward;
        }

        if(Input.GetKey(KeyCode.S))
        {
            vec += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            vec += Vector3.left;
        }

        if(Input.GetKey(KeyCode.D))
        {
            vec += Vector3.right;
        }

        if (vec != Vector3.zero)
        {
            float turn = Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg;

            // 카메라 정면을 기준으로 입력한 방향으로 즉시 회전
            transform.eulerAngles = followedCamera.eulerAngles + Vector3.up * turn;
        }

        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed * RollSpeedRate);
            yield return null;
        }

        animator.SetBool("Roll", false);
        yield return Move();
    }

    private IEnumerator Battle()
    {
        float bTime = Time.time;

        while (true)
        {
            // Tab으로 배틀상태 전환
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                animator.SetBool("Battle", !animator.GetBool("Battle"));
            }

            // 키를 입력하면 대기시간 초기화
            if(Input.anyKey)
            {
                bTime = Time.time;
            }

            // 대기시간이 5초를 넘으면 배틀상태 종료
            if(Time.time - bTime >= WaitTime)
            {
                animator.SetBool("Battle", false);
            }

            yield return null;
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            // 공격(마우스 좌클릭)
            bool mouse_left = Input.GetMouseButton(0);
            animator.SetBool("Attack", mouse_left);

            yield return null;
        }
    }

    private IEnumerator CameraRotation()
    {
        while (true)
        {
            // 마우스 오른쪽 클릭 후 드래그로 화면 회전
            if (Input.GetMouseButton(1))
            {
                followedCamera.RotateAround(followedCamera.position, Vector3.down, Input.GetAxis("Mouse Y") * CameraTurnSpeed);
            }
            yield return null;
        }
    }

    /// <summary>
    /// WASD 중 하나라도 누르고 있다면 참을 반환
    /// </summary>
    /// <returns></returns>
    private bool IsMove()
    {
        return Input.GetKey(KeyCode.W) ||
               Input.GetKey(KeyCode.A) ||
               Input.GetKey(KeyCode.S) ||
               Input.GetKey(KeyCode.D);
    }

    /// <summary>
    /// 공격 중 일때 참을 반환
    /// </summary>
    /// <returns></returns>
    private bool IsAttack()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            Debug.Log("공격당했당.");
        }
    }
}