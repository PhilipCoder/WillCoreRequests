using CodeBuilder.JS.Types;
using System.Collections.Generic;

namespace CodeBuilder.IBuilder
{
    public interface IComment : IRenderble
    {
        bool? IsPublic { get; set; }
        JSType ReturnType { get; set; }
        IDictionary<string, JSType> Params { get; set; }
        JSType Type { get; set; }
        string Description { get; set; }
    }
}