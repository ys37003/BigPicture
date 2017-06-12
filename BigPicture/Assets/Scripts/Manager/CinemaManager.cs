using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCINEMA_POS
{
    TITLE,
}

public class CinemaManager : Singleton<CinemaManager>
{
    public Transform[] poses;

    private void Awake()
    {
    }

    public void StartTitleCinema()
    {
        Camera cinema = CameraManager.Instance.GetCamera(eCAMERA.Cinema);
        GameObject go = cinema.gameObject;
        TweenTransform tt = go.AddComponent<TweenTransform>();

        tt.from = poses[(int)eCINEMA_POS.TITLE];
        tt.to = CameraManager.Instance.GetCamera(eCAMERA.Main).transform;
        tt.duration = 4;
        tt.delay = 1;
        EventDelegate.Add(tt.onFinished, () =>
        {
            Destroy(tt);
            CharacterUI.ShowUI(true);
        });
        tt.PlayForward();
    }
}