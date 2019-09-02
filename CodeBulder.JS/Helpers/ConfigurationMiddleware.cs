using ICodeBuilder.IOC;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS
{
    public static class ConfigurationMiddleware
    {
        /// <summary>
        /// Configures WillCore.Request's code building, only for JavaScript.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configureJavascript">Action to configure the code building.</param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureJavascript(this IApplicationBuilder builder, Action<Configuration, IOCMapper> configureJavascript)
        {
            configureJavascript(Configuration.Instance, Configuration.Instance.IOCContainer.TypeMapper);
            return builder;
        }
    }
}
