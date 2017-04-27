using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : UIBase<OptionUI>
{
    [SerializeField] private UIButtonEx btnClose, btnSave, btnLoad, btnMain, btnReturn1, btnReturn2;

    protected override void OverrideAwake()
    {
        EventDelegate.Add(btnSave.onClick, onClickSave);
        EventDelegate.Add(btnLoad.onClick, onClickLoad);
        EventDelegate.Add(btnMain.onClick, onClickMain);

        EventDelegate.Add(btnClose.onClick, Destroy);
        EventDelegate.Add(btnReturn1.onClick, Destroy);
        EventDelegate.Add(btnReturn2.onClick, Destroy);
    }

    private void onClickMain()
    {
    }

    private void onClickLoad()
    {
    }

    private void onClickSave()
    {
    }
}