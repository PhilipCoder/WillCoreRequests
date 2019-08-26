using System;
using System.Collections.Generic;

namespace CodeBulder.IBuilder
{ 
    public interface IImport : IRenderble
    {
        IEnumerable<string> Modules { get; set; } 
        string URL { get; set; }
    }
}