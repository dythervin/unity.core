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

        private Type FieldType
        {
            get
            {
                Type type = fieldInfo.FieldType;
                return type.IsArray ? type.GetElementType() : type;
            }
        }

        private void Init()
        {
            if (_initialized)
                return;


            Type type = FieldType;
            if (!type.IsGenericType)
                throw new ArgumentException("Type must be generic");

            if (type.GenericTypeArguments.Length != 1)
                throw new ArgumentException("Type must have only 1 generic parameter");

            if (!type.GenericTypeArguments[0].IsClass)
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
                Object newValue = EditorGUI.ObjectField(position, label, obj.objectReferenceValue, FieldType.GenericTypeArguments[0],
                    property.serializedObject.targetObject is Component);

                if (EditorGUI.EndChangeCheck() && (!newValue || Validate(newValue)))
                {
                    obj.objectReferenceValue = newValue;
                }
            }
            EditorGUI.EndProperty();
        }

        protected virtual bool Validate(Object obj)
        {
            return true;
        }
    }
}