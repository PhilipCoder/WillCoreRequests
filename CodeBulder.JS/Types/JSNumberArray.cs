using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Types
{
    public class JSNumberArray : JSType
    {
        public override string JSTypeDef { get { return IsPromise ? "PromiseLike<Number[]>" : "Number[]"; } set { } }
    }
}
