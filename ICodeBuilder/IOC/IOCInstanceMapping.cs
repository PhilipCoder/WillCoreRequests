using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICodeBuilder.IOC
{
    public class IOCInstanceMapping
    {
        public Type[] ConstructorTypes { get; set; }
        public Type TargetType { get; set; }

        public IOCInstanceMapping(Type targetType, Type[] constructorTypes)
        {
            ConstructorTypes = constructorTypes;
            TargetType = targetType;
        }

        public bool ContainsConstructor(Type[] constructorTypes)
        {
            if (constructorTypes.Length != ConstructorTypes.Length) return false;
            for (int constructorIndex = 0; constructorIndex < constructorTypes.Length; constructorIndex++)
            {
                if (!ConstructorTypes[constructorIndex].IsAssignableFrom(constructorTypes[constructorIndex])) return false;
            }
            return true;
        }
    }
}
