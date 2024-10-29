using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    
    [CreateAssetMenu(fileName = "MAR", menuName = "MikanLab/��������Դ")]
    /// <summary>
    /// ���Դ洢�������Ե���Դ�ļ�
    /// </summary>
    public class MultiAttributeResource : ScriptableObject
    {
        [SerializeField]public List<Attribute> attributes = new();

        /// <summary>
        /// ���ͷ���
        /// </summary>
        public enum Type
        {
            String, Int, Float, Bool, Enum, Array
        }

        [Serializable]
        /// <summary>
        /// ��������
        /// </summary>
        public class Attribute
        {
            /// <summary>
            /// ʵ������
            /// </summary>
            public System.Type realType;

            [SerializeField]
            /// <summary>
            /// ����ö��
            /// </summary>
            public Type typeEnum;

            [SerializeField]
            /// <summary>
            /// ������
            /// </summary>
            public string name;

            [SerializeReference]
            /// <summary>
            /// ����ֵ
            /// </summary>
            public object[] value = new object[] { "1" };


            /// <summary>
            /// �������캯��
            /// </summary>
            /// <param name="name">������</param>
            /// <param name="value">����ֵ</param>
            /// <param name="type">��������</param>
            /// <exception cref="System.Exception">����Ϊ����</exception>
            public Attribute(string name,object value,System.Type type)
            {

                //�ж�����
                if(type.IsArray) throw new System.Exception("Use the Array Form of Constructor Instead");

                //�ж�ö��
                if(type.IsEnum)
                {
                    this.typeEnum = Type.Enum;
                    this.realType = type;
                }
                //�жϻ�������
                else
                {
                    if (type == typeof(int))
                    {
                        this.realType = typeof(int);
                        this.typeEnum = Type.Int;
                    }
                    else if(type == typeof(float))
                    {
                        this.realType = typeof(float);
                        this.typeEnum = Type.Float;
                    }
                    else if(type == typeof(bool))
                    {
                        this.realType = typeof(bool);
                        this.typeEnum = Type.Bool;
                    }
                    else if(type == typeof(string))
                    {
                        this.realType= typeof(string);
                        this.typeEnum= Type.String;
                    }
                }

                this.name = name;
                this.value = new object[1];
                this.value[0] = value;
            }

            /// <summary>
            /// ���鹹�캯��
            /// </summary>
            /// <param name="name">������</param>
            /// <param name="value">����ֵ</param>
            /// <param name="type">Ԫ��������</param>
            /// <exception cref="System.Exception">����Ϊ������</exception>
            public Attribute(string name, object[] value, System.Type type)
            {
                if (!type.IsArray) throw new System.Exception("Use the Single Form of Constructor Instead");

                this.name =name;
                this.realType = type.GetElementType();
                typeEnum = Type.Array;
                this.value = value;
            }

        }

        /// <summary>
        /// �������͵ػ�ȡֵ
        /// </summary>
        /// <param name="Name">��������</param>
        /// <returns>ֵ����</returns>
        public object GetValue(string Name)
        {
            foreach (Attribute a in attributes)
            {
                if (a.name == Name) return a.typeEnum == Type.Array ? a.value : a.value[0];
            }
            throw new System.Exception($"Attibute {Name} Not Found");
        }

        /// <summary>
        /// ��ȡһ���ַ���
        /// </summary>
        /// <param name="Name">��������</param>
        /// <returns>stringֵ</returns>
        public string GetString(string Name)
        {
            foreach (Attribute a in attributes)
            {
                if (a.name == Name)
                {
                    if (a.typeEnum == Type.String) return a.value[0] as string;
                    else throw new System.Exception($"Type of {Name} Dosen't Match {a.typeEnum}");
                }
            }
            throw new System.Exception($"Attibute {Name} Not Found");
        }

        /// <summary>
        /// ��ȡһ��floatֵ
        /// </summary>
        /// <param name="Name">��������</param>
        /// <returns>floatֵ</returns>
        public float GetFloat(string Name)
        {
            foreach (Attribute a in attributes)
            {
                if (a.name == Name)
                {
                    if (a.typeEnum == Type.Float) return (float) a.value[0];
                    else throw new System.Exception($"Type of {Name} Dosen't Match {a.typeEnum}");
                }
            }
            throw new System.Exception($"Attibute {Name} Not Found");
        }

        /// <summary>
        /// ��ȡһ��intֵ
        /// </summary>
        /// <param name="Name">��������</param>
        /// <returns>intֵ</returns>
        public int GetInt(string Name)
        {
            foreach (Attribute a in attributes)
            {
                if (a.name == Name)
                {
                    if (a.typeEnum == Type.Int) return (int)a.value[0];
                    else throw new System.Exception($"Type of {Name} Dosen't Match {a.typeEnum}");
                }
            }
            throw new System.Exception($"Attibute {Name} Not Found");
        }

        /// <summary>
        /// ��ȡһ��boolֵ
        /// </summary>
        /// <param name="Name">��������</param>
        /// <returns>boolֵ</returns>
        public bool GetBool(string Name)
        {
            foreach (Attribute a in attributes)
            {
                if (a.name == Name)
                {
                    if (a.typeEnum == Type.Bool) return (bool)a.value[0];
                    else throw new System.Exception($"Type of {Name} Dosen't Match {a.typeEnum}");
                }
            }
            throw new System.Exception($"Attibute {Name} Not Found");
        }
    }
}