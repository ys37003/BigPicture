using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonEx : MonoBehaviour
{
    [SerializeField] private UIButton    button;
    [SerializeField] private UILabel     label;
    [SerializeField] private UISprite    icon;

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public bool IsEnable
    {
        get { return button.isEnabled; }
        set { button.isEnabled = value; }
    }

    public string Text
    {
        get { return label.text; }
        set { label.text = value; }
    }
}