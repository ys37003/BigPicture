using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleQuestUI : UIBase<SimpleQuestUI>
{
    [SerializeField] SimpleQuestBar bar;

    private void Awake()
    {
        bar.SetActive(false);
    }

    public void SetData(QuestData data)
    {
        bar.SetData(data);
        bar.SetActive(true);
    }
}