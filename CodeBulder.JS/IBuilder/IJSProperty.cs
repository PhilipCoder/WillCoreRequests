using CodeBulder.JS.Builder;

namespace CodeBulder.IBuilder
{
    public interface IJSProperty : IRenderble
    {
        IAssignAble Assignable { get; set; }
        IComment Comment { get; set; }
        string Name { get; set; }
    }
}