using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    Key,
    KeyDown,
    KeyUp,
}

public struct InputKey
{
    public KeyCode   Code;
    public InputType Type;

    public InputKey(KeyCode code, InputType type)
    {
        Code = code;
        Type = type;
    }
}

public struct InputValue
{
    public Action onInputTrue;
    public Action onInputFalse;

    public InputValue(Action inputTrue, Action inputFalse)
    {
        onInputTrue  = inputTrue;
        onInputFalse = inputFalse;
    }
}

public class InputManager : Singleton<InputManager>
{
    private Dictionary<InputKey, InputValue> InputDic = new Dictionary<InputKey, InputValue>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        AddKey(new InputKey(KeyCode.F1, InputType.KeyDown), new InputValue(() =>
        {
            if (StatusUI.IsShow)
            {
                StatusUI.DestroyUI();
            }
            else
            {
                StatusUI.CreateUI();
            }
        }, null));
    }

    private void Start()
    {
        StartCoroutine("InputCheck");
    }

    public void AddKey(InputKey key, InputValue value)
    {
        InputDic.Add(key, value);
    }

    IEnumerator InputCheck()
    {
        while(true)
        {
            foreach(InputKey key in InputDic.Keys)
            {
                bool isInput = false;
                switch (key.Type)
                {
                    case InputType.Key:     isInput = Input.GetKey(key.Code);     break;
                    case InputType.KeyDown: isInput = Input.GetKeyDown(key.Code); break;
                    case InputType.KeyUp:   isInput = Input.GetKeyUp(key.Code);   break;
                }

                if (isInput)
                {
                    if (InputDic[key].onInputTrue != null)
                        InputDic[key].onInputTrue();
                }
                else
                {
                    if (InputDic[key].onInputFalse != null)
                        InputDic[key].onInputFalse();
                }
            }

            yield return null;
        }
    }
}