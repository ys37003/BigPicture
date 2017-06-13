using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleQuestUI : UIBase<SimpleQuestUI>
{
    [SerializeField] SimpleQuestBar bar;

    protected override void OverrideAwake()
    {
        bar.SetActive(false);
    }

    public void SetData(QuestData data)
    {
        bar.SetData(data);
        bar.SetActive(true);
    }
}