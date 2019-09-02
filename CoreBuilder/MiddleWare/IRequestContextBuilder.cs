using System.Threading.Tasks;
using ICodeBuilder;
using Microsoft.AspNetCore.Http;

namespace CodeBuilder
{
    public interface IRequestContextBuilder
    {
        ClassContainter ClassContainter { get; set; }
        Task InvokeAsync(HttpContext context);

        void BuildCode();
    }
}