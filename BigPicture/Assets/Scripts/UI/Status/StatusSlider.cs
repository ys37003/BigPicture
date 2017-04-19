using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusSlider : MonoBehaviour
{
    [SerializeField]
    private UISliderEx slider;
    private float beforeValue = 0;

    public eSTAT stat = eSTAT.STRENGTH;

    public delegate int getSkillPoint();
    public getSkillPoint GetSkillPoint;
    public Action<eSTAT, int> onUpdateStat;

    private void Awake()
    {
        EventDelegate.Add(slider.onChange, onChangeStat);
    }

    public void SetData(int value)
    {
        beforeValue  = value * 0.1f;
        slider.value = value * 0.1f;
    }

    bool first = true;
    private void onChangeStat()
    {
        if (first)
        {
            first = false;
            return;
        }

        UISliderEx current = UIProgressBar.current as UISliderEx;

        int result = Convert.ToInt32((current.value - beforeValue) * 10f);
        if (result > GetSkillPoint())
        {
            result = GetSkillPoint();
            current.Set(beforeValue + result * 0.1f, false);
        }

        onUpdateStat(stat, result);
        beforeValue = current.value;
    }
}