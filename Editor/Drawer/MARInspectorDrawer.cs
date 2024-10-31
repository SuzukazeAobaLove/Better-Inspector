using MikanLab;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    [CustomEditor(typeof(MultiAttributeResource))]
    public class MultiAttributeDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("�˴������鿴����Ҫ�༭���ڱ༭���н��У�",MessageType.Warning);
            if(GUILayout.Button("�ڱ༭���д�"))
            {
                MultiAttributeWindow.ShowWindow(serializedObject.targetObject as MultiAttributeResource);
            }

            SerializedProperty ints = serializedObject.FindProperty("ints");
            SerializedProperty bools = serializedObject.FindProperty("bools");
            SerializedProperty strings = serializedObject.FindProperty("strings");
            SerializedProperty floats = serializedObject.FindProperty("floats");
            SerializedProperty orders = serializedObject.FindProperty("orders");

            int intor = 0, boolor = 0, stringor = 0, floator = 0;
            for (int i= 0;i< orders.arraySize;++i)
            {

                EditorGUILayout.PropertyField((AttributeType)orders.GetArrayElementAtIndex(i).enumValueIndex switch
                {
                    AttributeType.String => strings.GetArrayElementAtIndex(stringor++),
                    AttributeType.Bool => bools.GetArrayElementAtIndex(boolor++),
                    AttributeType.Int => ints.GetArrayElementAtIndex(intor++),
                    AttributeType.Float => floats.GetArrayElementAtIndex(floator++),
                    _ => throw new System.Exception("Unsupported Attribute Type")
                });
            }
        }
    }
}
