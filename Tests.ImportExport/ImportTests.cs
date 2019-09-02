using CodeBuilder.IBuilder;
using CodeBuilder.JS;
using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder;
using ICodeBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ImportExport
{
    [TestClass]
    public class ImportTests
    {
        [TestMethod]
        public void RequestContextImport()
        {
            var typeStructure = new TypeStructure
            {
                IsArray = false,
                IsSytemType = false,
                Name = "Person",
                TypeName = "PersonType"
            };
           
            var jsCode = ((JSRenderble)DI.Get<IRequestContextImport>(typeStructure)).GetText();

            Assert.AreEqual(jsCode, "import {PersonType} from \"./models/PersonType.js\";");
        }

        [TestMethod]
        public void POCOImport()
        {
            var typeStructure = new TypeStructure
            {
                IsArray = false,
                IsSytemType = false,
                Name = "Person",
                TypeName = "PersonType"
            };

            var jsCode = ((JSRenderble)DI.Get<IImport>(typeStructure)).GetText();

            Assert.AreEqual(jsCode, "import {PersonType} from \"./PersonType.js\";");
        }
    }
}
