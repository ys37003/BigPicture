using System;
using UnityEngine;

public class TitleUI : UIBase<TitleUI>
{
    [SerializeField] private UIButton btnStart, btnLoad, BtnSetting, btnCredit, btnExit;

    private void Awake()
    {
        EventDelegate.Add(btnStart.onClick, onClickStart);
        EventDelegate.Add(btnLoad.onClick, onClickLoad);
        EventDelegate.Add(BtnSetting.onClick, onClickSetting);
        EventDelegate.Add(btnCredit.onClick, onClickCredit);
        EventDelegate.Add(btnExit.onClick, onClickExit);
    }

    private void onClickStart()
    {
        CinemaManager.Instance.StartTitleUICinema();

        TweenAlpha ta = gameObject.AddComponent<TweenAlpha>();
        ta.from = 1;
        ta.to = 0;
        ta.duration = 1;
        EventDelegate.Add(ta.onFinished, () =>
        {
            DestroyUI();
        });
        ta.PlayForward();
    }

    private void onClickLoad()
    {
    }

    private void onClickSetting()
    {
    }

    private void onClickCredit()
    {
    }

    private void onClickExit()
    {
        Application.CancelQuit();
    }
}