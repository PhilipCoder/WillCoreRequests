using CodeBuilder.IBuilder;
using CodeBuilder.JS.Properties;
using CodeBuilder.JS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Builder
{
    public class JSComment : JSRenderble, IComment
    {
        public bool? IsPublic { get; set; }
        public JSType ReturnType { get; set; }
        public IDictionary<string, JSType> Params { get; set; }
        public JSType Type { get; set; }
        public string Description { get; set; }
        public JSComment() {
            Params = new Dictionary<string, JSType>();
        }

        public new String GetText()
        {
            var result = new StringBuilder();
            result.AppendLine("/**");
            if (Description != null)
            {
                result.AppendLine($"* {Description}");
            }
            if (IsPublic.HasValue)
            {
                result.AppendLine(IsPublic.Value ? "* @public" : "* @private");
            }
            if (Type != null)
            {
                result.AppendLine($"* @type {{{Type.JSTypeDef}}}");
            }
            foreach (var key in Params.Keys)
            {
                result.AppendLine($"* @param {{{Params[key].JSTypeDef}}} {key}");
            }
            if (ReturnType != null)
            {
                result.AppendLine($"* @return {{{ReturnType.JSTypeDef}}}");
            }
            result.Append($"*/");
            return result.ToString();
        }
    }
}
