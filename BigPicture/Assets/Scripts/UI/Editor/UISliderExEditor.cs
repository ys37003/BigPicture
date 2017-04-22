//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2016 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(UISliderEx))]
public class UISliderExEditor : UIProgressBarEditor
{
    private UISliderEx uiSldierEx;

    void OnEnable()
    {
        uiSldierEx = target as UISliderEx;
    }

    bool togle = true;
    protected override void OnDrawExtraFields()
    {
        if (NGUIEditorTools.DrawHeader("BreakPoint", "BreakPoint", false, true))
        {
            NGUIEditorTools.BeginContents(true);
            NGUIEditorTools.DrawPaddedProperty("Min", serializedObject, "breakMin", GUILayout.Width(110));
            NGUIEditorTools.DrawPaddedProperty("Max", serializedObject, "breakMax", GUILayout.Width(110));
            NGUIEditorTools.EndContents();
        }
    }

    protected override void OnDrawAppearance()
    {
        if (GUILayout.Button("Init"))
        {
            uiSldierEx.InitSliderEx();
        }
    }
}