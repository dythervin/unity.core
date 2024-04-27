using System;
using UnityEditor;
using UnityEngine;

namespace Dythervin.Editor
{
    [CustomPropertyDrawer(typeof(SimpleGenericAttribute))]
    public abstract class SimpleGenericDrawerBase : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 20;
        }

        private bool _initialized;

        protected Type FieldType
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

            PreInitCheck(type);

            _initialized = true;
        }

        protected virtual void PreInitCheck(Type type)
        {
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init();
            property.Next(true);
            SerializedProperty obj = property.Copy();
            EditorGUI.BeginProperty(position, label, obj);
            {
                DrawGui(position, property, label, obj);
            }
            EditorGUI.EndProperty();
        }

        protected abstract void DrawGui(Rect position, SerializedProperty property, GUIContent label, SerializedProperty obj);
    }
}