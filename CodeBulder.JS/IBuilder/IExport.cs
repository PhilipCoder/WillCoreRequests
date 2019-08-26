using System.Collections.Generic;

namespace CodeBulder.IBuilder
{
    public interface IExport : IRenderble
    {
        IEnumerable<string> Modules { get; set; }
    }
}