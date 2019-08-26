using CodeBuilder.CoreBuilder;
using CodeBuilder.Structure;
using System;
using System.Linq;
using System.Reflection;

namespace ContractExtractor
{
    public class TypeTraveler : BaseTraveler
    {
        internal const BindingFlags propertyBindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.DeclaredOnly;
        public void travelResult<T>(Type type, TypeStructure typeStructure, ClassContainter<T> container)
        {
            if (type == typeof(void)) return;
            var objectType = GetItemType(type);
            typeStructure.IsArray = objectType.IsArray;
            typeStructure.IsSytemType = objectType.IsSystem;
            typeStructure.Type = objectType.Type;
            typeStructure.TypeName = objectType.Type.Name;
            if (!objectType.IsSystem && AppDomain.CurrentDomain.GetAssemblies().Contains(objectType.Type.Assembly))
            {
                foreach (var property in typeStructure.Type.GetProperties(propertyBindingFlags))
                {
                    var propertyName = ResultCamelCase ? $"{property.Name.Substring(0, 1).ToLower()}{(property.Name.Length > 1 ? property.Name.Substring(1) : "")}" : property.Name;
                    var newTypeStructure = new TypeStructure
                    {
                        Name = propertyName,
                        Attributes = property.GetCustomAttributes().ToDictionary(a => a.GetType().Name, b => b)
                    };
                    travelResult(property.PropertyType, newTypeStructure, container);
                    typeStructure.Properties.Add(new TypeStructure(newTypeStructure));
                }
                if (!container.Models.ContainsKey(typeStructure.TypeName))
                {
                    container.Models[typeStructure.TypeName] = typeStructure;
                }
            }
        }

    }
}
