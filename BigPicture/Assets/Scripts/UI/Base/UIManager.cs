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
    private Dictionary<UIType, List<UIBase>>    uiListDic       = new Dictionary<UIType, List<UIBase>>();
    private Dictionary<UIType, Stack<UIPanel>>  panelStackDic   = new Dictionary<UIType, Stack<UIPanel>>();
    private Dictionary<UIType, int>             panelDepthDic   = new Dictionary<UIType, int>();

    [SerializeField]
    private Transform ui2DRoot = null, ui3DRoot = null;

    private void Awake()
    {
        uiListDic.Add(UIType.Camera2D, new List<UIBase>());
        uiListDic.Add(UIType.Camera3D, new List<UIBase>());

        panelStackDic.Add(UIType.Camera2D, new Stack<UIPanel>());
        panelStackDic.Add(UIType.Camera3D, new Stack<UIPanel>());

        panelStackDic[UIType.Camera2D].Push(ui2DRoot.GetComponent<UIPanel>());
        panelStackDic[UIType.Camera3D].Push(ui3DRoot.GetComponent<UIPanel>());

        panelDepthDic.Add(UIType.Camera2D, 0);
        panelDepthDic.Add(UIType.Camera3D, 0);
    }

    public void AddUI(UIBase ui)
    {
        uiListDic[ui.Type].Add(ui);

        /*
            추가된 UI에 서브패널이 있다면 나중에 생성된 UI가 위로 올라오도록 해주기 위해서
            새 패널을 생성하고 나중에 생성되는 UI는 새 패널 아래에 둔다.
        */
        if (ui.IsSubPanel)
        {
            int depth = ui.InitSubPanelDepth(panelDepthDic[ui.Type]) + 1;
            panelDepthDic[ui.Type] = depth;

            GameObject go = new GameObject(string.Format("SubPanel{0}", depth));
            Transform tf = go.transform;

            switch (ui.Type)
            {
                case UIType.Camera2D: tf.parent = ui2DRoot; break;
                case UIType.Camera3D: tf.parent = ui3DRoot; break;
                default: break;
            }

            tf.localPosition = Vector3.zero;
            tf.localEulerAngles = Vector3.zero;
            tf.localScale = Vector3.one;

            UIPanel panel = go.AddComponent<UIPanel>();
            panel.depth = depth;

            panelStackDic[ui.Type].Push(panel);
        }
    }

    public void RemoveUI(UIBase ui)
    {
        uiListDic[ui.Type].Remove(ui);
        panelDepthDic[ui.Type] -= ui.SubPanelCount;
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