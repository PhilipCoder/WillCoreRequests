using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ICodeBuilder.IOC
{
    /// <summary>
    /// Storage place for all IOC type mappings.
    /// 
    /// Author : Philip Schoeman
    /// </summary>
    public class IOCContainer
    {
        /// <summary>
        /// Container that stores all the IOC types, only accessible in the same assembly.
        /// </summary>
        internal Dictionary<Type, List<IOCInstanceMapping>> TypeMappings { get; set; } = new Dictionary<Type, List<IOCInstanceMapping>>();
        /// <summary>
        /// Used to map the types for IOC.
        /// </summary>
        public IOCMapper TypeMapper { get; set; }

        public IOCContainer()
        {
            TypeMapper = new IOCMapper(this);
        }

        /// <summary>
        /// Factory method to get registered instance of type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="constructorParameters"></param>
        /// <returns></returns>
        public T GetInstance<T>(params object[] constructorParameters)
        {
            if (TypeMappings.ContainsKey(typeof(T)))
            {
                var constructorTypes = constructorParameters.Select(x => x.GetType()).ToArray();
                var instance = TypeMappings[typeof(T)].FirstOrDefault(x => x.ContainsConstructor(constructorTypes));
                if (instance == null)
                {
                    throw new KeyNotFoundException($"Type {typeof(T).Name} has not been registered!");
                }
                return (T)IOCInstanceFactory.CreateInstance(instance.TargetType, constructorParameters);
            }
            throw new KeyNotFoundException($"Type {typeof(T).Name} has not been registered!");
        }


    }
}
