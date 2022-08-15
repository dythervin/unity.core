using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Dythervin.Core.Utils
{
    public class WeakReferenceT<T> : WeakReference
        where T : class
    {
        public WeakReferenceT(T target) : base(target) { }

        public WeakReferenceT(T target, bool trackResurrection) : base(target, trackResurrection) { }

        protected WeakReferenceT([NotNull] SerializationInfo info, StreamingContext context) : base(info, context) { }

        public new T Target
        {
            get => (T)base.Target;
            set => base.Target = value;
        }

        public bool TryGetTarget(out T target)
        {
            if (IsAlive)
            {
                target = Target;
                return true;
            }

            target = null;
            return false;
        }
    }
}