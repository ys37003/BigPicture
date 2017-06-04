using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkBar : MonoBehaviour
{
    [SerializeField] private UITexture texture;
    [SerializeField] private UILabel labelDescription;
    [SerializeField] private GameObject goChoice;
    [SerializeField] private List<UIButtonEx> buttonList;

    private TalkDetailData data;

    private void Awake()
    {
        EventDelegate.Add(buttonList[0].onClick, OnClickResult1);
        EventDelegate.Add(buttonList[1].onClick, OnClickResult2);
        EventDelegate.Add(buttonList[2].onClick, OnClickResult3);
    }

    public void SetData(TalkDetailData data)
    {
        this.data = data;

        Init();
    }

    private void Init()
    {
        string illustPath = string.Format("UI/Illust/ui_conversation_illust{0}", (int)data.Name);
        texture.mainTexture = Resources.Load<Texture>(illustPath);

        labelDescription.text = data.Description;

        int choiceCount = data.ChoiceList.Count;
        if (choiceCount > 0)
        {
            goChoice.SetActive(true);
            for (int i = 0; i < buttonList.Count; ++i)
            {
                if(i < choiceCount)
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
    }

    public void Result()
    {
        if (data.ChoiceList.Count <= 0)
            OnClickResult1();
    }

    public void OnClickResult1()
    {
        if (data.ResultList.Count <= 0)
            return;
    }

    public void OnClickResult2()
    {
        if (data.ResultList.Count <= 1)
            return;
    }

    public void OnClickResult3()
    {
        if (data.ResultList.Count <= 2)
            return;
    }
}