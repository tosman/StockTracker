using System;
using System.Linq;
using System.Reflection;

namespace StructureMap.AutoMocking
{
    public static class NSubstituteAutoMockerBuilder
    {
        public class NSubstituteAutoMocker<T> : AutoMocker<T> where T : class
        {
            public NSubstituteAutoMocker()
            {
                _serviceLocator = new NSubstituteServiceLocator();
                _container = new AutoMockedContainer(_serviceLocator);
            }
        }

        public class NSubstituteServiceLocator : ServiceLocator
        {
            private readonly SubstituteFactory _substituteFactory = new SubstituteFactory();

            public T Service<T>() where T : class
            {
                return (T) _substituteFactory.CreateMock(typeof(T));
            }

            public object Service(Type serviceType)
            {
                return _substituteFactory.CreateMock(serviceType);
            }

            public T PartialMock<T>(params object[] args) where T : class
            {
                return (T) _substituteFactory.CreateMock(typeof(T));
            }
        }

        public class SubstituteFactory
        {
            private readonly Func<Type, object> _factory;

            public SubstituteFactory()
            {
                var method = typeof(NSubstitute.Substitute).GetMethods().First(x => x.ContainsGenericParameters && x.GetGenericArguments().Length == 1);
                _factory = typeToMock => method.MakeGenericMethod(typeToMock).Invoke(null, new object[] {null});
            }

            public object CreateMock(Type type)
            {
                return _factory(type);
            }
        }

        private static Type _assembledType;


        public static object Build<T>()
        {
            var type = _assembledType ?? (_assembledType = Compile());

            var genericType = type.MakeGenericType(new[] {typeof(T)});

            return Activator.CreateInstance(genericType, new object[] {});
        }

        private static Type Compile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assembledType =
                assembly.GetTypes().FirstOrDefault(
                    x => x.ContainsGenericParameters && x.Name.Contains("NSubstituteAutoMocker"));

            return assembledType;
        }
    }
}