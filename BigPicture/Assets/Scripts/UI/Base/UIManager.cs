using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    Camera2D,
    Camera3D,
}

public class UIManager : Singleton<UIManager>
{
    private Dictionary<UIType, List<UIDepth>>   uiDepthDic    = new Dictionary<UIType, List<UIDepth>>();
    private Dictionary<UIType, Stack<UIPanel>>  panelStackDic = new Dictionary<UIType, Stack<UIPanel>>();
    private Dictionary<UIType, int>             panelDepthDic = new Dictionary<UIType, int>();

    [SerializeField]
    private Transform ui2DRoot = null, ui3DRoot = null;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        uiDepthDic.Add(UIType.Camera2D, new List<UIDepth>());
        uiDepthDic.Add(UIType.Camera3D, new List<UIDepth>());

        panelStackDic.Add(UIType.Camera2D, new Stack<UIPanel>());
        panelStackDic.Add(UIType.Camera3D, new Stack<UIPanel>());

        panelStackDic[UIType.Camera2D].Push(ui2DRoot.GetComponent<UIPanel>());
        panelStackDic[UIType.Camera3D].Push(ui3DRoot.GetComponent<UIPanel>());

        panelDepthDic.Add(UIType.Camera2D, 0);
        panelDepthDic.Add(UIType.Camera3D, 0);
    }

    public void AddUI(UIType type, UIDepth uiDepth)
    {
        int count = uiDepthDic[type].Count;
        if (count > 0)
        {
            // 마지막에 생성된 UI의 depth보다 1크게 기준 depth를 부여한다.
            uiDepth.Depth = uiDepthDic[type][count - 1].Depth + 1;
        }

        uiDepthDic[type].Add(uiDepth);

        /*
            추가된 UI에 서브패널이 있다면 나중에 생성된 UI가 위로 올라오도록 해주기 위해서
            새 패널을 생성하고 나중에 생성되는 UI는 새 패널 아래에 둔다.
        */
        if (uiDepth.IsPanel)
        {
            int depth = uiDepth.InitPanelDepth(panelDepthDic[type]) + 1;
            panelDepthDic[type] = depth;

            GameObject go = new GameObject(string.Format("SubPanel{0}", depth));
            Transform tf = go.transform;

            switch (type)
            {
                case UIType.Camera2D: tf.parent = ui2DRoot; break;
                case UIType.Camera3D: tf.parent = ui3DRoot; break;
                default: break;
            }

            tf.localPosition    = Vector3.zero;
            tf.localEulerAngles = Vector3.zero;
            tf.localScale       = Vector3.one;

            UIPanel panel = go.AddComponent<UIPanel>();
            panel.depth = depth;

            panelStackDic[type].Push(panel);
        }
    }

    public void RemoveUI(UIType type, UIDepth depth)
    {
        uiDepthDic[type].Remove(depth);
        panelDepthDic[type] -= depth.PanelCount;
    }

    public void PopPanel(UIType type)
    {
        panelStackDic[type].Pop();
        panelDepthDic[type]--;
    }

    public Transform GetUIRoot(UIType type)
    {
        if (panelStackDic[type].Count > 0)
            return panelStackDic[type].Peek().transform;

        return null;
    }
}