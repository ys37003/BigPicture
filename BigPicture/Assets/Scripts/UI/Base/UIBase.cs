using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    public static readonly string Path = "UI/Prefabs/";

    private List<UIWidget>  widgetList  = new List<UIWidget>();
    private List<UIPanel>   panelList   = new List<UIPanel>();

    [SerializeField]
    private UIType type;
    public  UIType Type
    {
        get { return type; }
    }

    private int depth;
    /// <summary>
    /// 기준 depth * 100으로 서로 다른 UI간에 겹침현상 제거
    /// </summary>
    public int UIDepth
    {
        get { return depth; }
        set
        {
            depth = value;

            foreach (UIWidget widget in widgetList)
            {
                widget.depth += depth * 100;
            }
        }
    }

    public bool IsSubPanel { get { return panelList.Count > 0; } }

    public int SubPanelCount { get { return panelList.Count; } }
 
    /// <summary>
    /// 월드에 UI가 없다면 UI를 생성
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static T Create<T>() where T : UIBase
    {
        T ui = FindObjectOfType<T>();

        if (ui != null)
            return ui;

        GameObject go = Resources.Load<GameObject>(string.Concat(Path, typeof(T)));
        ui = go.GetComponent<T>();

        go = Instantiate(go, UIManager.Instance.GetUIRoot(ui.type), false);
        go.transform.localScale = Vector3.one;

        return ui;
    }

    private void Awake()
    {
        widgetList.AddRange(GetComponentsInChildren<UIWidget>());
        panelList.AddRange(GetComponentsInChildren<UIPanel>());

        overrideAwake();
    }

    private void Start()
    {
        UIManager.Instance.AddUI(this);

        overrideStart();
    }

    protected abstract void overrideAwake();
    protected abstract void overrideStart();

    /// <summary>
    /// 서브패널은 무조건 depth가 1부터 시작
    /// 서브패널 depth를 초기화하고 최종 depth를 반환
    /// </summary>
    /// <param name="depth"></param>
    /// <returns></returns>
    public int InitSubPanelDepth(int depth)
    {
        foreach (UIPanel panel in panelList)
        {
            panel.depth += depth;
        }

        return panelList.Count + depth;
    }

    public virtual void Show(bool active)
    {
        gameObject.SetActive(active);
    }

    public virtual void Destroy()
    {
        UIManager.Instance.RemoveUI(this);

        Transform root = transform.parent;
        if (root.GetComponent<UIPanel>() && root.childCount == 1)
        {
            UIManager.Instance.PopPanel(type);
            Destroy(root);
        }
        else
        {
            Destroy(this);
        }
    }
}