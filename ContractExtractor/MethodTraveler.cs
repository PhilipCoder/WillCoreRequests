using CodeBuilder.CoreBuilder;
using CodeBuilder.Structure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;
using System.Reflection;

namespace ContractExtractor
{
    public class MethodTraveler : ParameterTraveler
    {
        public void travelAction<T>(MethodInfo action, ClassStructure classStructure, ClassContainter<T> classContainter)
        {
            if (shouldMethodBeExcluded(action, classContainter) || shouldMethodBeIncluded(action, classContainter)) return;
            MethodStructure newMethod = getMethodStructureInstance(action, classStructure, classContainter);
            populateParameters(action, classContainter, newMethod);
            var resultTypeStructure = new TypeStructure();
            travelResult<T>(action.ReturnType, resultTypeStructure, classContainter);
            newMethod.Result = new TypeStructure(resultTypeStructure);
        }

        private void populateParameters<T>(MethodInfo action, ClassContainter<T> classContainter, MethodStructure newMethod)
        {
            foreach (var parameter in action.GetParameters())
            {
                travelParamters<T>(parameter, newMethod, classContainter);
            }
        }

        private MethodStructure getMethodStructureInstance<T>(MethodInfo action, ClassStructure classStructure, ClassContainter<T> classContainter)
        {
            var actionURL = action.GetCustomAttribute<HttpMethodAttribute>()?.Template ?? null;
            var newMethod = new MethodStructure
            {
                Attributes = filterAndMapAttributesToDictionary(action.GetCustomAttributes(), classContainter),
                Name = action.Name,
                URL = classStructure.URL.Replace("[controller]", classStructure.Name).Replace("[action]", action.Name) + ((actionURL != null) ? $"/{actionURL}" : "")
            };
            classStructure.Methods.Add(newMethod);
            return newMethod;
        }

        private bool shouldMethodBeIncluded<T>(MethodInfo action, ClassContainter<T> classContainter)
        {
            return classContainter.MethodIncludeFilterAttributes.Length > 0 && !classContainter.MethodIncludeFilterAttributes.Any(x => action.GetCustomAttribute(x) != null);
        }

        private bool shouldMethodBeExcluded<T>(MethodInfo action, ClassContainter<T> classContainter)
        {
            return classContainter.MethodExcludeFilterAttributes.Any(x => action.GetCustomAttribute(x) != null);
        }
    }
}
