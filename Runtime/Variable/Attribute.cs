using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// ���ͷ���
    /// </summary>
    public enum AttributeType
    {
        String, Int, Float, Bool, Enum, Array
    }

    [Serializable]
    /// <summary>
    /// ��������
    /// </summary>
    public abstract class BaseAttribute
    {

        [SerializeField]
        /// <summary>
        /// ����ö��
        /// </summary>
        public AttributeType typeEnum;

        [SerializeField]
        /// <summary>
        /// ������
        /// </summary>
        public string name;

    }

    [Serializable]
    public class StringAttribute: BaseAttribute
    {
        [SerializeField]
        /// <summary>
        /// ʵ��ֵ
        /// </summary>
        public string value;

        public StringAttribute()
        {
            typeEnum = AttributeType.String;
            value = string.Empty;
            name = "default";
        }

        public string GetString() => value;
    }

    [Serializable]
    public class FloatAttribute : BaseAttribute
    {
        [SerializeField]
        /// <summary>
        /// ʵ��ֵ
        /// </summary>
        public float value;

        public FloatAttribute()
        {
            typeEnum = AttributeType.Float;
            value = 0;
            name = "default";
        }

        public float GetFloat() => value;
    }

    [Serializable]
    public class IntAttribute : BaseAttribute
    {
        [SerializeField]
        /// <summary>
        /// ʵ��ֵ
        /// </summary>
        public int value;

        public IntAttribute()
        {
            typeEnum = AttributeType.Int;
            value = 0;
            name = "default";
        }

        public int GetInt() => value;
    }

    [Serializable]
    public class BoolAttribute : BaseAttribute
    {
        [SerializeField]
        /// <summary>
        /// ʵ��ֵ
        /// </summary>
        public bool value;

        public BoolAttribute()
        {
            typeEnum = AttributeType.Bool;
            value = false;
            name = "default";
        }

        public bool GetBool() => value;
    }
}


