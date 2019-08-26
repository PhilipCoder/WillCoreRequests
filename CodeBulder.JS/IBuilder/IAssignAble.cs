using System.Collections.Generic;
using CodeBulder.JS.Types;

namespace CodeBulder.IBuilder
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