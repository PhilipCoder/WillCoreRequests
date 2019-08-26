using CodeBuilder.CoreBuilder;
using CodeBuilder.Structure;
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
        public ClassContainter<T> TravelClasses<T>(ClassContainter<T> classContainter)
        {
            foreach (var controller in getAllEntities<T>())
            {
                if (shouldClassBeExcluded(classContainter, controller) || shouldClassBeIncluded(classContainter, controller)) continue;
                ClassStructure newController = getClassStructureInstance(classContainter, controller);
                classContainter.Classes.Add(newController);
                travelController<T>(controller, newController, classContainter);
            }
            return classContainter;
        }

        private void travelController<T>(Type controller, ClassStructure classStructure, ClassContainter<T> classContainter)
        {
            controller.GetMethods(methodBindingFlags).ToList().
                ForEach(action => travelAction<T>(action, classStructure, classContainter));
        }

        private bool shouldClassBeIncluded<T>(ClassContainter<T> classContainter, Type controller)
        {
            return classContainter.ClassIncludeFilterAttributes.Length > 0 && !classContainter.ClassIncludeFilterAttributes.Any(x => controller.GetCustomAttribute(x) != null);
        }

        private bool shouldClassBeExcluded<T>(ClassContainter<T> classContainter, Type controller)
        {
            return classContainter.ClassExcludeFilterAttributes.Any(x => controller.GetCustomAttribute(x) != null) || controller.GetType() == typeof(T);
        }

        private ClassStructure getClassStructureInstance<T>(ClassContainter<T> classContainter, Type controller)
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
