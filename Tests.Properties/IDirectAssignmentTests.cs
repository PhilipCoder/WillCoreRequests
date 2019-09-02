using CodeBuilder.JS;
using CodeBuilder.JS.Builder.Properties;
using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder.Properties;
using CodeBuilder.JS.Types;
using ICodeBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Properties
{
    [TestClass]
    public class IDirectAssignmentTests
    {
        [TestMethod]
        public void NewDirectClassAssingment()
        {
            var typeStructure = new TypeStructure
            {
                IsArray = false,
                IsSytemType = false,
                Name = "Person",
                TypeName = "PersonType",
                Properties = new List<TypeStructure>
                {
                    new TypeStructure
                    {
                        Name = "Age",
                        Type = typeof(int),
                        IsSytemType = true
                    },
                    new TypeStructure
                    {
                        Name = "Name",
                        Type = typeof(String),
                        IsSytemType = true
                    },
                    new TypeStructure
                    {
                        Name = "Details",
                        TypeName = "PersonDetails"
                    }
                }
            };
            var jsType = new CodeBuilder.JS.Types.JSClass
            {
                ClassName = "Person"
            };

            var jsCode = ((JSRenderble)DI.Get<IDirectAssignmentProperty>(typeStructure, jsType)).GetText();

            Assert.AreEqual(jsCode, "/**\r\n    * @type PersonType\r\n*/\r\nthis.Person = typeof Person !== \"undefined\" ? new PersonType(Person.Age,Person.Name,Person.Details) : null;\r\n");
        }

        [TestMethod]
        public void NewDirectAssingmentArray()
        {
            var typeStructure = new TypeStructure
            {
                IsArray = true,
                IsSytemType = false,
                Name = "Person",
                TypeName = "PersonType",
                Properties = new List<TypeStructure>
                {
                    new TypeStructure
                    {
                        Name = "Age",
                        Type = typeof(int),
                        IsSytemType = true
                    },
                    new TypeStructure
                    {
                        Name = "Name",
                        Type = typeof(String),
                        IsSytemType = true
                    },
                    new TypeStructure
                    {
                        Name = "Details",
                        TypeName = "PersonDetails"
                    }
                }
            };
            var jsType = new CodeBuilder.JS.Types.JSClassArray
            {
                ClassName = "Person"
            };

            var jsCode = ((JSRenderble)DI.Get<IDirectAssignmentProperty>(typeStructure, jsType)).GetText();

            Assert.AreEqual(jsCode, "/**\r\n    * @type PersonType[]\r\n*/\r\nthis.Person = typeof Person !== \"undefined\" ? Person.map(x=> new PersonType(x.Age,x.Name,x.Details)) : [];");
        }

        [TestMethod]
        public void NewDirectAssingment()
        {
            var typeStructure = new TypeStructure
            {
                IsArray = false,
                IsSytemType = true,
                Name = "Name",
                Type = typeof(string)
            };
            var jsType = new CodeBuilder.JS.Types.JSString(); 

            var jsCode = ((JSRenderble)DI.Get<IDirectAssignmentProperty>(typeStructure, jsType)).GetText();

            Assert.AreEqual(jsCode, "/**\r\n    * @type String\r\n*/\r\nthis.Name = Name;\r\n");
        }
    }
}
