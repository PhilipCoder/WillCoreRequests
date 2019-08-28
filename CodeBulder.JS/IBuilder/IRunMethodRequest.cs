using System.Collections.Generic;

namespace CodeBuilder.IBuilder
{ 
    public interface IRunMethodRequest
    {
        IComment Comment { get; set; }
        string Name { get; set; }
        List<string> Parameters { get; set; }
    }
}