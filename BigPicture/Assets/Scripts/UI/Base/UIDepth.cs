using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDepth
{
    private List<UIWidget> widgetList = new List<UIWidget>();
    private List<UIPanel>  panelList  = new List<UIPanel>();

    private int depth = 0;
    /// <summary>
    /// 기준 depth * 100으로 서로 다른 UI간에 겹침현상 제거
    /// </summary>
    public int Depth
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
    
    public bool IsPanel     { get { return panelList.Count > 0; } }
    public int  PanelCount  { get { return panelList.Count; } }

    /// <summary>
    /// 패널은 무조건 depth가 1부터 시작
    /// 패널 depth를 초기화하고 최종 depth를 반환
    /// </summary>
    /// <param name="depth"></param>
    /// <returns></returns>
    public int InitPanelDepth(int depth)
    {
        foreach (UIPanel panel in panelList)
        {
            panel.depth += depth;
        }

        return panelList.Count + depth;
    }

    public void AddWidget(UIWidget[] widget)
    {
        widgetList.AddRange(widget);
    }

    public void AddPanel(UIPanel[] panel)
    {
        panelList.AddRange(panel);
    }
}
