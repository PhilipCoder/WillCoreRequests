using CodeBulder.IBuilder;
using CodeBulder.JS;
using CodeBulder.JS.Helpers;
using CodeBulder.JS.Properties;
using CodeBulder.JS.Types;
using CodeBuilder.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeBulder.JS.Builder
{
    public class RunMethodFunction : JSRenderble, IRunMethodRequest
    {
        private const string MethodCommentTag = "<< method[comment] >>";
        private const string MethodNameTag = "<< method[name] >>";
        private const string PropertyAssignTag = "<< method[parameters] >>";
        private const string ParamberObjTag = "<< method[requestObj] >>";
        public String Name { get; set; }
        public IComment Comment { get; set; }
        public List<String> Parameters { get; set; }
        public RunMethodFunction() : base(Resources.runRequestMethod)
        {
        }

        public RunMethodFunction(MethodStructure methodStructure) : base(Resources.runRequestMethodFunction)
        {
            Name = methodStructure.IsRPC ? methodStructure.Name : NamingHelpers.GetRestfullMethodName(methodStructure);

            Comment = JSBuilderIOCContainer.Instance.CreateComment();
            Comment.Description = $"Method to invoke request to {methodStructure.URL}. Method: {HttpHelpers.GetHTTPMethod(methodStructure)}.";
            Comment.Params = methodStructure.Parameters.ToDictionary(k => k.Name, v => JSTypeMapping.GetJSType(v));
            Comment.ReturnType = JSTypeMapping.GetJSType(methodStructure.Result);
            Comment.ReturnType.IsPromise = true;

            Parameters = methodStructure.Parameters.Select(x => x.Name).ToList();
        }

        public override String GetText()
        {
            if (Name == null)
            {
                throw new InvalidDataException("Can't initialize a method without a name");
            }
            if (Comment != null)
            {
                Template = Template.Replace(MethodCommentTag, Comment.GetText());
            }
            Template = Template.Replace(MethodNameTag, Name);
            if (Parameters.Any())
            {
                var paramters = Parameters.Aggregate((a, b) => a + "," + b);
                Template = Template.Replace(PropertyAssignTag, paramters);
                var paramtersObj = Parameters.Select(x => x + ":" + x).Aggregate((a, b) => a + "," + b);
                Template = Template.Replace(ParamberObjTag, paramtersObj);
            }
            return Template;
        }
    }
}
