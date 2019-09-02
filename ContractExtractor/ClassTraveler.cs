using ICodeBuilder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;

namespace ContractExtractor
{
    public class ClassTraveler : MethodTraveler
    {
        private const BindingFlags methodBindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        public ClassTraveler(bool resultCamelCase)
        {
            ResultCamelCase = resultCamelCase;
        }
        public ClassContainter TravelClasses(ClassContainter classContainter)
        {
            foreach (var controller in getAllEntities(classContainter.recursionConfiguration.ControllerType))
            {
                if (shouldClassBeExcluded(classContainter, controller) || shouldClassBeIncluded(classContainter, controller)) continue;
                ClassStructure newController = getClassStructureInstance(classContainter, controller);
                classContainter.Classes.Add(newController);
                travelController(controller, newController, classContainter);
            }
            return classContainter;
        }

        private void travelController(Type controller, ClassStructure classStructure, ClassContainter classContainter)
        {
            controller.GetMethods(methodBindingFlags).ToList().
                ForEach(action => travelAction(action, classStructure, classContainter));
        }

        private bool shouldClassBeIncluded(ClassContainter classContainter, Type controller)
        {
            return classContainter.recursionConfiguration.ClassIncludeFilterAttributes.Count() > 0 && !classContainter.recursionConfiguration.ClassIncludeFilterAttributes.Any(x => controller.GetCustomAttribute(x) != null);
        }

        private bool shouldClassBeExcluded(ClassContainter classContainter, Type controller)
        {
            return classContainter.recursionConfiguration.ClassExcludeFilterAttributes.Any(x => controller.GetCustomAttribute(x) != null) || controller.GetType() == classContainter.recursionConfiguration.ControllerType;
        }

        private ClassStructure getClassStructureInstance(ClassContainter classContainter, Type controller)
        {
            return new ClassStructure()
            {
                Attributes = filterAndMapAttributesToDictionary(controller.GetCustomAttributes(), classContainter),
                Name = controller.Name.Replace("controller", "", StringComparison.OrdinalIgnoreCase),
                URL = controller.GetCustomAttribute<RouteAttribute>().Template ?? "[controller]/[action]"
            };
        }
    }
}
