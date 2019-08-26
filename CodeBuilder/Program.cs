using CodeBulder.JS;
using CodeBulder.JS.Builder;
using CodeBulder.JS.Types;
using ContractExtractor;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CodeBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var classContainer = (JSClassContainer<ControllerBase>)new ClassTraveler().TravelClasses<ControllerBase>(new JSClassContainer<ControllerBase>(ESMode.ES6) { });
            var jsClassContext = new JSClassContext<ControllerBase>(classContainer);
            //var js = new JSClass();
            //js.Name = "CoreLogic";
            //js.Extends = "SomeBaseClass";
            //js.ConstructorParamters.Add("data");
            //js.Imports.Add(new Import { Modules = new List<string> { "moduleA", "moduleB" }, URL = "./JS/myModule.js" });
            //js.Imports.Add(new Import { Modules = new List<string> { "moduleC", "moduleD" }, URL = "./JS/myModuleA.js" });
            //js.ClassComment = new Comment
            //{
            //    Description = "Some class here",
            //    IsPublic = true
            //};
            //js.HeaderComment = new Comment
            //{
            //    Description = "Author: DrPhil"
            //};
            //js.ConstructorComment = new Comment
            //{
            //    Description = "Creates A new Instance"
            //};
            //js.ConstructorComment.Params.Add("data", new JSArray(new JSObject()));
            //js.jsProperties.Add(new JSProperty
            //{
            //    Comment = new Comment { IsPublic = false, Type = new JSObject { } },
            //    Name = "_UpdateDomainMLEkey",
            //    Assignable = new AssignAble
            //    { NewInstanceType = "request", NewInstanceParamters = new List<string> { "this.baseUrl", "DomainApi/UpdateDomainMLEkey", "POST", "null", "null" } }
            //});
            //js.jsProperties.Add(new JSProperty
            //{
            //    Comment = new Comment
            //    {
            //        IsPublic = false,
            //        Type = new JSString { }
            //    },
            //    Name = "baseURL",
            //    Assignable = new AssignAble
            //    {
            //        ObjectAssignment = "data"
            //    }
            //});
            //js.jsProperties.Add(new JSProperty
            //{
            //    Comment = new Comment
            //    {
            //        Description = "The base url for the requests",
            //        Type = new JSString { }
            //    },
            //    Name = "baseURL",
            //    Assignable = new AssignAble
            //    {
            //        ObjectAssignment = "data",
            //        PropertyAssignment = "field"
            //    }
            //});
            //js.Methods.Add(new RunMethodRequest
            //{
            //    Comment = new Comment
            //    {
            //        Description = "Method to invoke POST request to AccountApi/ChangePassword.",
            //        Params = new Dictionary<string, JSType> {
            //            { "userId", new JSString()},
            //            { "newPassword", new JSString()}
            //        },
            //        Type = new JSArray(new JSClassType { ClassName = "ChangePasswordResult" }) { IsPromise = true },
            //    },
            //    Parameters = new List<string> { "userId", "newPassword" },
            //    Name = "ChangePassword"
            //});
            //js.Export = new Export
            //{
            //    Modules = new List<string> { "CoreLogic" }
            //};
            //var result = js.GetText();
            //Console.Write(result);
            Console.ReadLine();
        }
    }
}
