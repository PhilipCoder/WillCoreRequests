using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBulder.JS.Types
{
    public class JSClassType : JSType
    {
        public override String JSTypeDef { get { return IsPromise ? $"PromiseLike<{ClassName}>" : ClassName; } set { } }
        public String ClassName { get; set; }
        public JSClassType()
        {
        }
    }
}
