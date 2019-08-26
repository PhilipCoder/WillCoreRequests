using CodeBuilder.CoreBuilder;
using Microsoft.AspNetCore.Builder;
using System;

namespace ContractExtractor
{
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder GenerateJSContext<T>(this IApplicationBuilder builder, ClassContainter<T> classContainter, bool resultCamelCase = true)
        {
            return builder.UseMiddleware<RequestContextBuilder<T>>(classContainter, resultCamelCase);
        }
    }
}
