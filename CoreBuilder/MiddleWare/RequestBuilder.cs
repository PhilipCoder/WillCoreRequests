using ContractExtractor;
using ICodeBuilder;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CodeBuilder
{
    public class RequestContextBuilder : IRequestContextBuilder
    {
        public readonly RequestDelegate _next;
        public ClassContainter ClassContainter { get; set; }

        public RequestContextBuilder( ClassContainter classContainter)
        {
            ClassContainter = classContainter;
        }

        public void BuildCode()
        {
            var codeBuilder = new ClassTraveler(ClassContainter.Config.APIConfiguredForCamelCase).TravelClasses(ClassContainter);
            ClassContainter.BuildCode("/js");
            Debug.WriteLine("JS context built!");
        }

        public Task InvokeAsync(HttpContext context)
        {
            return _next(context);
        }
    }
}
