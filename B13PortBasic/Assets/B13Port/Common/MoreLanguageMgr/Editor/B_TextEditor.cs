/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
  ~  File：B_TextEditor.cs                          
  ~  Author：B13Port                         
  ~  E-mail：bai0613@foxmail.com                    
  ~  Date：2022/04/24 10:43:28                             
  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(B_Text), true)]
[CanEditMultipleObjects]
public class B_TextEditor : UnityEditor.UI.TextEditor
{
    private SerializedProperty isShowLauge;

    protected override void OnEnable()
    {
        base.OnEnable();
        isShowLauge = serializedObject.FindProperty("isShowLauge");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        serializedObject.Update();
        EditorGUILayout.PropertyField(isShowLauge);
        serializedObject.ApplyModifiedProperties();
    }
}
