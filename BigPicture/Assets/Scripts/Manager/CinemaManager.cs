using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCINEMA_POS
{
    TITLE,
    STATUS,
}

public class CinemaManager : Singleton<CinemaManager>
{
    public Transform[] poses;

    public void StartTitleUICinema()
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
            CameraManager.Instance.GetCamera(eCAMERA.HUD).farClipPlane = 1000;
            CharacterUI.CreateUI();
        });
        tt.PlayForward();
    }

    public void StartStatusUICinema()
    {
        Camera cinema = CameraManager.Instance.GetCamera(eCAMERA.Cinema);
        GameObject go = cinema.gameObject;

        TweenPosition tp = go.AddComponent<TweenPosition>();
        tp.from = CameraManager.Instance.GetCamera(eCAMERA.Main).transform.localPosition;
        tp.to = poses[(int)eCINEMA_POS.STATUS].localPosition;
        tp.duration = 0.4f;
        tp.delay = 0;
        EventDelegate.Add(tp.onFinished, () =>
        {
            Destroy(tp);
        });
        tp.PlayForward();

        Transform tfChar = TeamManager.Instance.GetPlayer().transform;
        Transform cp = cinema.transform.parent;

        TweenRotation tr = go.AddComponent<TweenRotation>();
        tr.from = CameraManager.Instance.GetCamera(eCAMERA.Main).transform.localEulerAngles;
        tr.to = poses[(int)eCINEMA_POS.STATUS].localEulerAngles;
        tr.duration = 0.4f;
        tr.delay = 0;
        EventDelegate.Add(tr.onFinished, () =>
        {
            Destroy(tr);
        });
        tr.PlayForward();

        TweenRotation tr2 = cp.gameObject.AddComponent<TweenRotation>();
        tr2.from = cp.eulerAngles;
        tr2.to = tfChar.eulerAngles;
        tr2.duration = 0.3f;
        tr2.delay = 0.4f;
        EventDelegate.Add(tr2.onFinished, () =>
        {
            Destroy(tr2);
        });
        tr2.PlayForward();

        CameraManager.Instance.GetCamera(eCAMERA.HUD).farClipPlane = 0.01f;
    }

    public void EndStatusUICinema()
    {
        Camera cinema = CameraManager.Instance.GetCamera(eCAMERA.Cinema);
        GameObject go = cinema.gameObject;
        TweenTransform tt = go.AddComponent<TweenTransform>();

        tt.from = cinema.transform;
        tt.to = CameraManager.Instance.GetCamera(eCAMERA.Main).transform;
        tt.duration = 1f;
        tt.delay = 0;
        EventDelegate.Add(tt.onFinished, () =>
        {
            Destroy(tt);
            CameraManager.Instance.GetCamera(eCAMERA.HUD).farClipPlane = 1000;
        });
        tt.PlayForward();
    }
}