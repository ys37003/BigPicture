using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIBase
{
    UIDepth UIDepth     { get; }
    UIType  Type        { get; }
    bool    CanClose    { get; }

    void Destroy();
}

public abstract class UIBase<T> : MonoBehaviour, IUIBase where T : class
{
    private static T instance;

    private UIDepth uiDepth = new UIDepth();
    public  UIDepth UIDepth
    {
        get { return uiDepth; }
    }

    [SerializeField]
    private UIType type = UIType.Camera2D;
    public  UIType Type
    {
        get { return type; }
    }

    [SerializeField]
    private bool canClose = true;
    public  bool CanClose
    {
        get { return canClose; }
    }

    private static bool isShow = true;
    public  static bool IsShow
    {
        get { return instance != null && isShow; }
    }

    /// <summary>
    /// 월드에 UI가 없다면 UI를 생성
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static T CreateUI()
    {
        if (instance == null)
        {
            instance = FindObjectOfType(typeof(T)) as T;
        }

        if (instance == null)
        {
            GameObject go = Resources.Load<GameObject>(string.Concat("UI/Prefabs/", typeof(T)));
            instance = go.GetComponent<T>();

            UIBase<T> ui = instance as UIBase<T>;
            UIPanel panel = UIManager.Instance.GetUIRoot(ui.type).GetComponent<UIPanel>();

            go = Instantiate(go, panel.transform, false);
            go.transform.localScale = Vector3.one;

            UIWidget widget = go.GetComponent<UIWidget>();
            widget.width    = (int)panel.width;
            widget.height   = (int)panel.height;

            instance = go.GetComponent<T>();
        }

        return instance;
    }

    /// <summary>
    /// 월드에 UI가 있다면 UI를 제거
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void DestroyUI()
    {
        if (instance != null)
        {
            (instance as UIBase<T>).Destroy();
        }
    }

    public static void ShowUI(bool active)
    {
        if (instance != null)
        {
            (instance as UIBase<T>).Show(active);
            isShow = active;
        }
    }

    private void Awake()
    {
        uiDepth.AddWidget(GetComponentsInChildren<UIWidget>());
        uiDepth.AddPanel(GetComponentsInChildren<UIPanel>());
        UIManager.Instance.AddUI(this as IUIBase);

        OverrideAwake();
    }

    private void Start()
    {
        OverrideStart();
    }

    protected virtual void OverrideAwake()
    {
    }

    protected virtual void OverrideStart()
    {
    }

    protected virtual void OverrideDestroy()
    {
        UIManager.Instance.RemoveUI(this as IUIBase);

        Transform root = transform.parent;
        if (root.GetComponent<UIPanel>() && root.childCount == 1)
        {
            UIManager.Instance.PopPanel(type);
            Destroy(root);
        }
        else
        {
            Destroy(gameObject);
        }

        instance = null;
    }

    protected virtual void Show(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Destroy()
    {
        OverrideDestroy();
    }
}