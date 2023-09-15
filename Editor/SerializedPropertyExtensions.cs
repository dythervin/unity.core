using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Dythervin.Core.Extensions;
using UnityEditor;

namespace Dythervin.Core.Editor
{
    public static class SerializedPropertyExtensions
    {
        public static object GetValue(this SerializedProperty property, bool getContainer = false)
        {
            return GetValue(property, out _, getContainer);
        }

        public static object GetValue(this SerializedProperty property, out FieldInfo fieldInfo,
            bool getContainer = false)
        {
            object instance = property.serializedObject.targetObject;
            fieldInfo = null;
            var paths = property.propertyPath.Split('.');
            for (int i = 0; i < paths.Length; i++)
            {
                var type = instance.GetType();
                string fieldName = paths[i];
                fieldInfo = type.GetFieldExt(fieldName,
                    BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public);

                if (fieldInfo == null)
                {
                    fieldInfo = null;
                    return null;
                }

                if (fieldInfo.FieldType.IsArray)
                {
                    //Skip "Array"
                    i += 2;
                    if (getContainer && i >= paths.Length - 1)
                        return instance;

                    Array array = (Array)fieldInfo.GetValue(instance);
                    var indexString = paths[i];
                    indexString = indexString.Remove("data[").Remove("]");
                    int index = int.Parse(indexString);
                    //Should be data['index']
                    instance = array.GetValue(index);
                }
                else
                {
                    if (getContainer && i >= paths.Length - 1)
                        return instance;

                    instance = fieldInfo.GetValue(instance);
                }
            }

            return instance;
        }

        public static void SetValue(this SerializedProperty property, object value)
        {
            object instance = property.serializedObject.targetObject;
            var paths = property.propertyPath.Split('.');
            for (int i = 0; i < paths.Length; i++)
            {
                var type = instance.GetType();
                string fieldName = paths[i];
                FieldInfo fieldInfo = type.GetFieldExt(fieldName,
                    BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public);

                if (fieldInfo == null)
                    return;

                if (fieldInfo.FieldType.IsArray)
                {
                    //Skip "Array"
                    i += 2;
                    Array array = (Array)fieldInfo.GetValue(instance);
                    var indexString = paths[i];
                    indexString = indexString.Remove("data[").Remove("]");
                    int index = int.Parse(indexString);
                    //Should be data['index']
                    if (i == paths.Length - 1)
                    {
                        array.SetValue(value, index);
                        return;
                    }

                    instance = array.GetValue(index);
                }
                else
                {
                    if (i == paths.Length - 1)
                    {
                        fieldInfo.SetValue(instance, value);
                    }

                    instance = fieldInfo.GetValue(instance);
                }
            }
        }

        public static void SetDefaultAutoValue(this SerializedProperty serializedProperty)
        {
            switch (serializedProperty.propertyType)
            {
                case SerializedPropertyType.Integer:
                    serializedProperty.intValue = default;
                    break;

                case SerializedPropertyType.Boolean:
                    serializedProperty.boolValue = default;
                    break;

                case SerializedPropertyType.Float:
                    serializedProperty.floatValue = default;
                    break;

                case SerializedPropertyType.String:
                    serializedProperty.stringValue = default;
                    break;

                case SerializedPropertyType.Color:
                    serializedProperty.colorValue = default;
                    break;

                case SerializedPropertyType.ObjectReference:
                    serializedProperty.objectReferenceValue = default;
                    break;

                case SerializedPropertyType.LayerMask:
                    serializedProperty.intValue = default;
                    break;

                case SerializedPropertyType.Enum:
                    serializedProperty.enumValueIndex = default;
                    serializedProperty.intValue = default;
                    break;

                case SerializedPropertyType.Vector2:
                    serializedProperty.vector2Value = default;
                    break;

                case SerializedPropertyType.Vector3:
                    serializedProperty.vector3Value = default;
                    break;

                case SerializedPropertyType.Vector4:
                    serializedProperty.vector4Value = default;
                    break;

                case SerializedPropertyType.Rect:
                    serializedProperty.rectValue = default;
                    break;

                case SerializedPropertyType.ArraySize:
                    serializedProperty.intValue = default;
                    break;

                case SerializedPropertyType.Character:
                    serializedProperty.intValue = default;
                    break;

                case SerializedPropertyType.AnimationCurve:
                    serializedProperty.animationCurveValue = default;
                    break;

                case SerializedPropertyType.Bounds:
                    serializedProperty.boundsValue = default;
                    break;

                case SerializedPropertyType.ExposedReference:
                    serializedProperty.exposedReferenceValue = default;
                    break;

                case SerializedPropertyType.Vector2Int:
                    serializedProperty.vector2IntValue = default;
                    break;

                case SerializedPropertyType.Vector3Int:
                    serializedProperty.vector3IntValue = default;
                    break;

                case SerializedPropertyType.RectInt:
                    serializedProperty.rectIntValue = default;
                    break;

                case SerializedPropertyType.BoundsInt:
                    serializedProperty.boundsIntValue = default;
                    break;

                case SerializedPropertyType.Gradient:
                case SerializedPropertyType.Generic:
                case SerializedPropertyType.Quaternion:
                case SerializedPropertyType.FixedBufferSize:
                case SerializedPropertyType.ManagedReference:
                case SerializedPropertyType.Hash128:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static object GetAutoValue(this SerializedProperty serializedProperty)
        {
            switch (serializedProperty.propertyType)
            {
                case SerializedPropertyType.Integer:
                    return serializedProperty.intValue;

                case SerializedPropertyType.Boolean:
                    return serializedProperty.boolValue;

                case SerializedPropertyType.Float:
                    return serializedProperty.floatValue;

                case SerializedPropertyType.String:
                    return serializedProperty.stringValue;

                case SerializedPropertyType.Color:
                    return serializedProperty.colorValue;

                case SerializedPropertyType.ObjectReference:
                    return serializedProperty.objectReferenceValue;

                case SerializedPropertyType.LayerMask:
                    return serializedProperty.intValue;

                case SerializedPropertyType.Enum:
                    return serializedProperty.enumValueIndex;

                case SerializedPropertyType.Vector2:
                    return serializedProperty.vector2Value;

                case SerializedPropertyType.Vector3:
                    return serializedProperty.vector3Value;

                case SerializedPropertyType.Vector4:
                    return serializedProperty.vector4Value;

                case SerializedPropertyType.Rect:
                    return serializedProperty.rectValue;

                case SerializedPropertyType.ArraySize:
                    return serializedProperty.intValue;

                case SerializedPropertyType.Character:
                    return serializedProperty.intValue;

                case SerializedPropertyType.AnimationCurve:
                    return serializedProperty.animationCurveValue;

                case SerializedPropertyType.Bounds:
                    return serializedProperty.boundsValue;

                case SerializedPropertyType.ExposedReference:
                    return serializedProperty.exposedReferenceValue;

                case SerializedPropertyType.Vector2Int:
                    return serializedProperty.vector2IntValue;

                case SerializedPropertyType.Vector3Int:
                    return serializedProperty.vector3IntValue;

                case SerializedPropertyType.RectInt:
                    return serializedProperty.rectIntValue;

                case SerializedPropertyType.BoundsInt:
                    return serializedProperty.boundsIntValue;

                case SerializedPropertyType.Gradient:
                case SerializedPropertyType.Generic:
                case SerializedPropertyType.Quaternion:
                case SerializedPropertyType.FixedBufferSize:
                case SerializedPropertyType.ManagedReference:
                case SerializedPropertyType.Hash128:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}