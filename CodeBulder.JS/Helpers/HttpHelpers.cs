using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Helpers
{
    public static class HttpHelpers
    {
        public static string GetHTTPMethod(MethodStructure methodStructure)
        {
            return methodStructure.Attributes.ContainsKey("HttpPostAttribute") ? "POST" :
                methodStructure.Attributes.ContainsKey("HttpPutAttribute") ? "PUT" :
                methodStructure.Attributes.ContainsKey("HttpDeleteAttribute") ? "DELETE" :
                 methodStructure.Attributes.ContainsKey("HttpPatchAttribute") ? "PATCH" : "GET";
        }
        public static string GetParameterSource(TypeStructure methodStructure)
        {
            return methodStructure.Attributes.ContainsKey("FromBodyAttribute") ? "BODY" : "";
        }

        public static string GetRequestParametersSourceObject(MethodStructure methodStructure)
        {
            return methodStructure.Parameters.Any()
                ? "{" + methodStructure.Parameters.Select(x => getSourceParameters(methodStructure, x)).Aggregate((a, b) => a + "," + b) + "}"
                : "{}";
        }

        private static string getSourceParameters(MethodStructure methodStructure, TypeStructure typeStructure)
        {
            return $"{typeStructure.Name}:{(typeStructure.Attributes.ContainsKey("FromBodyAttribute") ? "\"BODY\"" : methodStructure.URL.Contains($"{{{typeStructure.Name}}}") ? "\"URL\"" : "\"QUERY\"")}";
        }
    }
}
