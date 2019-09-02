using ICodeBuilder;
using ICodeBuilder.IOC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeBuilder
{
    public static class RequestCultureMiddlewareExtensions
    {
        /// <summary>
        /// Configures WillCore.Requests reflection.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configureReflection">Action to set the configuration.</param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureWillCoreReflection(this IApplicationBuilder builder, Action<RecursionConfiguration> configureReflection)
        {
            var requestBuilder = builder.ApplicationServices.GetService<IRequestContextBuilder>();
            configureReflection(requestBuilder.ClassContainter.recursionConfiguration);
            return builder;
        }
        /// <summary>
        /// Generates WillCore.Reqeusts code.
        /// </summary>
        /// <typeparam name="T">The base class the recursion will be performed on.</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder WillCoreBuildMyCode<T>(this IApplicationBuilder builder)
        {
            var requestBuilder = builder.ApplicationServices.GetService<IRequestContextBuilder>();
            requestBuilder.ClassContainter.recursionConfiguration.ControllerType = typeof(T);
            requestBuilder.BuildCode();
            return builder;
        }
        /// <summary>
        /// Registers WillCore.Requests as a service.
        /// </summary>
        /// <typeparam name="T">Instance of a ClassContainer</typeparam>
        /// <param name="services">Instance of a ClassContainer</param>
        /// <returns></returns>
        public static IOCMapper AddWillCoreRequests<T>(this IServiceCollection services) where T : ClassContainter, new()
        {
            var requestContext = new RequestContextBuilder(new T());
            services.AddSingleton<IRequestContextBuilder>(requestContext);
            return requestContext.ClassContainter.Config.IOCContainer.TypeMapper;
        }
    }
}
