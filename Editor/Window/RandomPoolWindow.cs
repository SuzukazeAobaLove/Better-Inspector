using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using JetBrains.Annotations;
using System;
using System.IO.Pipes;

namespace MikanLab
{
    public class RandomPoolWindow : EditorWindow
    {
        private static readonly string prefKey = "MikanLab_RandomPoolWindow";
        private bool isOpenPropertyWindow = false;
        private RandomPool target;
        private Setting setting;

        #region 偏好设置
        [Serializable]
        public class Setting
        {
            public float width = 500f;
            public float height = 300f;
            public float position_x = 0f;
            public float position_y = 0f;
            public bool propertyWindowOn = false;
        }
        #endregion

        #region 生命周期
        public static void Invoke(RandomPool target)
        {
            var window = GetWindow<RandomPoolWindow>("RandomPoolWindow");
            window.target = target;

            if (!EditorPrefs.HasKey(prefKey)) window.setting = new();
            else window.setting = JsonUtility.FromJson<Setting>(EditorPrefs.GetString(prefKey));
            window.FromPref();
            window.AddElements();
        }

        private void OnDestroy()
        {
            SavePref();
            SaveGraph();
        }

        private void OnDisable()
        {
            SaveGraph();
        }
        #endregion

        #region 元素组件
        private RandomPoolGraph graph;
        private VisualElement Graph
        {
            get
            {
                if (graph == null)
                {
                    graph = new RandomPoolGraph(target) { style = { flexGrow = 1 } };
                    graph.LoadFromAsset();
                }
                return graph;
            }

        }

        private PropertyWindow propertyWindow;
        private PropertyWindow PropertyWindow
        {
            get
            {
                if (propertyWindow == null)
                {
                    propertyWindow = new PropertyWindow();
                }
                return propertyWindow;
            }
        }

        #endregion

        #region 绘制控制
        private void AddElements()
        {
            var propertyWindow = PropertyWindow;
            rootVisualElement.Add(new Button(propertyWindow.Reverse) { text = "属性" });
            //rootVisualElement.Add(new Button(graphView.Execute) { text = "Execute" });

            rootVisualElement.Add(PropertyWindow);
            rootVisualElement.Add(Graph);

        }

        private void FromPref()
        {
            position = new(setting.position_x, setting.position_y, setting.width, setting.height);
        }
        #endregion

        #region 保存
        private void SaveGraph()
        {
            if (target == null) return;
            if (graph == null) return;

            graph.SaveChangeToAsset();
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(target));
        }

        private void SavePref()
        {
            setting.width = position.width;
            setting.height = position.height;
            setting.position_x = position.x;
            setting.position_y = position.y;
            setting.propertyWindowOn = isOpenPropertyWindow;
            EditorPrefs.SetString(prefKey, JsonUtility.ToJson(setting));
        }
        #endregion
    }
}