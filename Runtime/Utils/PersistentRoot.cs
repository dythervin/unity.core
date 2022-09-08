using System.Collections.Generic;
using UnityEngine;

namespace Dythervin.Core.Utils
{
    public static class PersistentRoot
    {
        private static readonly Dictionary<string, GameObject> Transforms = new Dictionary<string, GameObject>();

        public static Transform Transform { get; private set; }

        public static GameObject Get(string name)
        {
            if (!Transforms.TryGetValue(name, out GameObject target))
                Transforms[name] = target = new GameObject(name) { isStatic = true, transform = { parent = Transform } };

            return target;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            var gameObject = new GameObject(nameof(PersistentRoot)) { isStatic = true };
            Object.DontDestroyOnLoad(gameObject);
            Transform = gameObject.transform;
        }
    }
}