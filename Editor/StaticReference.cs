using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// ������̬����
    /// </summary>
    public interface IBaseStaticObject
    {
        /// <summary>
        /// �Ƿ�ɱ༭
        /// </summary>
        public bool IfEditable { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// ��������
        /// </summary>
        public void Draw();

        /// <summary>
        /// ��ȡ��Ӧ�������͵Ļ�����ɫ
        /// </summary>
        /// <returns></returns>
        public Color GetAccessColor();

        /// <summary>
        /// ��ȡ��Ӧ�������͵��ı�
        /// </summary>
        /// <returns></returns>
        public string GetAccessText();
    }

    /// <summary>
    /// ��̬�ֶ�����
    /// </summary>
    public class FieldReference : IBaseStaticObject
    {
        public FieldInfo FieldInfo { get; private set; }

        public bool IfEditable { get; set; }

        public Type ValueType { get; set; }

        public FieldReference(FieldInfo fieldInfo, bool editable)
        {
            FieldInfo = fieldInfo;
            ValueType = fieldInfo.FieldType;
            IfEditable = editable;
        }

        public string Name => FieldInfo.Name;

        public object Value => FieldInfo.GetValue(null);

        public void SetValue(object value) => FieldInfo.SetValue(null, value);

        public Color GetAccessColor()
        {
            if (FieldInfo.IsPublic) return Color.gray;
            if (FieldInfo.IsPrivate) return Color.red;
            if (FieldInfo.IsFamily) return Color.cyan;
            if (FieldInfo.IsAssembly) return Color.blue;
            if (FieldInfo.IsFamilyOrAssembly) return Color.green;
            else return Color.magenta;
        }

        public string GetAccessText()
        {
            if (FieldInfo.IsPublic) return "public";
            if (FieldInfo.IsPrivate) return "private";
            if (FieldInfo.IsFamily) return "protected";
            if (FieldInfo.IsAssembly) return "internal";
            if (FieldInfo.IsFamilyOrAssembly) return "protected internal";
            else return "unknown";
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal();
            GUI.enabled = IfEditable;
            if (!Application.isPlaying) GUI.enabled = false;

            //���ƶ����������
            GUI.contentColor = GetAccessColor();
            EditorGUILayout.LabelField(GetAccessText(), GUILayout.Width(100));
            GUI.contentColor = Color.white;


            if (ValueType == typeof(int))
            {
                int newValue = EditorGUILayout.IntField(Name, (int)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(float))
            {
                float newValue = EditorGUILayout.FloatField(Name, (float)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(double))
            {
                double newValue = EditorGUILayout.DoubleField(Name, (double)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(long))
            {
                long newValue = EditorGUILayout.LongField(Name, (long)Value);
                SetValue(newValue);
            }

            // ��������
            else if (ValueType == typeof(bool))
            {
                bool newValue = EditorGUILayout.Toggle(Name, (bool)Value);
                SetValue(newValue);
            }

            // �ַ�������
            else if (ValueType == typeof(string))
            {
                string newValue = EditorGUILayout.TextField(Name, (string)Value);
                SetValue(newValue);
            }

            // ö������
            else if (ValueType.IsEnum)
            {
                Enum newValue = EditorGUILayout.EnumPopup(Name, (Enum)Value);
                SetValue(newValue);
            }

            // Unity �ض�����
            else if (ValueType == typeof(Vector2))
            {
                Vector2 newValue = EditorGUILayout.Vector2Field(Name, (Vector2)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Vector3))
            {
                Vector3 newValue = EditorGUILayout.Vector3Field(Name, (Vector3)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Vector4))
            {
                Vector4 newValue = EditorGUILayout.Vector4Field(Name, (Vector4)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Color))
            {
                Color newValue = EditorGUILayout.ColorField(Name, (Color)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Rect))
            {
                Rect newValue = EditorGUILayout.RectField(Name, (Rect)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(AnimationCurve))
            {
                AnimationCurve newValue = EditorGUILayout.CurveField(Name,
                                                                  (AnimationCurve)Value);
                SetValue(newValue);
            }

            // ��������
            else
            {

                EditorGUILayout.HelpBox("�ݲ�֧���Զ������ͻ���",MessageType.Warning);
            }
            GUILayout.EndHorizontal();
        }
    }

    /// <summary>
    /// ��̬��������
    /// </summary>
    public class PropertyReference : IBaseStaticObject
    {
        public PropertyInfo PropertyInfo { get; private set; }

        public bool IfEditable { get; set; }

        public Type ValueType { get; set; }

        public PropertyReference(PropertyInfo propertyInfo, bool editable)
        {
            PropertyInfo = propertyInfo;
            ValueType = propertyInfo.PropertyType;
            IfEditable = editable;

        }
        public string Name => PropertyInfo.Name;

        public object Value => PropertyInfo.GetValue(null);

        public void SetValue(object value) => PropertyInfo.SetValue(null, value);

        public Color GetAccessColor()
        {
            if (PropertyInfo.SetMethod != null)
            {
                if (PropertyInfo.SetMethod.IsPublic) return Color.gray;
                if (PropertyInfo.SetMethod.IsPrivate) return Color.red;
                if (PropertyInfo.SetMethod.IsFamily) return Color.cyan;
                if (PropertyInfo.SetMethod.IsAssembly) return Color.blue;
                if (PropertyInfo.SetMethod.IsFamilyOrAssembly) return Color.green;
                else return Color.magenta;
            }
            else if (PropertyInfo.GetMethod != null)
            {
                if (PropertyInfo.GetMethod.IsPublic) return Color.gray;
                if (PropertyInfo.GetMethod.IsPrivate) return Color.red;
                if (PropertyInfo.GetMethod.IsFamily) return Color.cyan;
                if (PropertyInfo.GetMethod.IsAssembly) return Color.blue;
                if (PropertyInfo.GetMethod.IsFamilyOrAssembly) return Color.green;
                else return Color.magenta;
            }
            else return Color.green;
        }

        public string GetAccessText()
        {
            //����ɱ༭�Ҵ���Set������������SetΪ��
            if (IfEditable && PropertyInfo.SetMethod != null)
            {
                if (PropertyInfo.SetMethod.IsPublic) return "public set";
                if (PropertyInfo.SetMethod.IsPrivate) return "private set";
                if (PropertyInfo.SetMethod.IsFamily) return "protected set";
                if (PropertyInfo.SetMethod.IsAssembly) return "internal set";
                if (PropertyInfo.SetMethod.IsFamilyOrAssembly) return "protected internal set";
                else return "unknown set";
            }
            else if (PropertyInfo.GetMethod != null)
            {
                if (PropertyInfo.GetMethod.IsPublic) return "public get";
                if (PropertyInfo.GetMethod.IsPrivate) return "private get";
                if (PropertyInfo.GetMethod.IsFamily) return "protected get";
                if (PropertyInfo.GetMethod.IsAssembly) return "internal get";
                if (PropertyInfo.GetMethod.IsFamilyOrAssembly) return "protected internal get";
                else return "unknown get";
            }
            else return "public get/set";
        }

        public void Draw()
        {

            GUILayout.BeginHorizontal();
            GUI.enabled = IfEditable;
            if (!Application.isPlaying) GUI.enabled = false;

            //���ƶ����������
            GUI.contentColor = GetAccessColor();
            EditorGUILayout.LabelField(GetAccessText(), GUILayout.Width(100));
            GUI.contentColor = Color.white;


            if (ValueType == typeof(int))
            {
                int newValue = EditorGUILayout.IntField(Name, (int)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(float))
            {
                float newValue = EditorGUILayout.FloatField(Name, (float)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(double))
            {
                double newValue = EditorGUILayout.DoubleField(Name, (double)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(long))
            {
                long newValue = EditorGUILayout.LongField(Name, (long)Value);
                SetValue(newValue);
            }

            // ��������
            else if (ValueType == typeof(bool))
            {
                bool newValue = EditorGUILayout.Toggle(Name, (bool)Value);
                SetValue(newValue);
            }

            // �ַ�������
            else if (ValueType == typeof(string))
            {
                string newValue = EditorGUILayout.TextField(Name, (string)Value);
                SetValue(newValue);
            }

            // ö������
            else if (ValueType.IsEnum)
            {
                Enum newValue = EditorGUILayout.EnumPopup(Name, (Enum)Value);
                SetValue(newValue);
            }

            // Unity �ض�����
            else if (ValueType == typeof(Vector2))
            {
                Vector2 newValue = EditorGUILayout.Vector2Field(Name, (Vector2)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Vector3))
            {
                Vector3 newValue = EditorGUILayout.Vector3Field(Name, (Vector3)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Vector4))
            {
                Vector4 newValue = EditorGUILayout.Vector4Field(Name, (Vector4)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Color))
            {
                Color newValue = EditorGUILayout.ColorField(Name, (Color)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Rect))
            {
                Rect newValue = EditorGUILayout.RectField(Name, (Rect)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(AnimationCurve))
            {
                AnimationCurve newValue = EditorGUILayout.CurveField(Name,
                                                                  (AnimationCurve)Value);
                SetValue(newValue);
            }

            // ��������
            else
            {
                EditorGUILayout.LabelField(Name, Value.ToString());
            }
            GUILayout.EndHorizontal();
        }
    }


    public class MethodReference : IBaseStaticObject
    {
        public MethodInfo MethodInfo { get; private set; }

        public bool IfEditable { get; set; }
        public MethodReference(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
            IfEditable = true;
        }

        public string Name => MethodInfo.Name;

        public Color GetAccessColor()
        {
            if (MethodInfo.IsPublic) return Color.gray;
            if (MethodInfo.IsPrivate) return Color.red;
            if (MethodInfo.IsFamily) return Color.cyan;
            if (MethodInfo.IsAssembly) return Color.blue;
            if (MethodInfo.IsFamilyOrAssembly) return Color.green;
            else return Color.magenta;
        }

        public string GetAccessText()
        {
            if (MethodInfo.IsPublic) return "public";
            if (MethodInfo.IsPrivate) return "private";
            if (MethodInfo.IsFamily) return "protected";
            if (MethodInfo.IsAssembly) return "internal";
            if (MethodInfo.IsFamilyOrAssembly) return "protected internal";
            else return "unknown";
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal();
            GUI.enabled = IfEditable;
            if (!Application.isPlaying) GUI.enabled = false;

            //���ƶ����������
            GUI.contentColor = GetAccessColor();
            EditorGUILayout.LabelField(GetAccessText(), GUILayout.Width(100));
            GUI.contentColor = Color.white;

            EditorGUILayout.LabelField(Name, GUILayout.Width(100));
            if (GUILayout.Button("����", GUILayout.Width(100)))
            {
                MethodInfo.Invoke(null, null);
            };
            GUILayout.EndHorizontal();
        }
    }
}