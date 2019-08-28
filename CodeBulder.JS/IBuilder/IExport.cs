using System.Collections.Generic;

namespace CodeBuilder.IBuilder
{
    public interface IExport : IRenderble
    {
        IEnumerable<string> Modules { get; set; }
    }
}