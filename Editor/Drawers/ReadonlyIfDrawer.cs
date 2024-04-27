using UnityEditor;
using UnityEngine;

namespace Dythervin.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyIfAttribute))]
    public class ReadonlyIfDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsConditionMet(property))
            {
                using (EditorGUI.DisabledScope disabledScope = new EditorGUI.DisabledScope(true))
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
            }
            else
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        private bool IsConditionMet(SerializedProperty serializedProperty)
        {
            ReadOnlyIfAttribute? conditionAttribute = attribute as ReadOnlyIfAttribute;
            if (conditionAttribute == null)
            {
                return false;
            }

            string condition = conditionAttribute.value;
            return serializedProperty.CheckCondition(condition);
        }
    }
}