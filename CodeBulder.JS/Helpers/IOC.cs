using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Helpers
{
    public static class DI
    {
        public static T Get<T>(params object[] parameters) 
        {
            return Configuration.Instance.IOCContainer.GetInstance<T>(parameters);
        }
    }
}
