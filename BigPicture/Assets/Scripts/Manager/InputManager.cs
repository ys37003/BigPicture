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
        AddKey(new InputKey(KeyCode.Escape, InputType.KeyDown), new InputValue(() =>
        {
            UIManager.Instance.LastUIClose();
        }, null));

        AddKey(new InputKey(KeyCode.F1, InputType.KeyDown), new InputValue(() =>
        {
            if (StatusUI.IsShow)
            {
                WorldManager.Instance.SetPause(false);
                StatusUI.DestroyUI();
            }
            else
            {
                WorldManager.Instance.SetPause(true);
                StatusUI.CreateUI();
            }
        }, null));

        AddKey(new InputKey(KeyCode.F2, InputType.KeyDown), new InputValue(() =>
        {
            if (OptionUI.IsShow)
            {
                WorldManager.Instance.SetPause(false);
                OptionUI.DestroyUI();
            }
            else
            {
                WorldManager.Instance.SetPause(true);
                OptionUI.CreateUI();
            }
        }, null));

        AddKey(new InputKey(KeyCode.F3, InputType.KeyDown), new InputValue(() =>
        {
            if(CharacterUI.IsShow)
            {
                WorldManager.Instance.SetPause(false);
                CharacterUI.DestroyUI();
            }
            else
            {
                WorldManager.Instance.SetPause(true);
                CharacterUI.CreateUI();
            }
        }, null));

        AddKey(new InputKey(KeyCode.F4, InputType.KeyDown), new InputValue(() =>
        {
            if(TalkUI.IsShow)
            {
                WorldManager.Instance.SetPause(false);
                TalkUI.DestroyUI();
            }
            else
            {
                WorldManager.Instance.SetPause(true);
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