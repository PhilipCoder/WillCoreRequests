using CodeBuilder.CoreBuilder;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ContractExtractor
{
    public class RequestContextBuilder<T> 
    {
        private readonly RequestDelegate _next;

        public RequestContextBuilder(RequestDelegate next, ClassContainter<T> classContainter, bool resultCamelCase = true)
        {
            var codeBuilder = new ClassTraveler(resultCamelCase).TravelClasses<T>(classContainter);
            classContainter.BuildCode("/js");
            Debug.WriteLine("JS context built!");
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            return _next(context);
        }
    }
}
