using ICodeBuilder.IOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICodeBuilder
{
    /// <summary>
    /// Configuration contract, specifying as singleton.
    /// </summary>
    public interface IConfiguration
    {
        IOCContainer IOCContainer { get; set; }
        bool APIConfiguredForCamelCase { get; set; }
    }
}
