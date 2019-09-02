using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS
{
    public class CommentConfiguration
    {
        /// <summary>
        /// Sets the class or function comments generated for result objects.
        /// </summary>
        public Func<string, string> ResultClassDescription { get; set; } = className => $"POCO class ${className}";
        /// <summary>
        /// Sets the class or function comments generated for request containers.
        /// </summary>
        public Func<string, string> RequestContainerDescription { get; set; } = className => $"Request Context.";
        /// <summary>
        /// Sets the constructor comment for request containers.
        /// </summary>
        public Func<string, string, string> RequestContainerConstructor { get; set; } = (className, url) => $"{className}. Use instance to make requests to: {url}";
        /// <summary>
        /// Sets the method description comments used to fire requests.
        /// </summary>
        public Func<string, string, string> RequestMethod { get; set; } = (httpMethod, url) => $"Method used to invoke request of type: {httpMethod} to URL: {url}.";
    }
}
