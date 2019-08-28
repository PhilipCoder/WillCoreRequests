using CodeBuilder.CoreBuilder;
using CodeBuilder.JS.Builder;
using CodeBuilder.JS.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS
{
    public class Configuration
    {
        private ESMode _esmode { get; set; }
        public String SingleFileOutputName { get; set; } = "requestContext.js";
        public String ModelsFolder { get; set; } = "models";
        public String OutputDirectory { get; set; } = "js";
        public bool MultiFileOutput { get; set; } = true;
        public ESMode ESMode
        {
            get { return _esmode; }
            set
            {
                if (value == ESMode.ES6)
                {
                    MultiFileOutput = true;
                    JSBuilderIOCContainer.Instance.CreateJSClass = () => new JSClass();
                    JSBuilderIOCContainer.Instance.CreateJSClassFromStructure = (structure) => new JSClass(structure);
                    JSBuilderIOCContainer.Instance.CreateJSClassFromTypeStructure = (typeStructure) => new JSClass(typeStructure);
                    JSBuilderIOCContainer.Instance.CreateRunMethodRequest = () => new RunMethodRequest();
                    JSBuilderIOCContainer.Instance.CreateRunMethodRequestFromMethod = (method) => new RunMethodRequest(method);
                    JSBuilderIOCContainer.Instance.AdditionalFiles = new Dictionary<string, string> {
                        {"request\\request.js", Resources.request }
                    };
                }
                else
                {
                    MultiFileOutput = false;
                    JSBuilderIOCContainer.Instance.CreateJSClass = () => new JSFunction();
                    JSBuilderIOCContainer.Instance.CreateJSClassFromStructure = (structure) => new JSFunction(structure);
                    JSBuilderIOCContainer.Instance.CreateJSClassFromTypeStructure = (typeStructure) => new JSFunction(typeStructure);
                    JSBuilderIOCContainer.Instance.CreateRunMethodRequest = () => new RunMethodFunction();
                    JSBuilderIOCContainer.Instance.CreateRunMethodRequestFromMethod = (method) => new RunMethodFunction(method);
                    JSBuilderIOCContainer.Instance.AdditionalFiles = new Dictionary<string, string> {
                        {"request\\request.js", Resources.requestES5 }
                    };
                }
                _esmode = value;
            }
        }
        public static Configuration Instance = new Configuration();
    }
}
