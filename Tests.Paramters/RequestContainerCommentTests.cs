using CodeBuilder.JS.Builder.Comments;
using CodeBuilder.JS.Types;
using ICodeBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestWeb.Models;

namespace Tests.Parameters
{
    [TestClass]
    public class RequestContainerCommentTests
    {
        [TestMethod]
        public void TestConstructorComment()
        {
            var comment = new RequestContainerComment(new ClassStructure()).GetText();
            Assert.AreEqual(" /**\r\n    * Request Context.\r\n    * @param { String } baseURL\r\n*/", comment);
        }
    }
}
