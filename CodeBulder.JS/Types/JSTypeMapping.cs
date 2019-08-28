using CodeBuilder.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Types
{
    public static class JSTypeMapping
    {
        public static Dictionary<Type, JSType> TypeMappings = new Dictionary<Type, JSType>
        {
            { typeof(sbyte), new JSNumber() },
            { typeof(byte), new JSNumber() },
            { typeof(short), new JSNumber() },
            { typeof(ushort), new JSNumber() },
            { typeof(int), new JSNumber() },
            { typeof(uint), new JSNumber() },
            { typeof(long), new JSNumber() },
            { typeof(ulong), new JSNumber() },
            { typeof(float), new JSNumber() },
            { typeof(double), new JSNumber() },
            { typeof(decimal), new JSNumber() },

            { typeof(sbyte?), new JSNumber() },
            { typeof(byte?), new JSNumber() },
            { typeof(short?), new JSNumber() },
            { typeof(ushort?), new JSNumber() },
            { typeof(int?), new JSNumber() },
            { typeof(uint?), new JSNumber() },
            { typeof(long?), new JSNumber() },
            { typeof(ulong?), new JSNumber() },
            { typeof(float?), new JSNumber() },
            { typeof(double?), new JSNumber() },
            { typeof(decimal?), new JSNumber() },

             { typeof(Enum), new JSNumber() },

            { typeof(string), new JSString() },
            { typeof(char), new JSString() },
            { typeof(char?), new JSString() },

            { typeof(DateTime), new JSDate() },
            { typeof(DateTime?), new JSDate() },

            { typeof(object), new JSObject() }
        };

        public static JSType GetJSType(TypeStructure type, bool isPromise = false)
        {
            if (type.IsSytemType)
            {
                var jsType = TypeMappings.ContainsKey(type.Type) ? (JSType)Activator.CreateInstance(TypeMappings[type.Type].GetType()) : new JSString();
                if (!type.IsArray)
                {
                    jsType.IsPromise = isPromise;
                }
                return type.IsArray ? new JSArray(jsType) { IsPromise = isPromise } : jsType;
            }
            else
            {
                var jsType = new JSClassType
                {
                    ClassName = type.TypeName,
                   
                };
                if (!type.IsArray)
                {
                    jsType.IsPromise = isPromise;
                }
                return type.IsArray ? new JSArray(jsType) { IsPromise = isPromise } : (JSType)jsType;
            }
        }
    }
}
