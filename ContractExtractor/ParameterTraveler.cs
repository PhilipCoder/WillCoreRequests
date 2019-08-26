using CodeBuilder.CoreBuilder;
using CodeBuilder.Structure;
using System;
using System.Linq;
using System.Reflection;

namespace ContractExtractor
{
    public class ParameterTraveler : TypeTraveler
    {
        public void travelParamters<T>(ParameterInfo parameter, MethodStructure method, ClassContainter<T> classContainter)
        {
            if (shouldExcludeParameter(parameter, classContainter) || shouldIncludeParamter(parameter, classContainter)) return;
            var objectType = GetItemType(parameter.ParameterType);
            TypeStructure parameterStructure = getNewTypeStructuree<T>(classContainter, parameter, method, objectType);
            if (shouldProcessParameter(objectType, parameterStructure))
            {
                var properties = parameter.ParameterType.GetProperties(propertyBindingFlags).ToList();
                properties.ForEach(property => travelObject(property, parameterStructure, classContainter));
            }

        }

        private void travelObject<T>(PropertyInfo objectType, TypeStructure typeStructure, ClassContainter<T> classContainter)
        {
            var type = GetItemType(objectType.PropertyType);
            TypeStructure newTypeStructure = getNewTypeStructure(objectType, type, classContainter);
            if (!type.IsSystem)
            {
                foreach (var property in type.Type.GetProperties(propertyBindingFlags))
                {
                    travelObject<T>(property, newTypeStructure, classContainter);
                }
            }
            typeStructure.Properties.Add(new TypeStructure(newTypeStructure));
        }

        private bool shouldProcessParameter(Models.ItemType objectType, TypeStructure parameterStructure)
        {
            return !parameterStructure.IsSytemType && AppDomain.CurrentDomain.GetAssemblies().Contains(objectType.Type.Assembly);
        }

        private bool shouldIncludeParamter<T>(ParameterInfo parameter, ClassContainter<T> classContainter)
        {
            return classContainter.ParameterIncludeFilterAttributes.Length > 0 && !classContainter.ParameterIncludeFilterAttributes.Any(x => parameter.GetCustomAttribute(x) != null);
        }

        private bool shouldExcludeParameter<T>(ParameterInfo parameter, ClassContainter<T> classContainter)
        {
            return classContainter.ParameterExcludeFilterAttributes.Any(x => parameter.GetCustomAttribute(x) != null);
        }
        
        private TypeStructure getNewTypeStructuree<T>(ClassContainter<T> classContainter, ParameterInfo parameter, MethodStructure method, Models.ItemType objectType)
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

        private TypeStructure getNewTypeStructure<T>(PropertyInfo objectType, Models.ItemType type, ClassContainter<T> classContainter)
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
            if (!paramterStructure.IsSytemType && !classContainter.Models.ContainsKey(paramterStructure.TypeName))
            {
                classContainter.Models[paramterStructure.TypeName] = paramterStructure;
            }
            return paramterStructure;
        }
    }
}
