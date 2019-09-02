using CodeBuilder.JS;
using CodeBuilder.JS.Builder.Properties;
using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder.Properties;
using CodeBuilder.JS.Types;
using ICodeBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Properties
{
    [TestClass]
    public class IFromObjectAssignmentPropertyTests
    {
        [TestMethod]
        public void NewAssingmentArrayFromObj()
        {
            var typeStructure = new TypeStructure
            {
                IsArray = true,
                IsSytemType = false,
                Name = "Persons",
                TypeName = "Person"
            };
            var jsType = new JSClassArray {
                ClassName = "Person"
            };

            var jsCode = ((JSRenderble)DI.Get<IFromObjectAssignmentProperty>(typeStructure, jsType)).GetText();

            Assert.AreEqual(jsCode, "/**\r\n    * @type Person[]\r\n*/\r\nthis.Persons = typeof dataObject !== \"undefined\" &&  typeof dataObject.Persons !== \"undefined\" ? dataObject.Persons.map(row => ( row => { let newObj = new Person(); newObj._loadFromObject(row); return newObj; })(row)) : [];\r\n");
        }
        [TestMethod]
        public void NewAssignmentFromObj()
        {
            var typeStructure = new TypeStructure
            {
                IsArray = false,
                IsSytemType = false,
                Name = "Person",
                TypeName = "PersonType"
            };
            var jsType = new JSClass
            {
                ClassName = "Person"
            };

            var jsCode = ((JSRenderble)DI.Get<IFromObjectAssignmentProperty>(typeStructure, jsType)).GetText();

            Assert.AreEqual(jsCode, "/**\r\n    * @type PersonType\r\n*/\r\nthis.Person = new PersonType();\r\nif (typeof dataObject.Person !== \"undefined\")\r\n{\r\n    this.Person._loadFromObject(dataObject.Person);\r\n}\r\n");
        }
        [TestMethod]
        public void AssignmentFromObj()
        {
            var typeStructure = new TypeStructure
            {
                IsArray = false,
                IsSytemType = true,
                Name = "Name",
                Type = typeof(string)
            };
            var jsType = new JSString();
           

            var jsCode = ((JSRenderble)DI.Get<IFromObjectAssignmentProperty>(typeStructure, jsType)).GetText();

            Assert.AreEqual(jsCode, "    /**\r\n    * @type String\r\n*/\r\n    this.Name = dataObject.Name;\r\n");
        }
    }
}
