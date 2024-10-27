using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// �ɱ༭�ľ�̬�ֶ�/����
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EditableStaticAttribute : Attribute { }

    /// <summary>
    /// ���ɶ��ľ�̬�ֶ�/����
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ReadonlyStaticAttribute : Attribute { }

    /// <summary>
    /// �޲εľ�̬����
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class VoidStaticMethodAttribute : Attribute { }

    /// <summary>
    /// ׷�پ�̬��Ա����
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TrackStaticAttribute : Attribute { }

}