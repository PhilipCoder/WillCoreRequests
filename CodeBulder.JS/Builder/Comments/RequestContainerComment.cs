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
    public class RequestContainerComment : JSRenderble, IRequestContainerComment
    {
        private const string descriptionTag = "description";

        public RequestContainerComment(ClassStructure classStructure) : base("PropertyComment")
        {
            tagValues = new Dictionary<string, string> {
                { descriptionTag, Configuration.Instance.Comments.RequestContainerDescription(classStructure.Name) }
            };
            childRenderbles.Add((JSRenderble)DI.Get<IParameterTypeComment>(new JSString(), "baseURL"));
        }
    }
}
