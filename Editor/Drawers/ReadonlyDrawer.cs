﻿using UnityEditor;
using UnityEngine;

namespace Dythervin.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadonlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using EditorGUI.DisabledScope disabledScope = new EditorGUI.DisabledScope(true);
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}