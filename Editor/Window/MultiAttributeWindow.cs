using MikanLab;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// ����������Դ�༭��
    /// </summary>
    public class MultiAttributeWindow : EditorWindow
    {
        /// <summary>
        /// ��ǰ���ڱ༭����Դ�ļ�
        /// </summary>
        public MultiAttributeResource curEdit;

        /// <summary>
        /// ���л�֮���curEdit
        /// </summary>
        SerializedObject sobj;

        /// <summary>
        /// �Ƿ��ǵ�һ�δ�
        /// </summary>
        bool isFirstOpened = true;

        /// <summary>
        /// �������򿪴���
        /// </summary>
        [MenuItem("Window/����������Դ�༭��")] // ���ò˵���·��
        public static void ShowWindow()
        {
            GetWindow<MultiAttributeWindow>("����������Դ�༭��");
        }

        /// <summary>
        /// �������Ĵ򿪴���
        /// </summary>
        /// <param name="multiAttibuteResource"></param>
        public static void ShowWindow(MultiAttributeResource multiAttibuteResource)
        {
            var window = GetWindow<MultiAttributeWindow>("����������Դ�༭��");
            window.curEdit = multiAttibuteResource;
        }

        void Awake()
        {
            
            minSize = new Vector2(800, 400);

            // �������ߴ�
            maxSize = new Vector2(1200, 800);
            position = new Rect(100, 100, 300, 150);
        }


        private void OnEnable()
        {
            if (curEdit != null) sobj = new(curEdit);
        }

        public void OnGUI()
        {
            if(isFirstOpened && curEdit != null)
            {
                isFirstOpened = false;
                sobj = new(curEdit);
            }
            EditorGUI.BeginChangeCheck();
            
            curEdit = (MultiAttributeResource) EditorGUILayout.ObjectField("��ǰ�༭����", curEdit, typeof(MultiAttributeResource),false);
            
            //��������Ƿ����仯
            if(EditorGUI.EndChangeCheck())
            {
                Debug.Log("��ǰ�༭����ı�");
                if (curEdit != null) sobj = new(curEdit);
                else sobj = null;
            }
            
            if (sobj == null || curEdit == null) return;

            EditorGUILayout.PropertyField(sobj.FindProperty("attributes"));
            

            sobj.ApplyModifiedProperties();
        }
    }
}
