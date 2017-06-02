using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkUI : UIBase<TalkUI>
{
    [SerializeField] private List<TalkBar> talkBarList;

    private TalkBaseData data;
    private List<TalkDetailData> detailDataList = new List<TalkDetailData>();

    private TalkBar lastBar;

    public static void CreateUI(TalkBaseData data)
    {
        TalkUI ui = CreateUI();
        ui.data = data;
        ui.detailDataList = DataManager.Instance().GetTalkDetailDataList(data.TalkNumber);
    }

    protected override void OverrideAwake()
    {
    }

    protected override void OverrideStart()
    {
        StartCoroutine("TalkNext");
    }

    IEnumerator TalkNext()
    {
        while(true)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                lastBar.Result();
            }

            if(Input.GetKeyDown(KeyCode.Keypad1))
            {
                lastBar.OnClickResult1();
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                lastBar.OnClickResult2();
            }

            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                lastBar.OnClickResult3();
            }

            yield return null;
        }
    }
}