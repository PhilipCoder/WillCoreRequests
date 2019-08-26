using CodeBuilder.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBulder.JS.Helpers
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
    }
}
