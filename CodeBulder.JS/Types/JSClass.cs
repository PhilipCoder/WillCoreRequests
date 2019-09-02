using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Types
{
    public class JSClass : JSType
    {
        public override String JSTypeDef { get { return IsPromise ? $"PromiseLike<{ClassName}>" : $"{ClassName}"; } set { } }
        public String ClassName { get; set; }
        public JSClass()
        {
        }
    }
}
