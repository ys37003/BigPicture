using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleQuestBar : MonoBehaviour
{
    [SerializeField] UIButtonEx btnShowQuest;
    [SerializeField] UILabel labelDescription;

    QuestData data;

    private void Awake()
    {
        EventDelegate.Add(btnShowQuest.onClick, onClickShowQuest);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetData(QuestData data)
    {
        this.data = data;
        labelDescription.text = data.Description;
    }

    private void onClickShowQuest()
    {
        QuestUI.CreateUI(data);
    }
}