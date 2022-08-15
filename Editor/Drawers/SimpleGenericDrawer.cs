using System;
using Dythervin.Core.Attributes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dythervin.Core.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(SimpleGenericAttribute))]
    public class SimpleGenericDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 20;
        }

        private bool _initialized;

        private void Init()
        {
            if (_initialized)
                return;


            if (!fieldInfo.FieldType.IsGenericType)
                throw new ArgumentException("Type must be generic");

            if (fieldInfo.FieldType.GenericTypeArguments.Length != 1)
                throw new ArgumentException("Type must have only 1 generic parameter");

            if (!fieldInfo.FieldType.GenericTypeArguments[0].IsClass)
                throw new ArgumentException("Generic parameter of a type must be class");

            _initialized = true;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init();
            property.Next(true);
            SerializedProperty obj = property.Copy();
            EditorGUI.BeginProperty(position, label, obj);
            {
                EditorGUI.BeginChangeCheck();
                Object newValue = EditorGUI.ObjectField(position, label, obj.objectReferenceValue, fieldInfo.FieldType.GenericTypeArguments[0],
                    property.serializedObject.targetObject is Component);

                if (EditorGUI.EndChangeCheck() && (!newValue || Validate(newValue)))
                    obj.objectReferenceValue = newValue;
            }
            EditorGUI.EndProperty();
        }

        protected virtual bool Validate(Object obj)
        {
            return true;
        }
    }
}