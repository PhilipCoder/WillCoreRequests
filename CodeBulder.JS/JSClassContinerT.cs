using CodeBulder.IBuilder;
using CodeBulder.JS.Builder;
using CodeBulder.JS.Properties;
using CodeBuilder.CoreBuilder;
using CodeBuilder.Structure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using ContractExtractor.Attributes;

namespace CodeBulder.JS
{
    public class JSClassContainer<T> : ClassContainter<T>
    {
        public override Type[] AttributeIncludeFilterAttributes => new Type[] {
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
        };
        public override Type[] ClassIncludeFilterAttributes => new Type[] { };
        public override Type[] AttributeExcludeFilterAttributes => new Type[] { };
        public override Type[] ClassExcludeFilterAttributes => new Type[] { typeof(ExcludeFromAPIContract) };
        public override Type[] MethodExcludeFilterAttributes => new Type[] { typeof(ExcludeFromAPIContract) };
        public override Type[] MethodIncludeFilterAttributes => new Type[] { };
        public override Type[] ParameterExcludeFilterAttributes => new Type[] { };
        public override Type[] ParameterIncludeFilterAttributes => new Type[] { };
        public override Type[] PropertyExcludeFilterAttributes => new Type[] { };
        public override Type[] PropertyIncludeFilterAttributes => new Type[] { };

        /// <summary>
        /// Singleton instance of the class container.
        /// </summary>
        internal static JSClassContainer<T> ClassContainerInstance { get; set; }
        /// <summary>
        /// Public accessable reference to the static IOC container used to overide built in code generation operations.
        /// </summary>
        public JSBuilderIOCContainer InstanceConfiguration { get; set; } = JSBuilderIOCContainer.Instance;
        public Configuration Configuration { get; set; } = Configuration.Instance;
        public JSClassContainer()
        {
            JSClassContainer<T>.ClassContainerInstance = this;
        }
        

        public override void BuildCode(string path)
        {
            var jsClassContext = new JSClassContext<T>(this);
        }
    }
}
