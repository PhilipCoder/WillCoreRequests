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
    public class ClassConstructorComment : JSRenderble, IClassConstructorComment
    {
        private const string descriptionTag = "description";

        public ClassConstructorComment(TypeStructure typeStructure) : base("ClassConstructorComment")
        {
            base.tagValues = new Dictionary<string, string> {
                { descriptionTag, Configuration.Instance.Comments.ResultClassDescription(Configuration.Instance.ModelsNameFactory(typeStructure.TypeName)) }
            };

            multiplyTags(typeStructure.Properties.Count, ((JSRenderble)DI.Get<IParameterTypeComment>(new JSString(), "")).Name);

            typeStructure.Properties.ForEach(property => childRenderbles.Add((JSRenderble)DI.Get<IParameterTypeComment>(JSTypeMapping.GetJSType(property), property.Name)));
        }


    }
}
