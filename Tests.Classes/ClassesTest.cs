using CodeBuilder.IBuilder;
using CodeBuilder.JS;
using CodeBuilder.JS.Builder;
using CodeBuilder.JS.Helpers;
using ICodeBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Tests.Classes
{
    [TestClass]
    public class ClassesTest
    {
        TypeStructure personTypeStructure = new TypeStructure
        {
            Name = "Person",
            TypeName = "Person",
            Attributes = new Dictionary<string, Attribute> { { "FromBodyAttribute", new IgnoreAttribute() } },
            Properties = new List<TypeStructure>
               {
                    new TypeStructure{ Name = "Id",IsSytemType=true, Type = typeof(int)},
                    new TypeStructure{ Name = "Name",IsSytemType=true, Type = typeof(string)},
                    new TypeStructure{ Name = "Surname",IsSytemType=true, Type = typeof(string)},
                    new TypeStructure{ Name = "DateOfBirth",IsSytemType=true, Type = typeof(DateTime)},
                    new TypeStructure{ Name = "Detail",IsSytemType=false, TypeName = "PersonDetails" , Properties = new List<TypeStructure>{
                        new TypeStructure{ Name = "Id",IsSytemType=true, Type = typeof(int)},
                         new TypeStructure{ Name = "DateOfBirth",IsSytemType=true, Type = typeof(DateTime)}
                    } },
                    new TypeStructure{ Name = "Details",IsSytemType=false, TypeName = "PersonDetails", IsArray = true, Properties = new List<TypeStructure>{
                        new TypeStructure{ Name = "Id",IsSytemType=true, Type = typeof(int)},
                         new TypeStructure{ Name = "DateOfBirth",IsSytemType=true, Type = typeof(DateTime)}
                    }  }
               }
        };

        TypeStructure successTypeStructure = new TypeStructure
        {
            Name = "ResultAction",
            TypeName = "ResultAction",
            Attributes = new Dictionary<string, Attribute> { },
            Properties = new List<TypeStructure>
               {
                    new TypeStructure{ Name = "Message",IsSytemType=true, Type = typeof(string)},
                    new TypeStructure{ Name = "Success",IsSytemType=true, Type = typeof(bool)},
               }
        };

        [TestMethod]
        public void TestPoco()
        {
            var jsCode = ((JSRenderble)DI.Get<IJSClass>(personTypeStructure)).GetText();
            Assert.AreEqual(jsCode, "import {PersonDetails} from \"./PersonDetails.js\";\r\n\r\nclass Person \r\n{\r\n     /**\r\n    * POCO class $Person\r\n    * @param { Number } Id\r\n    * @param { String } Name\r\n    * @param { String } Surname\r\n    * @param { Date } DateOfBirth\r\n    * @param { PersonDetails } Detail\r\n    * @param { PersonDetails[] } Details\r\n*/\r\n    constructor(Id,Name,Surname,DateOfBirth,Detail,Details) \r\n    {\r\n/**\r\n    * @type Number\r\n*/\r\nthis.Id = Id;\r\n\r\n/**\r\n    * @type String\r\n*/\r\nthis.Name = Name;\r\n\r\n/**\r\n    * @type String\r\n*/\r\nthis.Surname = Surname;\r\n\r\n/**\r\n    * @type Date\r\n*/\r\nthis.DateOfBirth = DateOfBirth;\r\n\r\n/**\r\n    * @type PersonDetails\r\n*/\r\nthis.Detail = typeof Detail !== \"undefined\" ? new PersonDetails(Detail.Id,Detail.DateOfBirth) : null;\r\n\r\n/**\r\n    * @type PersonDetails[]\r\n*/\r\nthis.Details = typeof Details !== \"undefined\" ? Details.map(x=> new PersonDetails(x.Id,x.DateOfBirth)) : [];\r\n    }\r\n    _loadFromObject(dataObject)\r\n    {\r\n        if (typeof dataObject === \"undefined\") return;\r\n    /**\r\n    * @type Number\r\n*/\r\n    this.Id = dataObject.Id;\r\n\r\n    /**\r\n    * @type String\r\n*/\r\n    this.Name = dataObject.Name;\r\n\r\n    /**\r\n    * @type String\r\n*/\r\n    this.Surname = dataObject.Surname;\r\n\r\n    /**\r\n    * @type Date\r\n*/\r\n    this.DateOfBirth = dataObject.DateOfBirth;\r\n\r\n/**\r\n    * @type PersonDetails\r\n*/\r\nthis.Detail = new PersonDetails();\r\nif (typeof dataObject.Detail !== \"undefined\")\r\n{\r\n    this.Detail._loadFromObject(dataObject.Detail);\r\n}\r\n\r\n/**\r\n    * @type PersonDetails[]\r\n*/\r\nthis.Details = typeof dataObject !== \"undefined\" &&  typeof dataObject.Details !== \"undefined\" ? dataObject.Details.map(row => ( row => { let newObj = new PersonDetails(); newObj._loadFromObject(row); return newObj; })(row)) : [];\r\n        \r\n    }\r\n}\r\n\r\nexport {Person};");
        }


        [TestMethod]
        public void TestRequestContainer()
        {
            var idParameter = new TypeStructure
            {
                Attributes = new Dictionary<string, Attribute> { },
                IsSytemType = true,
                Name = "id",
                Type = typeof(int)
            };
            var classStructure = new ClassStructure
            {
                Attributes = new Dictionary<string, Attribute> { },
                Name = "Person",
                URL = "API/Person",
                Methods = new List<MethodStructure>
                {
                    new MethodStructure
                    {
                        Attributes = new Dictionary<string, Attribute>{ {"HttpGetAttribute", new IgnoreAttribute() } },
                        Name = "Get",
                        URL = "API/Person",
                        IsRPC = false,
                        Parameters = new List<TypeStructure>
                        {
                        },
                        Result = new TypeStructure
                        {
                            IsArray = true,
                            Name = personTypeStructure.Name,
                            Properties = personTypeStructure.Properties,
                            TypeName = personTypeStructure.TypeName
                        }
                    },
                    new MethodStructure
                    {
                        Attributes = new Dictionary<string, Attribute>{ {"HttpGetAttribute", new IgnoreAttribute() } },
                        Name = "Get",
                        URL = "API/Person/{id}",
                        IsRPC = false,
                        Parameters = new List<TypeStructure>
                        {
                            idParameter
                        },
                        Result = personTypeStructure
                    }
                    ,
                    new MethodStructure
                    {
                        Attributes = new Dictionary<string, Attribute>{ {"HttpPutAttribute", new IgnoreAttribute() } },
                        Name = "Put",
                        URL = "API/Person/{id}",
                        IsRPC = false,
                        Parameters = new List<TypeStructure>
                        {
                           idParameter,
                           personTypeStructure
                        },
                        Result = successTypeStructure
                    } ,
                    new MethodStructure
                    {
                        Attributes = new Dictionary<string, Attribute>{ {"HttpPostAttribute", new IgnoreAttribute() } },
                        Name = "Post",
                        URL = "API/Person",
                        IsRPC = false,
                        Parameters = new List<TypeStructure>
                        {
                            personTypeStructure
                        },
                        Result = successTypeStructure
                    } ,
                    new MethodStructure
                    {
                        Attributes = new Dictionary<string, Attribute>{ {"HttpDeleteAttribute", new IgnoreAttribute() } },
                        Name = "Delete",
                        URL = "API/Person/{id}",
                        IsRPC = false,
                        Parameters = new List<TypeStructure>
                        {
                            idParameter
                        },
                        Result = successTypeStructure
                    }
                }
            };

            var jsCode = ((JSRenderble)DI.Get<IJSClass>(classStructure)).GetText();
            Assert.AreEqual(jsCode, "import {request,globalTokens} from \"./request/request.js\";\r\nimport {Person} from \"./models/Person.js\";\r\nimport {PersonDetails} from \"./models/PersonDetails.js\";\r\nimport {ResultAction} from \"./models/ResultAction.js\";\r\n\r\nclass PersonRequestContainer \r\n{\r\n     /**\r\n    * PersonRequestContainer. Use instance to make requests to: API/Person\r\n    * @param { String } baseURL\r\n*/\r\n    constructor(baseUrl) \r\n    {\r\n/**\r\n    * @type String\r\n*/\r\nthis.baseUrl = baseUrl;\r\n\r\nthis._Get = new request(this.baseUrl,\"API/Person\",\"GET\",{}, Person);\r\n\r\nthis._GetById = new request(this.baseUrl,\"API/Person/{id}\",\"GET\",{id:\"URL\"}, Person);\r\n\r\nthis._PutById = new request(this.baseUrl,\"API/Person/{id}\",\"PUT\",{id:\"URL\",Person:\"BODY\"}, ResultAction);\r\n\r\nthis._Post = new request(this.baseUrl,\"API/Person\",\"POST\",{Person:\"BODY\"}, ResultAction);\r\n\r\nthis._DeleteById = new request(this.baseUrl,\"API/Person/{id}\",\"DELETE\",{id:\"URL\"}, ResultAction);\r\n\r\n    }\r\n    setHttpHeaders(headerObject)\r\n    {\r\n       for (var key in headerObject){\r\n            globalTokens[key] = headerObject[key];\r\n        }\r\n    }\r\n     /**\r\n    * Method used to invoke request of type: GET to URL: API/Person.\r\n<< ParameterTypeComment >>\r\n    * @return {PromiseLike<Person[]>}\r\n*/\r\n    Get ()\r\n    {\r\n        return this._Get.ExecuteRequest({});\r\n    }\r\n\r\n     /**\r\n    * Method used to invoke request of type: GET to URL: API/Person/{id}.\r\n    * @param { Number } id\r\n    * @return {PromiseLike<Person>}\r\n*/\r\n    GetById (id)\r\n    {\r\n        return this._GetById.ExecuteRequest({id:id});\r\n    }\r\n\r\n     /**\r\n    * Method used to invoke request of type: PUT to URL: API/Person/{id}.\r\n    * @param { Number } id\r\n    * @param { Person } Person\r\n    * @return {PromiseLike<ResultAction>}\r\n*/\r\n    PutById (id,Person)\r\n    {\r\n        return this._PutById.ExecuteRequest({id:id,Person:Person});\r\n    }\r\n\r\n     /**\r\n    * Method used to invoke request of type: POST to URL: API/Person.\r\n    * @param { Person } Person\r\n    * @return {PromiseLike<ResultAction>}\r\n*/\r\n    Post (Person)\r\n    {\r\n        return this._Post.ExecuteRequest({Person:Person});\r\n    }\r\n\r\n     /**\r\n    * Method used to invoke request of type: DELETE to URL: API/Person/{id}.\r\n    * @param { Number } id\r\n    * @return {PromiseLike<ResultAction>}\r\n*/\r\n    DeleteById (id)\r\n    {\r\n        return this._DeleteById.ExecuteRequest({id:id});\r\n    }\r\n\r\n}\r\n\r\nexport {Person,PersonDetails,ResultAction,PersonRequestContainer};");
        }
    }
}
