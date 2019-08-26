using ContractExtractor.Models;
using CodeBuilder.CoreBuilder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ContractExtractor
{
    public abstract class BaseTraveler
    {
        public bool ResultCamelCase { get; set; }
        public ItemType GetItemType(Type type)
        {
            ItemType result = getType(type);
            reflectToBaseType(type, result);
            result.IsSystem = isBaseType(result);
            return result;
        }

        public List<Type> getAllEntities<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x =>
                 (typeof(T).IsGenericType && typeof(T).GenericTypeArguments[0].IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract) ||
                 typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract
                 ).ToList();
        }

        public Dictionary<string, Attribute> filterAndMapAttributesToDictionary<T>(IEnumerable<Attribute> attributes, ClassContainter<T> classContainter)
        {
            return attributes.Where(x => shouldIncludeAttribute(classContainter, x)).ToDictionary(a => a.GetType().Name, b => b);
        }

        public bool shouldIncludeAttribute<T>(ClassContainter<T> classContainter, Attribute attribute)
        {
            return !classContainter.AttributeExcludeFilterAttributes.Contains(attribute.GetType()) &&
                (classContainter.AttributeIncludeFilterAttributes.Length > 0 || classContainter.AttributeIncludeFilterAttributes.Contains(attribute.GetType()));
        }

        private static bool isBaseType(ItemType result)
        {
            return result.Type.FullName.StartsWith("System.") || result.Type.FullName.StartsWith("Microsoft.");
        }

        private static void reflectToBaseType(Type type, ItemType result)
        {
            if (type.IsArray)
            {
                result.IsArray = true;
                result.Type = type.GetElementType();
            }
            while (result.Type.IsGenericType)
            {
                result.Type = result.Type.GetGenericArguments()[0];
                if (typeof(IEnumerable).IsAssignableFrom(result.Type) || type.IsArray)
                {
                    result.IsArray = true;
                }
            }
        }

        private static ItemType getType(Type type)
        {
            var result = new ItemType {
                Type = type
            };
            if (type.FullName.StartsWith("System.") || type.FullName.StartsWith("Microsoft."))
            {
                if (type.IsArray)
                {
                    result.IsArray = true;
                    result.Type = type.GetElementType();
                }
                else if (typeof(IEnumerable).IsAssignableFrom(type) && type.GetGenericArguments().Length > 0)
                {
                    result.IsArray = true;
                    result.Type = type.GetGenericArguments()[0];
                }
            }
            return result;
        }

        
    }
}
