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

    private void Awake()
    {

    }
}