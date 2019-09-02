using CodeBuilder.IBuilder;
using CodeBuilder.JS;
using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder.Comments;
using CodeBuilder.JS.Properties;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Builder
{
    public class RunMethodRequest : JSRenderble, IRunRequestMethod
    {
        private const string nameTag = "name";
        private const string parameterTag = "parameters";
        private const string requestObjectTag = "requestObj";
        private const string controllerNameTag = "controllerName";

        public RunMethodRequest() : base("RunMethodRequest") { }
        public RunMethodRequest(MethodStructure methodStructure, string name) : base("RunMethodRequest")
        {
            tagValues = new Dictionary<string, string> {
                { controllerNameTag, name},
                { nameTag, methodStructure.IsRPC ? methodStructure.Name : NamingHelpers.GetRestfullMethodName(methodStructure) },
                { parameterTag, methodStructure.Parameters.GetCSV(x=>x.Name)},
                { requestObjectTag, methodStructure.Parameters.GetCSV(x=>$"{x.Name}:{x.Name}") }
            };
            childRenderbles.Add((JSRenderble)DI.Get<IRunRequestMethodComment>(methodStructure));
        }
    }
}
