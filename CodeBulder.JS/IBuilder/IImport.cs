using System;
using System.Collections.Generic;

namespace CodeBuilder.IBuilder
{ 
    public interface IImport : IRenderble
    {
        IEnumerable<string> Modules { get; set; } 
        string URL { get; set; }
    }
}