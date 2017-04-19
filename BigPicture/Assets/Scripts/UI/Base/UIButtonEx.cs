using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIButton 기능 확장
/// </summary>
public class UIButtonEx : MonoBehaviour
{
    [SerializeField] private UIButton    button;
    [SerializeField] private UILabel     label;
    [SerializeField] private UISprite    icon;

    /// <summary>
    /// 버튼 이벤트 추가
    /// </summary>
    public List<EventDelegate> onClick
    {
        get { return button.onClick; }
        set { button.onClick = value; }
    }

    /// <summary>
    /// 버튼 엑티브 온오프
    /// </summary>
    /// <param name="active"></param>
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// 버튼 작동 온오프
    /// </summary>
    public bool IsEnable
    {
        get { return button.isEnabled; }
        set { button.isEnabled = value; }
    }

    /// <summary>
    /// 버튼 텍스트
    /// </summary>
    public string Text
    {
        get { return label.text; }
        set { label.text = value; }
    }

    /// <summary>
    /// 버튼 초기화
    /// </summary>
    [ExecuteInEditMode]
    public void InitButtonEx()
    {
        button = GetComponent<UIButton>();
        label = GetComponentInChildren<UILabel>();

        Transform tfIcon = transform.FindChild("Icon");
        if (tfIcon != null)
        {
            icon = tfIcon.GetComponent<UISprite>();
        }
    }
}