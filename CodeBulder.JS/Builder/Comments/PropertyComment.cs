using CodeBuilder.JS.IBuilder.Comments;
using CodeBuilder.JS.Properties;
using CodeBuilder.JS.Types;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Builder.Comments
{
    public class PropertyComment : JSRenderble, IPropertyComment
    {
        private const string typeTag = "type";

        public PropertyComment(TypeStructure typeStructure) : base("PropertyComment")
        {
            var jsType = JSTypeMapping.GetJSType(typeStructure);
            base.tagValues = new Dictionary<string, string> {
                { typeTag, jsType.JSTypeDef }
            };
        }
    }
}
