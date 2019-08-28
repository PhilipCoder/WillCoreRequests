using CodeBuilder.IBuilder;
using CodeBuilder.JS;
using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.Properties;
using CodeBuilder.JS.Types;
using CodeBuilder.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Builder
{
    public partial class JSFunction : JSObjectBase, IJSClass
    {
        private const String headerCommentToken = "<< function[headerComment] >>";
        private const String httpHeaderFunctionCommentToken = "<< function[httpHeaderFunctionComment] >>";
        private const String constructorCommentToken = "<< function[constructorComment] >>";
        private const String classNameToken = "<< function[name] >>";
        private const String ConstructorParamterTag = "<< function[constructorParameters] >>";
        private const String PropertiesToken = "<< function[property] >>";
        private const string MethodToken = "<< function[method] >>";

        public IEnumerable<IImport> Imports { get; set; } = new List<IImport>();
        public IExport Export { get; set; }
        public IComment HeaderComment { get; set; }
        public IComment ClassComment { get; set; }
        public IComment ConstructorComment { get; set; }
        public IComment HttpHeaderFunctionComment { get; set; }
        public IEnumerable<String> ConstructorParamters { get; set; } = new List<string>();
        public IEnumerable<IJSProperty> jsProperties { get; set; } = new List<IJSProperty>();
        public IEnumerable<IRenderble> Methods { get; set; } = new List<IRenderble>();
        public String Name { get; set; }
        public String Extends { get; set; }

        public JSFunction() : base(Resources._class) {}

        public JSFunction(ClassStructure classStructure) : base(Resources.function)
        {
            createHttpHeaderFunctionComment(classStructure);
            createConstructorComment(classStructure);
            assignLocalFields(classStructure);
            createFieldsAndMethods(classStructure);
        }

        public JSFunction(TypeStructure typeStructure) : base( Resources.functionPOKO)
        {
            assignLocalProperties(typeStructure);
            createPOCOClassConstructorComment(typeStructure);
            createPOCOClassProperties(typeStructure);
        }

        public override String GetText()
        {
            //comments
            if (HeaderComment != null) Template = Template.Replace(headerCommentToken, HeaderComment.GetText());
            if (HttpHeaderFunctionComment != null) Template = Template.Replace(httpHeaderFunctionCommentToken, HttpHeaderFunctionComment.GetText());
            if (ConstructorComment != null) Template = Template.Replace(constructorCommentToken, ConstructorComment.GetText());
            if (ConstructorParamters.Any()) Template = Template.Replace(ConstructorParamterTag, ConstructorParamters.Aggregate((a, b) => a + "," + b));
            //class name
            Template = Template.Replace(classNameToken, Name);
            //Properties
            if (jsProperties.Any()) Template = Template.Replace(PropertiesToken, jsProperties.Select(x => x.GetText()).Aggregate((a, b) => a + "\r\n" + b));
            //Methods
            if (Methods.Any()) Template = Template.Replace(MethodToken, Methods.Select(x => x.GetText()).Aggregate((a, b) => a + "\r\n" + b));
            //remove unusedTokens
            CleanTemplateUp();
            return Template;
        }

    }
}
