using CodeBuilder.JS.Builder.Comments;
using CodeBuilder.JS.Types;
using ICodeBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestWeb.Models;

namespace Tests.Parameters
{
    [TestClass]
    public class PropertyCommentTests
    {
        [TestMethod]
        public void TestJSDateArray()
        {
            var typeStructure = new TypeStructure
            {
                IsArray = true,
                Name = "Test Property",
                Type = typeof(DateTime),
                IsSytemType = true
            };
            var propertyComment = new PropertyComment(typeStructure).GetText();
            Assert.AreEqual("/**\r\n    * @type Date[]\r\n*/", propertyComment);
        }

        [TestMethod]
        public void TestJSClass()
        {
            var typeStructure = new TypeStructure
            {
                IsArray = true,
                Name = "Test Property",
                TypeName = "Person",
                Type = typeof(Person),
                IsSytemType = false
            };
            var propertyComment = new PropertyComment(typeStructure).GetText();
            Assert.AreEqual("/**\r\n    * @type Person[]\r\n*/", propertyComment);
        }
    }
}
