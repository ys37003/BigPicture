using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager
{
    private static AnimatorManager instance;

    private AnimatorManager()
    {

    }

    public static AnimatorManager Instance()
    {
        if (instance == null)
        {
            instance = new AnimatorManager();
        }

        return instance;
    }

    /// <summary>
    /// Animator의 값을 변화시켜주는 오버로드된 함수들
    /// </summary>
    /// <param name="_ani"></param>
    /// <param name="_key"></param>
    /// <param name="_value"></param>
    public void SetAnimation(Animator _ani, string _key, bool _value)
    {
        _ani.SetBool(_key, _value);
    }

    public void SetAnimation(Animator _ani, string _key, int _value)
    {
        _ani.SetInteger(_key, _value);
    }
}