using CodeBuilder.IBuilder;
using CodeBuilder.JS;
using CodeBuilder.JS.Helpers;
using ICodeBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ImportExport
{
    [TestClass]
    public class ExportTests
    {
        [TestMethod]
        public void Export()
        {
            var typeStructure = new TypeStructure
            {
                IsArray = false,
                IsSytemType = false,
                Name = "Person",
                TypeName = "PersonType"
            };
           
            var jsCode = ((JSRenderble)DI.Get<IExport>(typeStructure)).GetText();

            Assert.AreEqual(jsCode, "export {PersonType};");
        }
    }
}
