using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : UIBase<QuestUI>
{
    [SerializeField] private UILabel labelDescription;
    [SerializeField] private UIButtonEx btnClose, btnOK;

    QuestData data;

    protected override void OverrideAwake()
    {
        EventDelegate.Add(btnClose.onClick, onClickClose);
        EventDelegate.Add(btnOK.onClick, onClickClose);
    }

    public static void CreateUI(QuestData data)
    {
        QuestUI ui = CreateUI();
        ui.data = data;
        ui.labelDescription.text = data.Description;
    }

    private void onClickClose()
    {
        DestroyUI();
    }
}