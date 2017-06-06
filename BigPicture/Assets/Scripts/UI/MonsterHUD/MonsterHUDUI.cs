using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 리소스와 변수명을 같게 하기.
/// </summary>
public enum eEmotion
{
    // ex
    Angry,
    Hpppy,
}

public enum eBuff
{
    //ex
    StrUp,
}

public class MonsterHUDUI : MonoBehaviour
{
    [SerializeField] private HPbar hpbar;
    [SerializeField] private UIFollowTarget follow;
    [SerializeField] private UITexture emotion;
    [SerializeField] private List<UITexture> buffList;

    private int buffCount = 0;

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Init(Transform target, StatusData status)
    {
        hpbar.SetData(status);

        follow.target     = target;
        follow.gameCamera = CameraManager.Instance.GetCamera(eCAMERA.Main);
        follow.uiCamera   = CameraManager.Instance.GetCamera(eCAMERA.HUD);
    }

    public void SetEmotion(eEmotion e)
    {
        string emotionPath = string.Format("UI/Emotion/{0}", e.ToString());
        emotion.mainTexture = Resources.Load<Texture>(emotionPath);
    }

    public void AddBuff(eBuff b)
    {
        string buffPath = string.Format("UI/Buff/{0}", b.ToString());
        buffList[buffCount].mainTexture = Resources.Load<Texture>(buffPath);
        ++buffCount;
    }

    public void RemoveBuff(eBuff b)
    {
        buffList[buffCount].mainTexture = null;
        --buffCount;
    }
}