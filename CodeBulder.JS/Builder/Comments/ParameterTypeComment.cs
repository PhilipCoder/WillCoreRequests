using CodeBuilder.JS.IBuilder.Comments;
using CodeBuilder.JS.Properties;
using CodeBuilder.JS.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Builder.Comments
{
    public class ParameterTypeComment : JSRenderble, IParameterTypeComment
    {
        private const string typeTag = "type";
        private const string nameTag = "name";

        public ParameterTypeComment() : base("ParameterTypeComment") { }

        public ParameterTypeComment(JSType jstype, String parameterName) : base("ParameterTypeComment")
        {
            base.tagValues = new Dictionary<string, string> {
                { typeTag, jstype.JSTypeDef },
                { nameTag,parameterName }
            };
        }
    }
}
