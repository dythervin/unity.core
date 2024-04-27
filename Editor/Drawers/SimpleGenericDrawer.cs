using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dythervin.Editor
{
    public class SimpleGenericDrawer : SimpleGenericDrawerBase
    {
        protected override void PreInitCheck(Type type)
        {
            base.PreInitCheck(type);
            if (type.GenericTypeArguments[0].Is<Object>())
                throw new ArgumentException("Generic parameter can't inherit UnityEngine.Object");
        }

        protected override void DrawGui(Rect position, SerializedProperty property, GUIContent label, SerializedProperty obj)
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}