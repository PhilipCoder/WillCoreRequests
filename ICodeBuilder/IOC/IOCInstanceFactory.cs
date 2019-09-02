using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ICodeBuilder.IOC
{
    public static class IOCInstanceFactory
    {
        public static object CreateInstance(Type T, params object[] instanciateParameters)
        {
            ConstructorInfo constructor = getConstructor(T, instanciateParameters);
            List<ParameterExpression> constructorParameters = getConstructorParameters(T, instanciateParameters, constructor);
            var constructorExpression = Expression.New(constructor, constructorParameters);
            var expression = Expression.Lambda(constructorExpression, constructorParameters).Compile();
            return expression.DynamicInvoke(instanciateParameters);
        }

        private static ConstructorInfo getConstructor(Type T, object[] instanciateParameters)
        {
            var constructor = T.GetConstructor(
                            BindingFlags.Instance | BindingFlags.Public,
                            null,
                            CallingConventions.HasThis,
                            instanciateParameters.Select(x => x.GetType()).ToArray(),
                            new ParameterModifier[0]);
            if (constructor == null)
            {
                throw new Exception(
                    $"Constructor not found for type {T.Name} with parameters of types: " +
                    $"{instanciateParameters.Select(x => x.GetType().Name).Aggregate((a, b) => a + "," + b)}.");
            }
            return constructor;
        }

        private static List<ParameterExpression> getConstructorParameters(Type T, object[] instanciateParameters, ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();

            var constructorParameters = new List<ParameterExpression>();
            for (int parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
            {
                ParameterInfo parameter = parameters[parameterIndex];
                if (!parameter.ParameterType.IsAssignableFrom(instanciateParameters[parameterIndex].GetType()))
                {
                    throw new InvalidCastException($"The type {T.Name} could not be constructed. Parameter type and index mismatch!");
                }
                constructorParameters.Add(Expression.Parameter(parameter.ParameterType, parameter.Name));
            }

            return constructorParameters;
        }
    }
}
