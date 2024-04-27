namespace Dythervin.Editor
{
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return IsConditionMet(property) ? EditorGUI.GetPropertyHeight(property) : 0;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsConditionMet(property))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        private bool IsConditionMet(SerializedProperty serializedProperty)
        {
            ShowIfAttribute? conditionAttribute = attribute as ShowIfAttribute;
            if (conditionAttribute == null)
            {
                return false;
            }

            string condition = conditionAttribute.value;
            return serializedProperty.CheckCondition(condition);
        }
    }
}