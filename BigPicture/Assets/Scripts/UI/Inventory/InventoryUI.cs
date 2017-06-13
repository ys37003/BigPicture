using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase<InventoryUI>
{
    [SerializeField] UIButtonEx btnPortion, btnMap;

    private int portionCount = 10;

    protected override void OverrideAwake()
    {
        EventDelegate.Add(btnPortion.onClick, onClickPortion);
        EventDelegate.Add(btnMap.onClick, onClickMap);

        btnPortion.Text = portionCount.ToString();
    }

    private void onClickPortion()
    {
        if (portionCount == 0)
            return;

        portionCount--;
        btnPortion.Text = portionCount.ToString();

        TeamManager.Instance.GetPlayer().Status.HP += 20;
    }

    private void onClickMap()
    {
    }
}