using System.Collections.Generic;
using UnityEngine;

namespace Dythervin.Core.Utils
{
    public static class PersistentRoot
    {
        private static Transform _instance;
        private static bool _initialized;
        private static readonly Dictionary<string, GameObject> Transforms = new Dictionary<string, GameObject>();

        public static Transform Transform
        {
            get
            {
                if (!_initialized)
                {
                    var gameObject = new GameObject(nameof(PersistentRoot)) { isStatic = true };
                    Object.DontDestroyOnLoad(gameObject);
                    _instance = gameObject.transform;
                    _initialized = true;
                }

                return _instance;
            }
        }

        public static GameObject Get(string name)
        {
            if (!Transforms.TryGetValue(name, out GameObject target))
                Transforms[name] = target = new GameObject(name) { isStatic = true, transform = { parent = Transform } };

            return target;
        }
    }
}