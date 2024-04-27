using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Dythervin
{
    public static class PersistentRoot
    {
        private static readonly Dictionary<string, Wrapper> Transforms = new Dictionary<string, Wrapper>();

        public static Transform Transform { get; private set; }

        public static GameObject Get(string name)
        {
            if (Transforms.TryGetValue(name, out var target))
            {
                target.counter++;
            }
            else
            {
                Transforms[name] = target = new Wrapper(new GameObject(name)
                {
                    isStatic = true,
                    transform = { parent = Transform }
                }) { counter = 1 };
            }

            return target.gameObject;
        }

        public static bool TryGet(string name, [NotNullWhen(true)] out GameObject? gameObject)
        {
            if (Transforms.TryGetValue(name, out var target))
            {
                target.counter++;
                gameObject = target.gameObject;
                return true;
            }

            gameObject = null;
            return false;
        }

        public static void Release(string name)
        {
            if (Transforms.TryGetValue(name, out var target))
            {
                target.counter--;
                if (target.counter == 0)
                {
                    Object.Destroy(target.gameObject);
                    Transforms.Remove(name);
                }
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            var gameObject = new GameObject(nameof(PersistentRoot)) { isStatic = true };
            Object.DontDestroyOnLoad(gameObject);
            Transform = gameObject.transform;
        }

        private class Wrapper
        {
            public readonly GameObject gameObject;
            public int counter;

            public Wrapper(GameObject gameObject)
            {
                this.gameObject = gameObject;
            }
        }
    }
}