using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(UIButtonEx))]
public class UIButtonExEditor : Editor
{
    private UIButtonEx uiButtonEx;

    void OnEnable()
    {
        uiButtonEx = target as UIButtonEx;
    }
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("초기화"))
        {
            uiButtonEx.InitButtonEx();
        }

        serializedObject.Update();
    }
}