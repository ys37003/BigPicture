using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkBar : MonoBehaviour
{
    [SerializeField] private UITexture texture;
    [SerializeField] private UILabel labelDescription;
    [SerializeField] private TypewriterEffect typerEffect;
    [SerializeField] private GameObject goChoice;
    [SerializeField] private List<UIButtonEx> buttonList;

    private TalkDescription descData;
    private TalkChoice      choiceData;

    public Action<TalkResultData> onClickNext;
    public Action onFinishTyper;

    private void Awake()
    {
        EventDelegate.Add(buttonList[0].onClick, OnClickResult1);
        EventDelegate.Add(buttonList[1].onClick, OnClickResult2);
        EventDelegate.Add(buttonList[2].onClick, OnClickResult3);
        EventDelegate.Add(typerEffect.onFinished, OnFinishTyper);
    }

    public void SetData(TalkDescription data)
    {
        descData = data;

        string illustPath = string.Format("UI/Illust/ui_conversation_illust{0}", (int)data.Name);
        texture.mainTexture = Resources.Load<Texture>(illustPath);

        labelDescription.text = data.Description;

        goChoice.SetActive(false);
        typerEffect.ResetToBeginning();
    }

    public void SetData(TalkChoice data)
    {
        choiceData = data;

        string illustPath = string.Format("UI/Illust/ui_conversation_illust{0}", (int)data.Name);
        texture.mainTexture = Resources.Load<Texture>(illustPath);

        labelDescription.text = string.Empty;

        int choiceCount = data.ChoiceList.Count;
        if (choiceCount > 0)
        {
            goChoice.SetActive(true);
            for (int i = 0; i < buttonList.Count; ++i)
            {
                if (i < choiceCount)
                {
                    buttonList[i].SetActive(true);
                    buttonList[i].Text = data.ChoiceList[i];
                }
                else
                {
                    buttonList[i].SetActive(false);
                    buttonList[i].Text = string.Empty;
                }
            }
        }
        else
        {
            goChoice.SetActive(false);
        }

        typerEffect.ResetToBeginning();
    }

    public void Result()
    {
        if (typerEffect.isActive)
        {
            typerEffect.Finish();
        }
        else if (onClickNext != null)
        {
            onClickNext(descData.Result);
            onClickNext = null;
        }
    }

    public void OnClickResult1()
    {
        if (choiceData.ResultList.Count <= 0)
            return;

        if (onClickNext != null)
        {
            onClickNext(choiceData.ResultList[0]);
            onClickNext = null;
        }
    }

    public void OnClickResult2()
    {
        if (choiceData.ResultList.Count <= 1)
            return;

        if (onClickNext != null)
        {
            onClickNext(choiceData.ResultList[1]);
            onClickNext = null;
        }
    }

    public void OnClickResult3()
    {
        if (choiceData.ResultList.Count <= 2)
            return;

        if (onClickNext != null)
        {
            onClickNext(choiceData.ResultList[2]);
            onClickNext = null;
        }
    }

    private void OnFinishTyper()
    {
        if (onFinishTyper != null)
        {
            onFinishTyper();
            onFinishTyper = null;
        }
    }
}