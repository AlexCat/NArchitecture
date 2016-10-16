using System;
using System.Linq;

namespace NArchitecture
{
    public static class TypeExtensions
    {
        private static Type[] handlerTypes = new Type[]
        {
            typeof(IHandleEvent<>),
            typeof(IHandleRequest<>),
            typeof(IHandleRequest<,>)
        };

        private static bool IsHandlerType(this Type type)
        {
            bool isHandlerType = false;
            if (type.IsGenericType)
            {
                Type genericTypeDefinition = type.GetGenericTypeDefinition();
                isHandlerType = handlerTypes.Contains(genericTypeDefinition);
            }
            return isHandlerType;
        }

        public static Type[] GetHandlerServiceTypes(this Type type)
        {
            return type.GetInterfaces()
                .Where(t => t.IsHandlerType())
                .ToArray();
        }
    }
}
