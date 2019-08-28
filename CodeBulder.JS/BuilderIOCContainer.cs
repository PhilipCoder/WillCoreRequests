using CodeBuilder.IBuilder;
using CodeBuilder.JS.Builder;
using CodeBuilder.Structure;
using System;
using System.Collections.Generic;
using System.Text;
using CodeBuilder.JS.Properties;

namespace CodeBuilder.JS
{
    public class JSBuilderIOCContainer
    {
        private static JSBuilderIOCContainer _instance { get; set; }
        public static JSBuilderIOCContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JSBuilderIOCContainer();
                }
                return _instance;
            }
        }
        public Func<IJSClass> CreateJSClass { get; set; }
        public Func<ClassStructure, IJSClass> CreateJSClassFromStructure { get; set; }
        public Func<TypeStructure, IJSClass> CreateJSClassFromTypeStructure { get; set; }

        public Func<IComment> CreateComment { get; set; }

        public Func<IJSProperty> CreateProperty { get; set; }
        public Func<MethodStructure, IJSProperty> CreatePropertyFromMethod { get; set; }

        public Func<IAssignAble> CreateAssignable { get; set; }

        public Func<IRunMethodRequest> CreateRunMethodRequest { get; set; }
        public Func<MethodStructure, IRunMethodRequest> CreateRunMethodRequestFromMethod { get; set; }
        public Func<IExport> CreateExport { get; set; }
        public Func<IImport> CreateImport { get; set; }
        public bool MultiFileOutput { get; set; }
        public Dictionary<string, string> AdditionalFiles = new Dictionary<string, string> {
            {"request\\request.js", Resources.request }
        };

        public JSBuilderIOCContainer()
        {
            CreateJSClass = () => new JSClass();
            CreateJSClassFromStructure = (structure) => new JSClass(structure);
            CreateJSClassFromTypeStructure = (typeStructure) => new JSClass(typeStructure);
            CreateComment = () => new JSComment();
            CreateProperty = () => new JSProperty();
            CreatePropertyFromMethod = (method) => new JSProperty(method);
            CreateAssignable = () => new JSPropertyAssignAble();
            CreateRunMethodRequest = () => new RunMethodRequest();
            CreateRunMethodRequestFromMethod = (method) => new RunMethodRequest(method);
            CreateExport = () => new Export();
            CreateImport = () => new Import();
        }
    }
}
