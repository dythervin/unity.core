using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Dythervin.Core.Extensions
{
    public static class GameObjects
    {
        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            foreach (Transform child in gameObject.transform)
                SetLayerRecursively(child.gameObject, layer);
        }

        public static void SetTagRecursively(this GameObject gameObject, string tag)
        {
            gameObject.tag = tag;
            foreach (Transform child in gameObject.transform)
                SetTagRecursively(child.gameObject, tag);
        }

        public static T AddComponentReflection<T>(this GameObject go, T toAdd)
            where T : Component
        {
            return go.AddComponent<T>().GetCopyOfReflection(toAdd);
        }

        public static T GetCopyOfReflection<T>(this Component comp, T other)
            where T : Component
        {
            Type type = comp.GetType();
            if (type != other.GetType())
                return null;
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
            var propInfo = type.GetProperties(flags);
            foreach (PropertyInfo propertyInfo in propInfo)
            {
                if (propertyInfo.CanWrite)
                {
                    bool obsolete = false;
                    IEnumerable attrData = propertyInfo.CustomAttributes;
                    foreach (CustomAttributeData data in attrData)
                    {
                        if (data.AttributeType == typeof(ObsoleteAttribute))
                        {
                            obsolete = true;
                            break;
                        }
                    }

                    if (obsolete)
                    {
                        continue;
                    }

                    try
                    {
                        propertyInfo.SetValue(comp, propertyInfo.GetValue(other, null), null);
                    }
                    catch (NotImplementedException) { }
                }
            }

            var fields = type.GetFields(flags);
            foreach (FieldInfo fieldInfo in fields)
            {
                fieldInfo.SetValue(comp, fieldInfo.GetValue(other));
            }

            return comp as T;
        }

        public static void GetComponentInChildren<T>(this GameObject gameObject, out T target, bool includeInactive = true)
        {
            target = gameObject.GetComponentInChildren<T>(includeInactive);
        }


#if UNITY_EDITOR
        public static T AddComponent<T>(this GameObject go, T toAdd)
            where T : Component
        {
            var obj = go.AddComponent<T>();

            UnityEditor.EditorUtility.CopySerialized(toAdd, obj);
            return obj;
        }
#endif
    }
}