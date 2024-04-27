#if DI_VCONTAINER
using System;
using System.Collections.Generic;
using VContainer;
using IObjectResolver = VContainer.IObjectResolver;
using Registration = VContainer.Registration;

namespace Dythervin
{
    public static class VContainerExtensions
    {
        private static readonly Dictionary<Type, Registration> RegistrationBuilders = new();

        public static T Create<T>(this IObjectResolver resolver, bool cache = true)
        {
            return (T)Create(resolver, typeof(T), cache);
        }

        public static object Create(this IObjectResolver resolver, Type type, bool cache = true)
        {
            if (!RegistrationBuilders.TryGetValue(type, out Registration registrationBuilder))
            {
                registrationBuilder = new RegistrationBuilder(type, Lifetime.Transient).Build();
                if (cache)
                {
                    RegistrationBuilders.Add(type, registrationBuilder);
                }
            }

            return resolver.Resolve(registrationBuilder);
        }

        public static T Create<T, TArg>(this IObjectResolver resolver, TArg arg)
        {
            return (T)resolver.Resolve(
                new RegistrationBuilder(typeof(T), Lifetime.Transient).WithParameter(arg).Build());
        }

        public static object Create<TArg>(this IObjectResolver resolver, Type type, TArg arg)
        {
            return resolver.Resolve(new RegistrationBuilder(type, Lifetime.Transient).WithParameter(arg).Build());
        }

        public static RegistrationBuilder RegisterInstance(this IContainerBuilder builder, Type type, object instance)
        {
            return builder.Register(new InstanceRegistration(type, instance));
        }

        private class InstanceRegistration : RegistrationBuilder
        {
            private readonly object _instance;

            public InstanceRegistration(Type implementationType, object instance) : base(implementationType,
                Lifetime.Singleton)
            {
                _instance = instance;
            }

            public override Registration Build()
            {
                return new(ImplementationType, Lifetime, InterfaceTypes, new Spawner(_instance));
            }

            private class Spawner : IInstanceProvider
            {
                private readonly object _instance;

                public Spawner(object instance)
                {
                    _instance = instance;
                }

                public object SpawnInstance(IObjectResolver resolver)
                {
                    return _instance;
                }
            }
        }
    }
}
#endif