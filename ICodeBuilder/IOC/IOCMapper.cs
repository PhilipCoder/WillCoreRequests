using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ICodeBuilder.IOC
{
    /// <summary>
    /// Contains the function to map contracts to types
    /// 
    /// Author : Philip Schoeman
    /// </summary>
    public class IOCMapper
    {
        private IOCContainer _iocContainer { get; set; }
        public IOCMapper(IOCContainer iocContainer)
        {
            _iocContainer = iocContainer;
        }

        public IOCMapper MapType<C, T>() where T : C
        {
            if (!_iocContainer.TypeMappings.ContainsKey(typeof(C)))
            {
                _iocContainer.TypeMappings[typeof(C)] = new List<IOCInstanceMapping>();
            }
            var keys = GetTypeKeys<C, T>();
            foreach (var key in keys)
            {
                _iocContainer.TypeMappings[typeof(C)].Add(key);
                Debug.WriteLine($"Registering type {key}");
            }
            return this;
        }

        private IOCInstanceMapping[] GetTypeKeys<C, T>() where T : C
        {
            var constructors = typeof(T).GetConstructors();
            return constructors.Select(
                constructor =>
                new IOCInstanceMapping(
                    typeof(T),
                    constructor.GetParameters().Select(parameter => parameter.ParameterType).ToArray()
                    )
                ).ToArray();
        }
    }
}
