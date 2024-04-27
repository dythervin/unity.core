using System;
using System.Reflection;
using UnityEditor;

namespace Dythervin.Editor
{
    public static class SerializedPropertyExtensions
    {
        public static object GetValue(this SerializedProperty property)
        {
            return GetValue(property, out _, out _);
        }

        public static object GetValue(this SerializedProperty property, out object obj)
        {
            return GetValue(property, out _, out obj);
        }

        public static object GetValue(this SerializedProperty property, out FieldInfo fieldInfo, out object obj)
        {
            obj = property.serializedObject.targetObject;
            fieldInfo = null;
            var paths = property.propertyPath.Split('.');
            for (int i = 0; i < paths.Length; i++)
            {
                var type = obj.GetType();
                string fieldName = paths[i];
                fieldInfo = type.GetFieldExt(fieldName,
                    BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic |
                    BindingFlags.Public);

                if (fieldInfo == null)
                {
                    fieldInfo = null;
                    return null;
                }

                if (fieldInfo.FieldType.IsArray)
                {
                    //Skip "Array"
                    i += 2;
                    // if (getContainer && i >= paths.Length - 1)
                    //     return instance;

                    Array array = (Array)(obj = fieldInfo.GetValue(obj));
                    var indexString = paths[i];
                    indexString = indexString.Remove("data[").Remove("]");
                    int index = int.Parse(indexString);
                    //Should be data['index']
                    if (i >= paths.Length - 1)
                        return array.GetValue(index);
                }
                else
                {
                    // if (getContainer && i >= paths.Length - 1)
                    //     return instance;

                    if (i >= paths.Length - 1)
                        return fieldInfo.GetValue(obj);
                }
            }

            return property.serializedObject.targetObject;
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
                    BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic |
                    BindingFlags.Public);

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
                    serializedProperty.gradientValue = default;
                    break;
                case SerializedPropertyType.Generic:
                    serializedProperty.boxedValue = default;
                    break;
                case SerializedPropertyType.Quaternion:

                case SerializedPropertyType.FixedBufferSize:
                case SerializedPropertyType.ManagedReference:
                case SerializedPropertyType.Hash128:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializedProperty"></param>
        /// <param name="condition">can be field, method or property name</param>
        /// <returns></returns>
        public static bool CheckCondition(this SerializedProperty serializedProperty, string condition)
        {
            serializedProperty.GetValue(out var obj);
            if (obj != null)
            {
                dynamic result = GetConditionValue(obj, condition);
                try
                {
                    return (bool)result;
                }
                catch
                {
                    return result != null;
                }
            }

            return false;
        }

        private static dynamic GetConditionValue(object obj, string condition)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                              BindingFlags.Static | BindingFlags.FlattenHierarchy;

            Type objType = obj.GetType();
            var property = objType.GetPropertyExt(condition, bindingFlags);
            if (property != null)
            {
                return property.GetValue(obj);
            }

            var method = objType.GetMethodExt(condition, bindingFlags);
            if (method != null)
            {
                return method.Invoke(obj, null);
            }

            var field = objType.GetFieldExt(condition, bindingFlags);
            if (field != null)
            {
                return field.GetValue(obj);
            }

            return null;
        }
    }
}