using CodeBuilder.IBuilder;
using CodeBuilder.JS;
using CodeBuilder.JS.Properties;
using CodeBuilder.JS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWeb
{
    public class JSXMLComment : JSRenderble, IComment
    {
        public bool? IsPublic { get; set; }
        public JSType ReturnType { get; set; }
        public IDictionary<string, JSType> Params { get; set; }
        public JSType Type { get; set; }
        public string Description { get; set; }
        public JSXMLComment() {
            Params = new Dictionary<string, JSType>();
        }

        public new String GetText()
        {
            var result = new StringBuilder();
            result.AppendLine("//===============================================");
            if (Description != null)
            {
                result.AppendLine($"//<summary>{Description}</summary>");
            }
            if (IsPublic.HasValue)
            {
                result.AppendLine(IsPublic.Value ? "//<access>Public</access>" : "</access>Private</access>");
            }
            if (Type != null)
            {
                result.AppendLine($"//<returns>{Type.JSTypeDef}</returns>");
            }
            foreach (var key in Params.Keys)
            {
                result.AppendLine($"//<param>{Params[key]}</param>");
                result.AppendLine($"//<typeparam>{Params[key].JSTypeDef}</typeparam>");
            }
            if (ReturnType != null)
            {
                result.AppendLine($"//<returns>{ReturnType.JSTypeDef}</returns>");
            }
            result.Append("//===============================================");
            return result.ToString();
        }
    }
}
