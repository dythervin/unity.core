using UnityEditor;
using UnityEngine;

namespace Dythervin.Editor
{
    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public class HideIfDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return IsConditionMet(property) ? 0 : EditorGUI.GetPropertyHeight(property);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsConditionMet(property))
            {
                return;
            }

            EditorGUI.PropertyField(position, property, label, true);
        }

        private bool IsConditionMet(SerializedProperty serializedProperty)
        {
            if (attribute is not HideIfAttribute conditionAttribute)
            {
                return false;
            }

            string condition = conditionAttribute.value;
            return serializedProperty.CheckCondition(condition);
        }
    }
}