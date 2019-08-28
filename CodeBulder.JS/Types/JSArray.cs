using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Types
{
    public class JSArray : JSType
    {
        public override String JSTypeDef { get { return IsPromise ? $"PromiseLike<{JSSecondTypeDef.JSTypeDef}[]>" :  $"{JSSecondTypeDef.JSTypeDef}[]"; } set { } }
        public JSType JSSecondTypeDef { get; set; }
        public JSArray(JSType secondaryType)
        {
            JSSecondTypeDef = secondaryType;
        }
    }
}
