using System.Collections.Generic;
using CodeBuilder.IBuilder;
using CodeBuilder.JS.Builder;

namespace CodeBuilder.IBuilder
{
    public interface IJSClass : IRenderble
    {
        IComment ClassComment { get; set; }
        IComment ConstructorComment { get; set; }
        IComment HeaderComment { get; set; }
        IEnumerable<string> ConstructorParamters { get; set; }
        IEnumerable<IImport> Imports { get; set; }
        IEnumerable<IJSProperty> jsProperties { get; set; }
        IEnumerable<IRenderble> Methods { get; set; }
        IExport Export { get; set; }
        string Name { get; set; }
        string Extends { get; set; }
        string GetText();
    }
}