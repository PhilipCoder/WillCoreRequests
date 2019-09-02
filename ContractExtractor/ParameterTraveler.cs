using ICodeBuilder;
using System;
using System.Linq;
using System.Reflection;

namespace ContractExtractor
{
    public class ParameterTraveler : TypeTraveler
    {
        public void travelParamters(ParameterInfo parameter, MethodStructure method, ClassContainter classContainter)
        {
            if (shouldExcludeParameter(parameter, classContainter) || shouldIncludeParamter(parameter, classContainter)) return;
            var objectType = GetItemType(parameter.ParameterType);
            TypeStructure parameterStructure = getNewTypeStructuree(classContainter, parameter, method, objectType);
            if (shouldProcessParameter(objectType, parameterStructure))
            {
                var properties = parameter.ParameterType.GetProperties(propertyBindingFlags).ToList();
                properties.ForEach(property => travelObject(property, parameterStructure, classContainter, method, 0));
            }

        }

        private void travelObject(PropertyInfo objectType, TypeStructure typeStructure, ClassContainter classContainter, MethodStructure method, int depth)
        {
            depth++;
            if (depth > classContainter.recursionConfiguration.MaxRecursiveDepth)
            {
                throw new Exception($"WillCore.Requests reflection has encountered a method parameter that exceeds the max recursive depth of {classContainter.recursionConfiguration.MaxRecursiveDepth}. This happened on method {method.Name} and type {typeStructure.TypeName}.  " +
                    $"Please check the class depth or increase the default maximum recursive depth of WillCore.Requests.");
            }
            var type = GetItemType(objectType.PropertyType);
            TypeStructure newTypeStructure = getNewTypeStructure(objectType, type, classContainter);
            if (!type.IsSystem && !classContainter.Models.ContainsKey(newTypeStructure.TypeName))
            {
                classContainter.Models[newTypeStructure.TypeName] = newTypeStructure;
                foreach (var property in type.Type.GetProperties(propertyBindingFlags))
                {
                    travelObject(property, newTypeStructure, classContainter, method, depth);
                }
            }
            typeStructure.Properties.Add(new TypeStructure(newTypeStructure));
        }

        private bool shouldProcessParameter(ItemType objectType, TypeStructure parameterStructure)
        {
            return !parameterStructure.IsSytemType && AppDomain.CurrentDomain.GetAssemblies().Contains(objectType.Type.Assembly);
        }

        private bool shouldIncludeParamter(ParameterInfo parameter, ClassContainter classContainter)
        {
            return classContainter.recursionConfiguration.ParameterIncludeFilterAttributes.Count() > 0 && !classContainter.recursionConfiguration.ParameterIncludeFilterAttributes.Any(x => parameter.GetCustomAttribute(x) != null);
        }

        private bool shouldExcludeParameter(ParameterInfo parameter, ClassContainter classContainter)
        {
            return classContainter.recursionConfiguration.ParameterExcludeFilterAttributes.Any(x => parameter.GetCustomAttribute(x) != null);
        }
        
        private TypeStructure getNewTypeStructuree(ClassContainter classContainter, ParameterInfo parameter, MethodStructure method, ItemType objectType)
        {
            var paramterStructure = new TypeStructure
            {
                Attributes = filterAndMapAttributesToDictionary(parameter.GetCustomAttributes(), classContainter),
                IsSytemType = objectType.IsSystem,
                Name = parameter.Name,
                Type = objectType.Type,
                TypeName = objectType.Type.Name,
                IsArray = objectType.Type.IsArray
            };
            method.Parameters.Add(new TypeStructure(paramterStructure));
            if (!paramterStructure.IsSytemType && !classContainter.Models.ContainsKey(paramterStructure.TypeName))
            {
                classContainter.Models[paramterStructure.TypeName] = paramterStructure;
            }
            return paramterStructure;
        }

        private TypeStructure getNewTypeStructure(PropertyInfo objectType, ItemType type, ClassContainter classContainter)
        {
            var paramterStructure = new TypeStructure
            {
                Attributes = objectType.GetCustomAttributes().ToDictionary(a => a.GetType().Name, b => b),
                IsSytemType = type.IsSystem,
                Type = type.Type,
                IsArray = type.IsArray,
                Name = objectType.Name,
                TypeName = type.Type.Name
            };
            return paramterStructure;
        }
    }
}
