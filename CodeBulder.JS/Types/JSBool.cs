using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Types
{
    public class JSBool : JSType
    {
        public override string JSTypeDef { get { return IsPromise ? "PromiseLike<Boolean>" : "Boolean"; } set { } }
    }
}
