using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBulder.JS.Types
{
    public class JSNumber : JSType
    {
        public override string JSTypeDef { get { return IsPromise ? "PromiseLike<Number>" : "Number"; } set { } }
    }
}
