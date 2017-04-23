using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase<T> : MonoBehaviour where T : class
{
    private static T instance;

    private UIDepth depth = new UIDepth();

    [SerializeField]
    private UIType type = UIType.Camera2D;
    public  UIType Type
    {
        get { return type; }
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
            go = Instantiate(go, UIManager.Instance.GetUIRoot(ui.type), false);
            go.transform.localScale = Vector3.one;

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
        depth.AddWidget(GetComponentsInChildren<UIWidget>());
        depth.AddPanel(GetComponentsInChildren<UIPanel>());
        UIManager.Instance.AddUI(type, depth);

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

    protected virtual void Show(bool active)
    {
        gameObject.SetActive(active);
    }

    protected virtual void Destroy()
    {
        UIManager.Instance.RemoveUI(type, depth);

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
}