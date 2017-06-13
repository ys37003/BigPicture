using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase<InventoryUI>
{
    [SerializeField] UIButtonEx btnPortion, btnMap, btnBack;
    [SerializeField] GameObject goInventory;

    private int portionCount = 10;

    protected override void OverrideAwake()
    {
        EventDelegate.Add(btnPortion.onClick, onClickPortion);
        EventDelegate.Add(btnMap.onClick, onClickMap);
        EventDelegate.Add(btnBack.onClick, onClickBack);

        btnPortion.Text = portionCount.ToString();
    }

    public static void ShowMapBackButton(bool active)
    {
        InventoryUI ui = CreateUI();
        if (active)
        {
            ui.btnBack.SetActive(true);
        }
        else
        {
            ui.goInventory.SetActive(true);
        }
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
        CinemaManager.Instance.StartMapCinema();
        goInventory.SetActive(false);
    }

    private void onClickBack()
    {
        CinemaManager.Instance.EndMapCinema();
        btnBack.SetActive(false);
    }
}