using ICodeBuilder;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;
using System.Reflection;

namespace ContractExtractor
{
    public class MethodTraveler : ParameterTraveler
    {
        public void travelAction(MethodInfo action, ClassStructure classStructure, ClassContainter classContainter)
        {
            if (shouldMethodBeExcluded(action, classContainter) || shouldMethodBeIncluded(action, classContainter)) return;
            MethodStructure newMethod = getMethodStructureInstance(action, classStructure, classContainter);
            populateParameters(action, classContainter, newMethod);
            var resultTypeStructure = new TypeStructure();
            travelResult(action.ReturnType, resultTypeStructure, classContainter, newMethod, 0);
            newMethod.Result = new TypeStructure(resultTypeStructure);
        }

        private void populateParameters(MethodInfo action, ClassContainter classContainter, MethodStructure newMethod)
        {
            foreach (var parameter in action.GetParameters())
            {
                travelParamters(parameter, newMethod, classContainter);
            }
        }

        private MethodStructure getMethodStructureInstance(MethodInfo action, ClassStructure classStructure, ClassContainter classContainter)
        {
            var actionURL = action.GetCustomAttribute<HttpMethodAttribute>()?.Template ?? null;
            var newMethod = new MethodStructure
            {
                Attributes = filterAndMapAttributesToDictionary(action.GetCustomAttributes(), classContainter),
                Name = action.Name,
                IsRPC = classStructure.URL.Contains("[action]"),
                URL = classStructure.URL.Replace("[controller]", classStructure.Name).Replace("[action]", action.Name) + ((actionURL != null) ? $"/{actionURL}" : "")
            };
            classStructure.Methods.Add(newMethod);
            return newMethod;
        }

        private bool shouldMethodBeIncluded(MethodInfo action, ClassContainter classContainter)
        {
            return classContainter.recursionConfiguration.MethodIncludeFilterAttributes.Count() > 0 && !classContainter.recursionConfiguration.MethodIncludeFilterAttributes.Any(x => action.GetCustomAttribute(x) != null);
        }

        private bool shouldMethodBeExcluded(MethodInfo action, ClassContainter classContainter)
        {
            return classContainter.recursionConfiguration.MethodExcludeFilterAttributes.Any(x => action.GetCustomAttribute(x) != null);
        }
    }
}
