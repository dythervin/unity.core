using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dythervin.Core.Extensions
{
    public static class Components
    {
        public static void GetComponents<T>(this Component monoBehaviour, out T[] value)
        {
            value = monoBehaviour.GetComponents<T>();
        }

        public static void GetComponentsInChildren<T>(this Component monoBehaviour, out T[] value, bool includeInactive = true)
        {
            value = monoBehaviour.GetComponentsInChildren<T>(includeInactive);
        }

        public static void GetComponentInChildren<T>(this Component monoBehaviour, out T value)
        {
            value = monoBehaviour.GetComponentInChildren<T>();
        }

        public static bool FindInChildrenRecursive(this Component component, Func<Transform, bool> func, out Transform value)
        {
            if (func.Invoke(component.transform))
            {
                value = component.transform;
                return true;
            }

            foreach (Transform child in component.transform)
            {
                if (FindInChildrenRecursive(component, func, out value))
                    return true;
            }

            value = null;
            return false;
        }

#if !UNITY_2021_3_OR_NEWER
        public static T GetComponentInParent<T>(this Component component, bool includeInactive)
            where T : class
        {
            if (includeInactive)
            {
                Transform parent = component.transform.parent;
                while (parent)
                {
                    if (parent.TryGetComponent(out T target))
                        return target;

                    if (parent.parent != null)
                        parent = parent.parent;
                }

                return null;
            }

            return component.GetComponentInParent<T>();
        }
#endif

#if UNITY_EDITOR
        [MenuItem("CONTEXT/Component/Force Remove")]
        private static void ForceRemove(MenuCommand command)
        {
            Component target = (Component)command.context;
            if (target)
                Object.DestroyImmediate(target);
        }
#endif
    }
}