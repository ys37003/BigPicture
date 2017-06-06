using System;
using UnityEngine;

public class HPbar : MonoBehaviour
{
    [SerializeField] private UILabel labelHP, labelMaxHP;
    [SerializeField] private UISliderEx slider;

    public void SetData(StatusData data)
    {
        data.onUpdateHP = onUpdateHP;
        onUpdateHP(data.HP);
    }

    private void onUpdateHP(float hp)
    {
        slider.value = hp / StatusData.MAX_HP;

        if (labelHP != null && labelMaxHP != null)
        {
            labelHP.text    = string.Format("{0}/", hp);
            labelMaxHP.text = string.Format("{0}", StatusData.MAX_HP);
        }
    }
}