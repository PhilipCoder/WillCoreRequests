using ICodeBuilder;
using System;
using System.Linq;
using System.Reflection;

namespace ContractExtractor
{
    public class TypeTraveler : BaseTraveler
    {
        internal const BindingFlags propertyBindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.DeclaredOnly;
        public void travelResult(Type type, TypeStructure typeStructure, ClassContainter container, MethodStructure method, int depth)
        {
            var objectType = GetItemType(type);
            typeStructure.IsArray = objectType.IsArray;
            typeStructure.IsSytemType = objectType.IsSystem;
            typeStructure.Type = objectType.Type;
            typeStructure.TypeName = objectType.Type.Name;
            depth++;
            if (depth > container.recursionConfiguration.MaxRecursiveDepth)
            {
                throw new Exception($"WillCore.Requests reflection has encountered a method result that exceeds the max recursive depth of {container.recursionConfiguration.MaxRecursiveDepth}. This happened on method {method.Name} and type {typeStructure.TypeName}.  " +
                    $"Please check the class depth or increase the default maximum recursive depth of WillCore.Requests.");
            }
            if (type == typeof(void))
            {
                return;
            };
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
                    travelResult(property.PropertyType, newTypeStructure, container, method, depth);
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
