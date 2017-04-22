using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(UIToggleEx))]
public class UIToggleExEditor : Editor
{
    private UIToggleEx uiToggleEx;

    void OnEnable()
    {
        uiToggleEx = target as UIToggleEx;
    }
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("초기화"))
        {
            uiToggleEx.InitToggleEx();
        }

        serializedObject.Update();
    }
}