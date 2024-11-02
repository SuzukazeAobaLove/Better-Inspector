using MikanLab;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        MultiAttributeResource curEdit;

        /// <summary>
        /// ������ֵ
        /// </summary>
        Vector2 scroll;

        /// <summary>
        /// ɾ��ͼ��
        /// </summary>
        Texture deleteIcon;

        /// <summary>
        /// 
        /// </summary>
        GUIStyle FieldStyle = new GUIStyle();
        
        /// <summary>
        /// �������򿪴���
        /// </summary>
        [MenuItem("Window/MikanLab/����������Դ�༭��")] // ���ò˵���·��
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
            //λ�ô�С����
            minSize = new Vector2(500, 400);
            maxSize = new Vector2(500, 800);
            position = new Rect(100, 100, 300, 150);

            //����Icon
            deleteIcon = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath("5f3bcd12f441e1f4a84b5a685237064a"));
            
            //�����߾�
            FieldStyle.margin.top = -4; 
            FieldStyle.margin.bottom = -4;
        }

        private void OnDisable()
        {
            if (curEdit == null) return;
            EditorUtility.SetDirty(curEdit);
            AssetDatabase.SaveAssets();
            curEdit = null;
        }

        public void OnGUI()
        {
            int deleteIndex = -1;
            
            curEdit = (MultiAttributeResource) EditorGUILayout.ObjectField("��ǰ�༭����", curEdit, typeof(MultiAttributeResource),false);

            if (curEdit == null) return;
            
            //����������
            GUILayout.BeginHorizontal();
            //���Ʋ�����ť
            if (GUILayout.Button("�������", GUILayout.Width(100)))
            {
                GUI.FocusControl("");
                curEdit.attributes.Add(new StringAttribute());
            }

            GUILayout.EndHorizontal();

            //���ͷ����
            GUILayout.BeginHorizontal();
            GUILayout.Label("����", GUILayout.Width(100));
            GUILayout.Label("������",GUILayout.Width(110));
            GUILayout.Label("����ֵ", GUILayout.Width(100));


            GUILayout.EndHorizontal();

            scroll =  GUILayout.BeginScrollView(scroll,false,false);
            

            //���λ�����������
            for(int index = 0;index < curEdit.attributes.Count;++index)
            {
                var Item = curEdit.attributes[index];
                GUILayout.BeginHorizontal(FieldStyle);
                
                //�ж��Ƿ��漰������ת��
                var newType = (AttributeType) EditorGUILayout.EnumPopup(Item.typeEnum, GUILayout.Width(100));
                if(newType != Item.typeEnum)
                {
                    switch (newType)
                    {
                        case AttributeType.String:
                            curEdit.attributes[index] = new StringAttribute();break;
                        case AttributeType.Int:
                            curEdit.attributes[index] = new IntAttribute();break;
                        case AttributeType.Float:
                            curEdit.attributes[index] = new FloatAttribute();break;
                        case AttributeType.Bool:
                            curEdit.attributes[index] = new BoolAttribute();break;
                    }
                }
                
                //��������
                Item.name = GUILayout.TextField(Item.name,GUILayout.Width(100));
                GUILayout.Label(":", GUILayout.Width(10));
                
                //����ʵ��ֵ
                switch(Item.typeEnum)
                {
                    case AttributeType.String:
                        (Item as StringAttribute).value = GUILayout.TextField((Item as StringAttribute).value, GUILayout.Width(100));
                        break;
                    case AttributeType.Int:
                        (Item as IntAttribute).value = EditorGUILayout.IntField((Item as IntAttribute).value, GUILayout.Width(100));
                        break;
                    case AttributeType.Float:
                        (Item as FloatAttribute).value = EditorGUILayout.FloatField((Item as FloatAttribute).value, GUILayout.Width(100));
                        break;
                    case AttributeType.Bool:
                        (Item as BoolAttribute).value = EditorGUILayout.Toggle((Item as BoolAttribute).value, GUILayout.Width(100));
                        break;
                }

                //ɾ����ť����
                if (GUILayout.Button(new GUIContent("ɾ��",deleteIcon),GUILayout.Width(50),GUILayout.Height(20)))
                {
                    deleteIndex = index;
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            //����״̬���и���
            if (deleteIndex != -1)
            {
                GUI.FocusControl("");
                curEdit.attributes.RemoveAt(deleteIndex);
                Repaint();
            }
            
        }
    }
}
