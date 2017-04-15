using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private Character  character       = null;
    [SerializeField] private Animator   animator        = null;
    [SerializeField] private Transform  followedCamera  = null;
    [SerializeField] private float      waitTime        = 5.0f;
    [SerializeField] private float      cameraTurnSpeed = 30;

    public bool IsMove
    {
        get
        {
            return Input.GetKey(KeyCode.W) ||
                   Input.GetKey(KeyCode.A) ||
                   Input.GetKey(KeyCode.S) ||
                   Input.GetKey(KeyCode.D);
        }
    }

    public bool IsRun { get; private set; }

    public bool IsRoll { get { return animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"); } }

    public bool IsAttack { get { return animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"); } }

    private void Awake()
    {
        UIBase.Create<StatusUI>();
        StartCoroutine("Move");
        StartCoroutine("Run");
        StartCoroutine("Battle");
        StartCoroutine("Attack");
        StartCoroutine("CameraRotation");
    }

    private IEnumerator Move()
    {
        float h = 0;
        float v = 0;
        float move = 0;

        Vector3 forward = Vector3.Scale(followedCamera.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right   = followedCamera.right;

        while (true)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            // 이동 입력이 있으면 1, 달리기(쉬프트) 입력이 있으면 x2
            move = Mathf.Abs(h) > Mathf.Abs(v) ? Mathf.Abs(h) : Mathf.Abs(v);
            move += IsRun ? move : 0;

            animator.SetFloat("Move", move);

            if (!IsMove)
            {
                // 카메라의 정면을 기준으로 캐릭터 방향 설정
                forward = Vector3.Scale(followedCamera.forward, new Vector3(1, 0, 1)).normalized;
                right = followedCamera.right;
            }
            else if(!IsAttack)
            {
                // 카메라 정면을 기준으로 입력한 방향으로 이동
                character.Move(transform.InverseTransformDirection(v * forward + h * right), move);
            }

            // 회피(Spcae)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yield return Roll();
            }

            yield return null;
        }
    }

    private IEnumerator Run()
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
                    IsRun = true;
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

            if (IsRun)
            {
                if (!IsMove)
                {
                    IsRun = false;
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
        if (Input.GetKey(KeyCode.W))
        {
            vec += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            vec += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            vec += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            vec += Vector3.right;
        }

        float turn = vec != Vector3.zero
                   ? Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg
                   : 0;

        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            // 카메라 정면을 기준으로 입력한 방향으로 즉시 회전
            character.Roll(followedCamera.eulerAngles + Vector3.up * turn);
            yield return null;
        }

        animator.SetBool("Roll", false);
    }

    private IEnumerator Battle()
    {
        float bTime = Time.time;

        animator.SetBool("Battle", false);
        character.Battle(false);

        while (true)
        {
            // Tab으로 배틀상태 전환
            if (Input.GetKeyDown(KeyCode.Tab) && !IsAttack)
            {
                bool active = !animator.GetBool("Battle");
                animator.SetBool("Battle", active);
                character.Battle(active);
            }

            // 키를 입력하면 대기시간 초기화
            if (Input.anyKey)
            {
                bTime = Time.time;
            }

            // 대기시간이 5초를 넘으면 배틀상태 종료
            if (Time.time - bTime >= waitTime)
            {
                animator.SetBool("Battle", false);
                character.Battle(false);
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
                followedCamera.RotateAround(followedCamera.position, Vector3.up, Input.GetAxis("Mouse X") * cameraTurnSpeed);
            }

            yield return null;
        }
    }
}