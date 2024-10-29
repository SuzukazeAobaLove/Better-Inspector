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
            EditorGUILayout.HelpBox("�˴������鿴����Ҫ�༭������Ӧ���ִ��", MessageType.Warning);
            if(GUILayout.Button("�ڱ༭���д�"))
            {
                MultiAttributeWindow.ShowWindow(serializedObject.targetObject as MultiAttributeResource);
            }

            GUI.enabled = false;

            var attirbuteArray = serializedObject.FindProperty("attributes");
            for (int i = 0; i < attirbuteArray.arraySize; i++)
            {
                // ��ȡ����Ԫ��
                SerializedProperty elementProperty = attirbuteArray.GetArrayElementAtIndex(i);

                // ��ȡ Name ����
                SerializedProperty nameProperty = elementProperty.FindPropertyRelative("name");

                // ʹ�� Name ���Ե�ֵ��Ϊ��ǩ��������Ԫ��
                EditorGUILayout.PropertyField(elementProperty, new GUIContent(nameProperty.stringValue), true);
            }
        }
    }
}
