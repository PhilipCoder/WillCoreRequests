using System.Collections.Generic;
using CodeBuilder.IBuilder;

namespace CodeBuilder.JS.Builder
{
    public interface IJSCodeBuilder
    {
        List<IJSClass> Classes { get; set; }
    }
}