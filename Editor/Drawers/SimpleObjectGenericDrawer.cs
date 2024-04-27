using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dythervin.Editor
{
    [CustomPropertyDrawer(typeof(SimpleGenericAttribute))]
    public class SimpleObjectGenericDrawer : SimpleGenericDrawerBase
    {
        protected override void PreInitCheck(Type type)
        {
            base.PreInitCheck(type);
            if (!type.GenericTypeArguments[0].IsClass)
                throw new ArgumentException("Generic parameter of a type must be class");
        }

        protected override void DrawGui(Rect position, SerializedProperty property, GUIContent label, SerializedProperty obj)
        {
            EditorGUI.BeginChangeCheck();
            Object newValue = EditorGUI.ObjectField(position,
                label,
                obj.objectReferenceValue,
                FieldType.GenericTypeArguments[0],
                property.serializedObject.targetObject is Component);

            if (EditorGUI.EndChangeCheck() && (!newValue || Validate(newValue)))
            {
                obj.objectReferenceValue = newValue;
            }
        }

        protected virtual bool Validate(Object obj)
        {
            return true;
        }
    }
}