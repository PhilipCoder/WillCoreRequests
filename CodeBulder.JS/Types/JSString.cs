using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Types
{
    public class JSString : JSType
    {
        public override string JSTypeDef { get { return IsPromise ? "PromiseLike<String>" : "String"; } set { } }
    }
}
