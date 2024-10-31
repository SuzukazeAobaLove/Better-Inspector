using System;


namespace MikanLab
{
    /// <summary>
    /// �ɱ༭�ľ�̬�ֶ�/����
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EditableStaticAttribute : System.Attribute { }

    /// <summary>
    /// ���ɶ��ľ�̬�ֶ�/����
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ReadonlyStaticAttribute : System.Attribute { }

    /// <summary>
    /// �޲εľ�̬����
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class VoidStaticMethodAttribute : System.Attribute { }

    /// <summary>
    /// ׷�پ�̬��Ա����
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TrackStaticAttribute : System.Attribute { }

}