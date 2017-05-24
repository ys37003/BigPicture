using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggleEx : MonoBehaviour
{
    [SerializeField] private UIToggle toggle;
    [SerializeField] private UILabel  label;
    [SerializeField] private UISprite icon;

    public bool IsCurrent { get { return toggle == UIToggle.current; } }

    /// <summary>
    /// 탭 넘버
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// 토글 엑티브 온오프
    /// </summary>
    /// <param name="active"></param>
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// 토글 작동 온오프
    /// </summary>
    public bool IsEnable
    {
        get { return toggle.isColliderEnabled; }
        set
        {
            Collider c = GetComponent<Collider>();
            if (c != null)
            {
                c.enabled = value;
            }

            Collider2D b = GetComponent<Collider2D>();
            if (b != null)
            {
                b.enabled = value;
            }

            toggle.enabled = value;
        }
    }

    /// <summary>
    /// 토글 텍스트
    /// </summary>
    public string Text
    {
        get { return label.text; }
        set { label.text = value; }
    }

    /// <summary>
    /// 토글 이벤트 추가
    /// </summary>
    public List<EventDelegate> onChange
    {
        get { return toggle.onChange; }
        set { toggle.onChange = value; }
    }

    /// <summary>
    /// 버튼 초기화
    /// </summary>
    [ExecuteInEditMode]
    public void InitToggleEx()
    {
        toggle = GetComponent<UIToggle>();
        label = GetComponentInChildren<UILabel>();

        Transform tfIcon = transform.Find("Icon");
        if (tfIcon != null)
        {
            icon = tfIcon.GetComponent<UISprite>();
        }
    }
}