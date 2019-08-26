using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBulder.JS.Types
{
    public class JSObject : JSType
    {
        public override string JSTypeDef { get { return IsPromise ? "PromiseLike<Object>" :  "Object"; } set { } }
    }
}
