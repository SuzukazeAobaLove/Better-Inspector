using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ���ʳ�
/// </summary>
public class ProbabilityPool : ScriptableObject
{

    /// <summary>
    /// �����
    /// </summary>
    List<int> pool;

    /// <summary>
    /// ������������
    /// </summary>
    class Item
    {
        public int weight;
        public string item;
    }

    int sum = 0;

    /// <summary>
    /// ����ʱ������
    /// </summary>
    /// <param name="weight">Ȩ��</param>
    /// <param name="item">��ƷID</param>
    public void Add(int weight, int item)
    {

    }

    /// <summary>
    /// ����ʱ�޸���
    /// </summary>
    /// <param name="weight"></param>
    /// <param name="item"></param>
    public void Edit(int weight, int item)
    {

    }

    /// <summary>
    /// ����ʱɾ����
    /// </summary>
    /// <param name="weight"></param>
    /// <param name="item"></param>
    public void Remove(int weight, int item)
    {

    }
}
