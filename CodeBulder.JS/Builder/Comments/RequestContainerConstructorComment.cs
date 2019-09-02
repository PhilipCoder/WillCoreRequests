using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder.Comments;
using CodeBuilder.JS.Properties;
using CodeBuilder.JS.Types;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Builder.Comments
{
    public class RequestContainerConstructorComment : JSRenderble, IClassConstructorComment
    {
        private const string descriptionTag = "description";

        public RequestContainerConstructorComment(ClassStructure classStructure) : base("ClassConstructorComment")
        {
            base.tagValues = new Dictionary<string, string> {
                 { descriptionTag,Configuration.Instance.Comments.RequestContainerConstructor(Configuration.Instance.RequestContextNameFactory(classStructure.Name), classStructure.URL) }
            };

            childRenderbles.Add((JSRenderble)DI.Get<IParameterTypeComment>(new JSString(), "baseURL"));
        }


    }
}
