using CodeBuilder.JS.IBuilder.Comments;
using CodeBuilder.JS.Properties;
using CodeBuilder.JS.Types;
using System;
using System.Collections.Generic;
using System.Text;
using CodeBuilder.JS.Helpers;
using ICodeBuilder;

namespace CodeBuilder.JS.Builder.Comments
{
    public class RunRequestMethodComment : JSRenderble, IRunRequestMethodComment
    {
        private const string descriptionTag = "description";
        private const string resultTypeTag = "resultType";

        public RunRequestMethodComment(MethodStructure methodStructure) : base( "RunRequestMethodComment")
        {
            tagValues = new Dictionary<string, string> {
                { descriptionTag, Configuration.Instance.Comments.RequestMethod(HttpHelpers.GetHTTPMethod(methodStructure),methodStructure.URL) },
                { resultTypeTag, JSTypeMapping.GetJSType(methodStructure.Result, true).JSTypeDef }
            };

            multiplyTags(methodStructure.Parameters.Count, ((JSRenderble)DI.Get<IParameterTypeComment>()).Name);

            methodStructure.Parameters.ForEach(parameter => 
                childRenderbles.Add((JSRenderble)DI.Get<IParameterTypeComment>(JSTypeMapping.GetJSType(parameter), parameter.Name))
            );
        }
    }
}
