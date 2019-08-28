using System.Collections.Generic;
using CodeBuilder.JS.Types;

namespace CodeBuilder.IBuilder
{
    public interface IAssignAble : IRenderble
    {
        IEnumerable<string> NewInstanceParamters { get; set; }
        string NewInstanceType { get; set; }
        string ObjectAssignment { get; set; }
        string PropertyAssignment { get; set; }
        JSType Type { get; set; }
    }
}