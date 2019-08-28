using CodeBuilder.JS.Builder;

namespace CodeBuilder.IBuilder
{
    public interface IJSProperty : IRenderble
    {
        IAssignAble Assignable { get; set; }
        IComment Comment { get; set; }
        string Name { get; set; }
    }
}