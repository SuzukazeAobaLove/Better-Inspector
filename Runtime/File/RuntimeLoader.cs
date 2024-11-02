using MikanLab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���м����������ɶ�����д
/// </summary>
public class RuntimeLoader : MonoBehaviour 
{
    private Dictionary<string, UnityEngine.Object> resourceCache = new Dictionary<string, UnityEngine.Object>();

    #region ����ģʽ
    private static RuntimeLoader instance;
    public static RuntimeLoader Instance { 
        get
        {
            if(instance == null)
            {
                var gameobject = new GameObject("RuntimeLoader");
                DontDestroyOnLoad(gameobject);
                instance = gameobject.AddComponent<RuntimeLoader>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    #endregion

    /// <summary>
    /// ������Դ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        if (!resourceCache.ContainsKey(path) || resourceCache[path] == null)
        {
            resourceCache[path] = Instantiate(Resources.Load<T>(path));
        }
        return (T)resourceCache[path];
    }
}
