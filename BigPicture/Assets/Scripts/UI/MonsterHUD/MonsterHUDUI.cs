using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

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
    [SerializeField] private FollowTarget follow;
    [SerializeField] private LookAtTarget lookAt;
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
        follow.target = target;
        lookAt.target = CameraManager.Instance.GetCamera(eCAMERA.Main).transform;
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