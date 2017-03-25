using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager  {

    private static AnimatorManager instance;
    private AnimatorManager()
    { }

    public static AnimatorManager Instance()
    {
        if (instance == null)
        {
            instance = new AnimatorManager();
        }
        return instance;
    }

    public void SetAnimation(Animator _ani , string _key , bool _value)
    {
        _ani.SetBool(_key, _value);
    }

    public void SetAnimation(Animator _ani, string _key, int _value)
    {
        _ani.SetInteger(_key, _value);
    }
}
