using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    [CustomPropertyDrawer(typeof(BaseNode),true)]
    public class BaseNodeDrawer : PropertyDrawer
    {
        bool fold = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            
            EditorGUI.BeginProperty(position, label, property);
            position.height = EditorGUIUtility.singleLineHeight;
            
            fold = EditorGUI.Foldout(position, fold, label);
            if (fold)
            {
                EditorGUI.indentLevel++;

                position.height = EditorGUIUtility.singleLineHeight;
                position.y += EditorGUIUtility.singleLineHeight;
                position.y += EditorGUIUtility.standardVerticalSpacing;

                var inputPorts = property.FindPropertyRelative("InputPorts");
                var outputPorts = property.FindPropertyRelative("OutputPorts");
                
                EditorGUI.PropertyField(position, inputPorts,new GUIContent("输入端口"));
                position.y += EditorGUI.GetPropertyHeight(inputPorts);
                position.y += EditorGUIUtility.standardVerticalSpacing;

                EditorGUI.PropertyField(position, outputPorts,new GUIContent("输出端口"));
                position.y += EditorGUI.GetPropertyHeight(outputPorts);
                position.y += EditorGUIUtility.standardVerticalSpacing;
                
                var name = property.FindPropertyRelative("NodeName");
                EditorGUI.PropertyField(position, name, new GUIContent("名称"));
                
                
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndProperty();
            
            /*
            input = EditorGUILayout.Foldout(input, new GUIContent("输入端口"),GUIUtilities.BoldFoldout);
            if (input)
            {
                EditorGUI.indentLevel++;
                var indentRect = EditorGUI.IndentedRect(position);

                DrawPortDict(inputPorts);

                EditorGUI.indentLevel--;
            }
            /*
            
            output = EditorGUILayout.Foldout(output, new GUIContent("输出端口"), GUIUtilities.BoldFoldout);
            if (output)
            {
                EditorGUI.indentLevel++;
                var indentRect = EditorGUI.IndentedRect(position);

                DrawPortDict(outputPorts);

                EditorGUI.indentLevel--;
            }
            */
            
        }

        public void DrawPortDict(SerializedProperty property)
        {
            var keys = property.FindPropertyRelative("keys");
            var values = property.FindPropertyRelative("values");

            for (int i = 0; i < keys.arraySize; ++i)
            {
                var key = keys.GetArrayElementAtIndex(i);
                var value = values.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(value,new GUIContent(key.stringValue));
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var inputPorts = property.FindPropertyRelative("InputPorts");
            var outputPorts = property.FindPropertyRelative("OutputPorts");

            // 计算总高度
            float totalHeight = 0;

            totalHeight += EditorGUIUtility.singleLineHeight;
            
            if (fold)
            {
                totalHeight += EditorGUIUtility.singleLineHeight;
                totalHeight += EditorGUIUtility.standardVerticalSpacing * 3;
                totalHeight += EditorGUI.GetPropertyHeight(inputPorts, GUIContent.none);
                totalHeight += EditorGUI.GetPropertyHeight(outputPorts, GUIContent.none);
            }
            
            return totalHeight;
        }
    }

    

}