using CodeBuilder.IBuilder;
using CodeBuilder.JS.Builder;
using CodeBuilder.JS.Properties;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using ICodeBuilder;
using ICodeBuilder.Attributes;
using CodeBuilder.JS.Helpers;

namespace CodeBuilder.JS
{
    public class JavaScript : ClassContainter
    {
        /// <summary>
        /// Public accessable reference to the static IOC container used to overide built in code generation operations.
        /// </summary>
        public override IConfiguration Config { get; set; } = Configuration.Instance;
        public JavaScript()
        {
            recursionConfiguration = new RecursionConfiguration
            {
                ClassIncludeFilterAttributes = new List<Type> { },
                AttributeExcludeFilterAttributes = new List<Type> { },
                ClassExcludeFilterAttributes = new List<Type> { typeof(ExcludeFromAPIContract) },
                MethodExcludeFilterAttributes = new List<Type> { typeof(ExcludeFromAPIContract) },
                MethodIncludeFilterAttributes = new List<Type> { },
                ParameterExcludeFilterAttributes = new List<Type> { },
                ParameterIncludeFilterAttributes = new List<Type> { },
                PropertyExcludeFilterAttributes = new List<Type> { },
                PropertyIncludeFilterAttributes = new List<Type> { },
                AttributeIncludeFilterAttributes = new List<Type> {
                    typeof(RouteAttribute),
                    typeof(HttpGetAttribute),
                    typeof(HttpDeleteAttribute),
                    typeof(HttpPostAttribute),
                    typeof(HttpPutAttribute),
                    typeof(HttpPatchAttribute),
                    typeof(FromBodyAttribute),
                    typeof(FromFormAttribute),
                    typeof(FromHeaderAttribute),
                    typeof(FromQueryAttribute)
                }
            };
        }


        public override void BuildCode(string path)
        {
            DI.Get<IJSCodeBuilder>(this);
        }
    }
}
