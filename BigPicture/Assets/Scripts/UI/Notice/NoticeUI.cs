using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeUI : UIBase<NoticeUI>
{
    [SerializeField] private UILabel labelDescription;

    public static void CreateUI(string text)
    {
        NoticeUI ui = CreateUI();
        ui.SetData(text);
    }

    protected override void OverrideStart()
    {
        TweenAlpha ta = gameObject.AddComponent<TweenAlpha>();
        ta.from = 1;
        ta.to = 0;
        ta.duration = 3;
        ta.delay = 1;
        EventDelegate.Add(ta.onFinished, () =>
        {
            DestroyUI();
        });
        ta.PlayForward();

        base.OverrideStart();
    }

    public void SetData(string text)
    {
        labelDescription.text = text;
    }
}