using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Types
{
    public abstract class JSType
    {
        public bool IsPromise { get; set; }
        public virtual String JSTypeDef { get; set; }
        public JSType()
        {

        }

        public JSType(JSType secondaryType)
        {

        }
    }
}
