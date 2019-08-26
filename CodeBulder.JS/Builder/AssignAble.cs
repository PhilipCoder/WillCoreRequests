using CodeBulder.IBuilder;
using CodeBulder.JS;
using CodeBulder.JS.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeBulder.JS.Builder
{
    public class JSPropertyAssignAble : JSRenderble, IAssignAble
    {
        public JSType Type { get; set; }
        public String ObjectAssignment { get; set; }
        public String PropertyAssignment { get; set; }
        public String NewInstanceType { get; set; }
        public IEnumerable<String> NewInstanceParamters { get; set; }

        public override String GetText()
        {
            string result = "null";
            if (ObjectAssignment != null)
            {
                result = ObjectAssignment;
                if (PropertyAssignment != null)
                {
                    result = $"{ObjectAssignment}.{PropertyAssignment}";
                }
            }
            else if (NewInstanceType != null)
            {
                var paramters = NewInstanceParamters != null && NewInstanceParamters.Any() ? NewInstanceParamters.Aggregate((a, b) => a + "," + b) : "";
                result = $"new {NewInstanceType}({paramters})";
            }
            return result;
        }
    }
}
