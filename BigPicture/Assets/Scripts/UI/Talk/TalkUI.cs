using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkUI : UIBase<TalkUI>
{
    [SerializeField] private TalkBar[] talkBars;
    [SerializeField] private UIFollowTarget[] follows;

    private TalkBaseData data;
    private List<TalkDetailData> detailDataList = new List<TalkDetailData>();
    private TalkDetailData nowDetailData;

    private TalkBar lastBar;
    private Transform tfCharHud, tfNpcHud;

    public static void CreateUI(Transform tfCharHud, Transform tfNpcHud, TalkBaseData data)
    {
        TalkUI ui = CreateUI();
        ui.data = data;
        ui.detailDataList = DataManager.Instance().GetTalkDetailDataList(data.TalkNumber);

        ui.follows[0].target     = tfCharHud;
        ui.follows[0].gameCamera = CameraManager.Instance.GetCamera(eCAMERA.Main);
        ui.follows[0].uiCamera   = CameraManager.Instance.GetCamera(eCAMERA.UI2D);

        ui.follows[1].target     = tfNpcHud;
        ui.follows[1].gameCamera = CameraManager.Instance.GetCamera(eCAMERA.Main);
        ui.follows[1].uiCamera   = CameraManager.Instance.GetCamera(eCAMERA.UI2D);
    }

    protected override void OverrideAwake()
    {
    }

    protected override void OverrideStart()
    {
        Init();
        StartCoroutine("TalkNext");
    }

    private void Init()
    {
        TalkDetailData first = detailDataList[0];
        Next(first);
    }

    private void Next(TalkDetailData data)
    {
        nowDetailData = data;

        if (data.Name == ePARTNER_NAME.Pearl)
        {
            talkBars[0].SetData(new TalkDescription
            (
                data.Name,
                data.Description,
                data.ResultList.Count > 0 ? data.ResultList[0] : null
            ));

            lastBar = talkBars[0];
            lastBar.onClickNext = onTalkResult;
        }
        else
        {
            if (data.Description != string.Empty)
            {
                talkBars[1].SetData(new TalkDescription
                (
                    data.Name,
                    data.Description,
                    data.ResultList.Count > 0 ? data.ResultList[0] : null
                ));
            }

            lastBar = talkBars[1];
            lastBar.onClickNext = onTalkResult;

            if (data.ChoiceList.Count > 0)
            {
                lastBar.onFinishTyper = () =>
                {
                    talkBars[0].SetData(new TalkChoice(ePARTNER_NAME.Pearl, data.ChoiceList, data.ResultList));
                    lastBar = talkBars[0];
                    lastBar.onClickNext = onTalkResult;
                };
            }
        }
    }

    private IEnumerator TalkNext()
    {
        while(true)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                lastBar.Result();
            }

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                lastBar.OnClickResult1();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                lastBar.OnClickResult2();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                lastBar.OnClickResult3();
            }

            yield return null;
        }
    }

    private void onTalkResult(TalkResultData result)
    {
        if(result == null)
        {
            if (detailDataList.Count > nowDetailData.Order + 1)
            {
                Next(detailDataList[nowDetailData.Order + 1]);
            }
            else
            {
                if (!data.Repeat || result.RepeatEnd)
                {
                    SaveManager.Instance.TalkRepeatEnd(nowDetailData.TalkNumber);
                }

                DestroyUI();
            }
            return;
        }

        // 호감도 획득
        SaveManager.Instance.AddLikeavillity(result.Name, result.Like);

        // 퀘스트 획득

        if (result.Talk >= 0)
        {
            // 다른 대화로 이동
            data = DataManager.Instance().GetTalkBaseData(result.Name, result.Talk);
            detailDataList = DataManager.Instance().GetTalkDetailDataList(data.TalkNumber);
            Next(detailDataList[0]);
            return;
        }

        if(result.Order >= 0)
        {
            // 다른 대사로 이동
            Next(detailDataList[result.Order]);
            return;
        }

        if (result.RepeatEnd)
        {
            SaveManager.Instance.TalkRepeatEnd(nowDetailData.TalkNumber);
            // 대화 종료
            DestroyUI();
        }
    }
}