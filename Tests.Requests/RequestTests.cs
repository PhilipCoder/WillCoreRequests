using CodeBuilder.IBuilder;
using CodeBuilder.JS;
using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder.Requests;
using ICodeBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Tests.Requests
{
    [TestClass]
    public class RequestTests
    {
        [TestMethod]
        public void RunMethodRequest()
        {
            var methodStructure = new MethodStructure
            {
                URL = "/API/Person",
                Name = "Put",
                Result = new TypeStructure
                {
                    IsSytemType = false,
                    IsArray = true,
                    TypeName = "AddPersonResult",
                },
                Parameters = new List<TypeStructure>
               {
                    new TypeStructure{ Name = "Id",IsSytemType=true, Type = typeof(int),Attributes = new Dictionary<string, Attribute>{ } },
                    new TypeStructure{ Name = "Details",IsSytemType=false, TypeName = "PersonDetails" ,Attributes = new Dictionary<string, Attribute>{ { "FromBodyAttribute", new IgnoreAttribute() } } }
               },
                Attributes = new Dictionary<string, Attribute> {
                    { "HttpPutAttribute", new IgnoreAttribute() }
                }
            };
            var jsCode = ((JSRenderble)DI.Get<IRunRequestMethod>(methodStructure, "")).GetText();
            Assert.AreEqual(jsCode, "     /**\r\n    * Method used to invoke request of type: PUT to URL: /API/Person.\r\n    * @param { Number } Id\r\n    * @param { PersonDetails } Details\r\n    * @return {PromiseLike<AddPersonResult[]>}\r\n*/\r\n    PutById (Id,Details)\r\n    {\r\n        return this._PutById.ExecuteRequest({Id:Id,Details:Details});\r\n    }\r\n");
        }


        [TestMethod]
        public void RunRequestProperty()
        {
            var methodStructure = new MethodStructure
            {
                URL = "/API/Person",
                Name = "Put",
                Result = new TypeStructure
                {
                    IsSytemType = false,
                    IsArray = true,
                    TypeName = "AddPersonResult",
                },
                Parameters = new List<TypeStructure>
               {
                    new TypeStructure{ Name = "Id",IsSytemType=true, Type = typeof(int),Attributes = new Dictionary<string, Attribute>{ } },
                    new TypeStructure{ Name = "Details",IsSytemType=false, TypeName = "PersonDetails" ,Attributes = new Dictionary<string, Attribute>{ { "FromBodyAttribute", new IgnoreAttribute() } } }
               },
                Attributes = new Dictionary<string, Attribute> {
                    { "HttpPutAttribute", new IgnoreAttribute() }
                }
            };
            var jsCode = ((JSRenderble)DI.Get<IRunRequestProperty>(methodStructure)).GetText();
            Assert.AreEqual(jsCode, "this._PutById = new request(this.baseUrl,\"/API/Person\",\"PUT\",{Id:\"QUERY\",Details:\"BODY\"}, AddPersonResult);\r\n");
        }
    }
}
