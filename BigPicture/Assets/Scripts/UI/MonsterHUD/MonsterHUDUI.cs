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
  Pleasure,
  Sad,
  Surprise
}

public enum eBuff
{
    PowerUp,
    PowerDown,
    SpeedUp,
    SpeedDown
}

public class MonsterHUDUI : MonoBehaviour
{
    [SerializeField] private HPbar hpbar;
    [SerializeField] private FollowTarget follow;
    [SerializeField] private LookAtTarget lookAt;
    [SerializeField] private UITexture emotion;
    [SerializeField] private List<UITexture> buffList;
    [SerializeField] private List<eBuff> eBuffList;

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
        if (false == eBuffList.Contains(b))
            eBuffList.Add(b);
        //string buffPath = string.Format("UI/Buff/{0}", b.ToString());
        //buffList[buffCount].mainTexture = Resources.Load<Texture>(buffPath);
        SetBuff();
    }

    void SetBuff()
    {
        for (int i = 0; i < buffList.Count; ++i)
        {
            buffList[i].mainTexture = null;
        }

        for (int i = 0; i < eBuffList.Count; ++i)
        {
            string buffPath = string.Format("UI/Buff/{0}", eBuffList[i].ToString());
            buffList[i].mainTexture = Resources.Load<Texture>(buffPath);
        }
    }
    public void RemoveBuff(eBuff b)
    {
        if( true == eBuffList.Contains(b))
            eBuffList.Remove(b);

        SetBuff();
        //for(int i = 0; i < buffCount; ++ i)
        //    buffList[i].mainTexture = null;


        //eBuffList.Clear();
    }
}