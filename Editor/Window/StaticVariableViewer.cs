using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// һ������׷�پ�̬�������
    /// </summary>
    public class ClassOfStaticVariable
    {
        public List<SerializedObject> serialOb = new();
        public List<IBaseStaticObject> refs = new();
        public bool foldout = true;
    }

    
    /// <summary>
    /// ��̬������ʾ��
    /// </summary>
    public class StaticVariableViewer : EditorWindow
    {
        private Dictionary<Type, ClassOfStaticVariable> trackedClasses = new();

        /// <summary>
        /// ��ʾ����
        /// </summary>
        [MenuItem("Window/MikanLab/��̬��Ա������")]
        public static void ShowWindow()
        {
            GetWindow(typeof(StaticVariableViewer), false, "��̬��Ա������");
        }

        /// <summary>
        /// �ڼ����ʱ���ȡ
        /// </summary>
        public void OnEnable()
        {
            //��ʽ��С����
            minSize = new Vector2(500, 300);
            maxSize = new Vector2(1000, 600);
            EditorGUI.indentLevel++;

            List<System.Type> classesToShow = GetTargetClass();

            foreach (System.Type classType in classesToShow)
            {
                //��ȡ�����µ�����static�ֶ�
                FieldInfo[] fields = classType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic);
                PropertyInfo[] properties = classType.GetProperties(BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic);
                MethodInfo[] methods = classType.GetMethods(BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.NonPublic);

                //���ݱ�ǩ�ж��Ƿ�����ֶ�
                foreach (FieldInfo field in fields)
                {
                    if (Attribute.IsDefined(field, typeof(EditableStaticAttribute)))
                        trackedClasses[classType].refs.Add(new FieldReference(field, true));

                    if (Attribute.IsDefined(field, typeof(ReadonlyStaticAttribute)))
                        trackedClasses[classType].refs.Add(new FieldReference(field, false));
                }

                //���ݱ�ǩ�ж��Ƿ��������
                foreach (PropertyInfo property in properties)
                {
                    if (Attribute.IsDefined(property, typeof(EditableStaticAttribute)))
                        trackedClasses[classType].refs.Add(new PropertyReference(property, true));

                    if (Attribute.IsDefined(property, typeof(ReadonlyStaticAttribute)))
                        trackedClasses[classType].refs.Add(new PropertyReference(property, false));
                }

                //���ݱ�ǩ�Լ��Ƿ���Ҫ�����ж��Ƿ���뷽��
                foreach (MethodInfo method in methods)
                {
                    if (method.GetParameters().Length != 0) continue;

                    if (Attribute.IsDefined(method, typeof(VoidStaticMethodAttribute)))
                        trackedClasses[classType].refs.Add(new MethodReference(method));

                }
            }
        }

        /// <summary>
        /// �ڴ�������ʱ����
        /// </summary>
        public void OnDisable()
        {

        }

        /// <summary>
        /// ͨ��������������Ҫ׷�ٵ���
        /// </summary>
        /// <returns></returns>
        public List<System.Type> GetTargetClass()
        {
            //��ȡ��ǰʹ�õĳ����������������
            List<System.Type> classes = new List<System.Type>();
            Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            //������ȡ������׷�����Ե���
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    //������������Ծ͸���
                    if (Attribute.IsDefined(type, typeof(TrackStaticAttribute)))
                    {
                        classes.Add(type);
                        trackedClasses[type] = new();
                    }
                }
            }
            return classes;
        }

        /// <summary>
        /// ������ʾ����
        /// </summary>
        private void OnGUI()
        {
            #region ʵ������

            #endregion

            // ���ƾ����
            if (trackedClasses.Count == 0) EditorGUILayout.HelpBox("��ǰ��û���౻׷��", MessageType.Warning);


            foreach (var classItem in trackedClasses)
            {

                GUI.enabled = true;

                //����ÿһ�����Title
                classItem.Value.foldout = EditorGUILayout.Foldout(classItem.Value.foldout, classItem.Key.FullName + " : " + classItem.Value.refs.Count);

                //ÿһ�������һ���۵���
                if (classItem.Value.foldout)
                {

                    EditorGUI.indentLevel++;
                    GUILayout.BeginVertical("box");

                    //����ÿһ������
                    foreach (var item in classItem.Value.refs) item.Draw();

                    GUILayout.EndVertical();
                    EditorGUI.indentLevel--;
                }

            }
        }
    }
}