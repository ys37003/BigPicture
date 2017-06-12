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

public enum eJob
{
    // ex
    Archer,
    Dealer,
    Tanker,
    Warrior,
    Wizard,
}

public enum eBuff
{
    PowerUp,
    PowerDown,
    SpeedUp,
    SpeedDown,
    Bleeding,
    Heal,
    Shock,
    Poisoning
}

public class MonsterHUDUI : MonoBehaviour
{
    [SerializeField] private HPbar hpbar;
    [SerializeField] private FollowTarget follow;
    [SerializeField] private LookAtTarget lookAt;
    [SerializeField] private UITexture job;
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
        emotion.color = new Color(255, 255, 255, 1);
        string emotionPath = string.Format("UI/Emotion/{0}", e.ToString());
        emotion.mainTexture = Resources.Load<Texture>(emotionPath);
        StartCoroutine( EmotionDelay(emotion));
    }
    public void SetJob(eJob e)
    {
        string jobPath = string.Format("UI/Job/{0}", e.ToString());
        job.mainTexture = Resources.Load<Texture>(jobPath);

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

    IEnumerator EmotionDelay(UITexture _emotion)
    {
        Color color = _emotion.color;
        while(true)
        {
            color.a -= 0.01f;
            _emotion.color = color;
            if (0 > color.a )
            {
                break;
            }
            yield return null;
        }
    }
}