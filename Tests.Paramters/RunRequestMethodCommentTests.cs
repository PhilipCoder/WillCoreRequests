using CodeBuilder.IBuilder;
using CodeBuilder.JS;
using CodeBuilder.JS.Builder.Comments;
using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder.Comments;
using CodeBuilder.JS.Types;
using ICodeBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestWeb.Models;

namespace Tests.Parameters
{
    [TestClass]
    public class RunRequestMethodCommentTests
    {
        [TestMethod]
        public void RunRequestMethodComment()
        {
            var methodStructure = new MethodStructure
            {
                URL = "/Person/Add",
                Result = new TypeStructure
                {
                    IsSytemType = false,
                    IsArray = true,
                    TypeName = "AddPersonResult",
                },
                Parameters = new List<TypeStructure>
               {
                    new TypeStructure{ Name = "Id",IsSytemType=true, Type = typeof(int) },
                    new TypeStructure{ Name = "Name",IsSytemType=true, Type = typeof(string) },
                    new TypeStructure{ Name = "DateOfBirth",IsSytemType=true, Type = typeof(DateTime) },
                    new TypeStructure{ Name = "Details",IsSytemType=false, TypeName = "PersonDetails" }
               },
                Attributes = new Dictionary<string, Attribute> {
                    { "HttpPostAttribute", new IgnoreAttribute() }
                }
            };
            var jsCode = ((JSRenderble)DI.Get<IRunRequestMethodComment>(methodStructure)).GetText();
            Assert.AreEqual(jsCode, " /**\r\n    * Method used to invoke request of type: POST to URL: /Person/Add.\r\n    * @param { Number } Id\r\n    * @param { String } Name\r\n    * @param { Date } DateOfBirth\r\n    * @param { PersonDetails } Details\r\n    * @return {PromiseLike<AddPersonResult[]>}\r\n*/");
        }
    }
}
