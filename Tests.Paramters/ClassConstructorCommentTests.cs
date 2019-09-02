using CodeBuilder.JS.Builder.Comments;
using CodeBuilder.JS.Types;
using ICodeBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestWeb.Models;

namespace Tests.Parameters
{
    [TestClass]
    public class ClassConstructorCommentTests
    {
        [TestMethod]
        public void TestClassConstructorComment()
        {
            var newTypeStructure = new TypeStructure
            {
                TypeName = "Person",
                Properties = new List<TypeStructure>
                {
                    new TypeStructure{ Name = "Id",IsSytemType=true, Type = typeof(int) },
                    new TypeStructure{ Name = "Name",IsSytemType=true, Type = typeof(string) },
                    new TypeStructure{ Name = "DateOfBirth",IsSytemType=true, Type = typeof(DateTime) },
                    new TypeStructure{ Name = "Details",IsSytemType=false, TypeName = "PersonDetails" },
                }
            };
            var comment = new ClassConstructorComment(newTypeStructure).GetText();
            Assert.AreEqual(" /**\r\n    * POCO class $Person\r\n    * @param { Number } Id\r\n    * @param { String } Name\r\n    * @param { Date } DateOfBirth\r\n    * @param { PersonDetails } Details\r\n*/", comment);
        }
    }
}
