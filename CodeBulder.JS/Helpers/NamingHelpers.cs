using CodeBuilder.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Helpers
{
    public static class NamingHelpers
    {
        public static string GetRestfullMethodName(MethodStructure methodStructure)
        {
            var parameters = methodStructure.Parameters.Where(x => !x.Attributes.ContainsKey("FromBodyAttribute"));
            if (parameters.Count() > 0)
                return $"{methodStructure.Name}{"By" + parameters.Select(x => x.Name.Substring(0, 1).ToUpper() + x.Name.Substring(1)).Aggregate((a, b) => a + "And" + b)}";
            else
                return methodStructure.Name;
        }
    }
}
