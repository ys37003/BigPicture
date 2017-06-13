using System;
using UnityEngine;

public class TitleUI : UIBase<TitleUI>
{
    [SerializeField] private UIButton btnStart, btnLoad, BtnSetting, btnCredit, btnExit, btnBack;
    [SerializeField] private GameObject goMain, goCredit;

    protected override void OverrideAwake()
    {
        EventDelegate.Add(btnStart.onClick, onClickStart);
        EventDelegate.Add(btnLoad.onClick, onClickLoad);
        EventDelegate.Add(BtnSetting.onClick, onClickSetting);
        EventDelegate.Add(btnCredit.onClick, onClickCredit);
        EventDelegate.Add(btnExit.onClick, onClickExit);
        EventDelegate.Add(btnBack.onClick, onClickBack);

        base.OverrideAwake();
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
        NoticeUI.CreateUI("개발 중입니다.");
    }

    private void onClickSetting()
    {
        NoticeUI.CreateUI("개발 중입니다.");
    }

    private void onClickCredit()
    {
        TweenAlpha ta = goMain.AddComponent<TweenAlpha>();
        ta.from = 1;
        ta.to = 0;
        ta.duration = 1;
        EventDelegate.Add(ta.onFinished, () =>
        {
            Destroy(ta);
        });
        ta.PlayForward();

        TweenAlpha ta2 = goCredit.AddComponent<TweenAlpha>();
        ta2.from = 0;
        ta2.to = 1;
        ta2.duration = 0.7f;
        ta2.delay = 0.8f;
        EventDelegate.Add(ta2.onFinished, () =>
        {
            Destroy(ta2);
        });
        ta2.PlayForward();
    }

    private void onClickExit()
    {
        Application.CancelQuit();
    }

    private void onClickBack()
    {
        TweenAlpha ta = goMain.AddComponent<TweenAlpha>();
        ta.from = 0;
        ta.to = 1;
        ta.duration = 1;
        ta.delay = 0.8f;
        EventDelegate.Add(ta.onFinished, () =>
        {
            Destroy(ta);
        });
        ta.PlayForward();

        TweenAlpha ta2 = goCredit.AddComponent<TweenAlpha>();
        ta2.from = 1;
        ta2.to = 0;
        ta2.duration = 0.7f;
        EventDelegate.Add(ta2.onFinished, () =>
        {
            Destroy(ta2);
        });
        ta2.PlayForward();
    }
}