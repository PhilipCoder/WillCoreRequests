using ICodeBuilder;
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

            { typeof(bool?), new JSBool() },
            { typeof(bool), new JSBool() },

            { typeof(object), new JSObject() }
        };

        public static Dictionary<Type, JSType> TypeMappingsArray = new Dictionary<Type, JSType>
        {
            { typeof(sbyte), new JSNumberArray() },
            { typeof(byte), new JSNumberArray() },
            { typeof(short), new JSNumberArray() },
            { typeof(ushort), new JSNumberArray() },
            { typeof(int), new JSNumberArray() },
            { typeof(uint), new JSNumberArray() },
            { typeof(long), new JSNumberArray() },
            { typeof(ulong), new JSNumberArray() },
            { typeof(float), new JSNumberArray() },
            { typeof(double), new JSNumberArray() },
            { typeof(decimal), new JSNumberArray() },

            { typeof(sbyte?), new JSNumberArray() },
            { typeof(byte?), new JSNumberArray() },
            { typeof(short?), new JSNumberArray() },
            { typeof(ushort?), new JSNumberArray() },
            { typeof(int?), new JSNumberArray() },
            { typeof(uint?), new JSNumberArray() },
            { typeof(long?), new JSNumberArray() },
            { typeof(ulong?), new JSNumberArray() },
            { typeof(float?), new JSNumberArray() },
            { typeof(double?), new JSNumberArray() },
            { typeof(decimal?), new JSNumberArray() },

             { typeof(Enum), new JSNumberArray() },

            { typeof(string), new JSStringArray() },
            { typeof(char), new JSStringArray() },
            { typeof(char?), new JSStringArray() },

            { typeof(DateTime), new JSDateArray() },
            { typeof(DateTime?), new JSDateArray() },

            { typeof(bool?), new JSBoolArray() },
            { typeof(bool), new JSBoolArray() },

            { typeof(object), new JSObjectArray() }
        };

        public static JSType GetJSType(TypeStructure type, bool isPromise = false)
        {
            JSType jsType;
            if (type.IsSytemType)
            {
                if (!type.IsArray)
                {
                    jsType = TypeMappings.ContainsKey(type.Type) ? (JSType)Activator.CreateInstance(TypeMappings[type.Type].GetType()) : new JSString();
                }
                else
                {
                    jsType = TypeMappingsArray.ContainsKey(type.Type) ? (JSType)Activator.CreateInstance(TypeMappingsArray[type.Type].GetType()) : new JSStringArray();
                }
            }
            else
            {
                if (!type.IsArray)
                {
                    jsType = new JSClass
                    {
                        ClassName = type.TypeName,
                    };
                }
                else
                {
                    jsType = new JSClassArray
                    {
                        ClassName = type.TypeName,
                    };
                }
            }
            jsType.IsPromise = isPromise;
            return jsType;
        }
    }
}
