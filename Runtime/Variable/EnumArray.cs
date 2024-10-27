using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    /// <summary>
    /// ö������
    /// </summary>
    /// <typeparam name="TEnum">ö������</typeparam>
    /// <typeparam name="TValue">ֵ����</typeparam>
    [Serializable]
    public class EnumArray<TEnum, TValue> where TEnum : Enum
    {
        /// <summary>
        /// ʵ������
        /// </summary>
        [SerializeField] private TValue[] array;
        
        /// <summary>
        /// ��Ӧ������
        /// </summary>
        static private Dictionary<TEnum, int> CorInd;

        static EnumArray()
        {
            CorInd = new Dictionary<TEnum, int>();
            int ind = 0;
            foreach (TEnum EnumValue in Enum.GetValues(typeof(TEnum)))
            {
                CorInd[EnumValue] = ind;
                ind++;
            }
        }

        public EnumArray()
        {
            int size = Enum.GetValues(typeof(TEnum)).Length;
            array = new TValue[size];
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="_enum">ö��ֵ</param>
        /// <returns>��Ӧֵ</returns>
        public TValue this[TEnum _enum]
        {
            get => array[CorInd[_enum]]; 
            set => array[CorInd[_enum]] = value;
        }


    }

    /// <summary>
    /// ��άö������
    /// </summary>
    /// <typeparam name="TEnum1">ö��һ</typeparam>
    /// <typeparam name="TEnum2">ö�ٶ�</typeparam>
    /// <typeparam name="TValue">ֵ����</typeparam>
    public class DEnumArray<TEnum1, TEnum2, TValue>
    where TEnum1 : Enum
    where TEnum2 : Enum
    {
        private TValue[] array;

        public DEnumArray()
        {
            int size1 = Enum.GetValues(typeof(TEnum1)).Length;
            int size2 = Enum.GetValues(typeof(TEnum2)).Length;
            array = new TValue[size1 * size2];
        }

        public TValue this[TEnum1 enum1, TEnum2 enum2]
        {
            get => array[CalculateIndex(enum1, enum2)]; 
            set => array[CalculateIndex(enum1, enum2)] = value;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="enum1">ö��ֵ1</param>
        /// <param name="enum2">ö��ֵ2</param>
        /// <returns>ʵ������</returns>
        private int CalculateIndex(TEnum1 enum1, TEnum2 enum2)
        {
            int index1 = Convert.ToInt32(enum1);
            int index2 = Convert.ToInt32(enum2);
            int size2 = Enum.GetValues(typeof(TEnum2)).Length;
            return index1 * size2 + index2;
        }
    }
}