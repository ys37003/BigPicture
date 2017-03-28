using System;
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

    public float MoveSpeed = 10;

    /// <summary>
    /// 구르기 속도는 이동속도(MoveSpeed) %이다.
    /// ex) RollSpeed가 0.7이면 7(MoveSpeed * 0.7)의 속도로 구른다.
    /// </summary>
    public float RollSpeed = 0.7f;
    public float CameraTurnSpeed = 30;

    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private Transform followedCamera = null;

    private bool run = false;

    private void Awake()
    {
        StartCoroutine("Move");
        StartCoroutine("RunCheck");
        StartCoroutine("Attack");
        StartCoroutine("CameraRotation");
   }

    IEnumerator Move()
    {
        float h = 0;
        float v = 0;
        float move = 0;

        while (true)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            // 이동 입력이 있으면 1, 달리기(쉬프트) 입력이 있으면 x2
            move = Mathf.Abs(h) > Mathf.Abs(v) ? Mathf.Abs(h) : Mathf.Abs(v);
            move += run ? move : 0;

            animator.SetFloat("Move", move);

            if (move > 0)
            {
                // 카메라의 정면을 기준으로 캐릭터 방향 설정
                Vector3 forward = Vector3.Scale(followedCamera.forward, new Vector3(1, 0, 1)).normalized;
                Vector3 dir = transform.InverseTransformDirection(v * forward + h * followedCamera.right);

                float turn = Mathf.Atan2(dir.x, dir.z);
                float turnSpeed = Mathf.Lerp(180, 360, dir.z);

                transform.Rotate(0, turn * Time.deltaTime * turnSpeed, 0);
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

    IEnumerator RunCheck()
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
                    run = true;
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

            if (run)
            {
                if (!Input.GetKey(KeyCode.W) &&
                    !Input.GetKey(KeyCode.A) &&
                    !Input.GetKey(KeyCode.S) &&
                    !Input.GetKey(KeyCode.D))
                {
                    run = false;
                    for (int i = 0; i < 4; ++i)
                    {
                        beforeInputTime[i] = 0;
                    }
                }
            }

            yield return null;
        }
    }

    IEnumerator Roll()
    {
        animator.SetBool("Roll", true);
        yield return new WaitForSeconds(0.05f);

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

        float turn = Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg;

        // 카메라 정면을 기준으로 입력한 방향으로 즉시 회전
        transform.eulerAngles = followedCamera.eulerAngles + Vector3.up * turn;

        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed * 0.7f);
            yield return null;
        }

        animator.SetBool("Roll", false);
        yield return Move();
    }

    IEnumerator Attack()
    {
        while (true)
        {
            // 공격(마우스 좌클릭)
            bool mouse_left = Input.GetMouseButton(0);
            animator.SetBool("MouseL", mouse_left);

            yield return null;
        }
    }

    IEnumerator CameraRotation()
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
}