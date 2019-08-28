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
    public partial class JSClass : JSObjectBase , IJSClass
    {
        private const String importToken = "<< class[import] >>";
        private const String headerCommentToken = "<< class[headerComment] >>";
        private const String classCommentToken = "<< class[comment] >>";
        private const String httpHeaderFunctionCommentToken = "<< class[httpHeaderFunctionComment] >>";
        private const String constructorCommentToken = "<< class[constructorComment] >>";
        private const String classNameToken = "<< class[name] >>";
        private const String extendsToken = "<< class[extends] >>";
        private const String ConstructorParamterTag = "<< class[constructorParameters] >>";
        private const String PropertiesToken = "<< class[property] >>";
        private const string MethodToken = "<< class[method] >>";
        private const string ExportToken = "<< class[exports] >>";

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

        public JSClass() : base(Resources._class) {}

        public JSClass(ClassStructure classStructure) : base(Resources._class)
        {
            createRequestImport();
            createClassComment(classStructure);
            createHttpHeaderFunctionComment(classStructure);
            createConstructorComment(classStructure);
            assignLocalFields(classStructure);
            createFieldsAndMethods(classStructure);
        }

        public JSClass(TypeStructure typeStructure) : base( Resources.classPOKO)
        {
            assignLocalProperties(typeStructure);
            createPOCOClassConstructorComment(typeStructure);
            createPOCOClassProperties(typeStructure);
        }

        public override String GetText()
        {
            //imports
            if (Imports.Any()) Template = Template.Replace(importToken, Imports.Select(x => x.GetText()).Aggregate((a, b) => a + "\r\n" + b));
            //comments
            if (HeaderComment != null) Template = Template.Replace(headerCommentToken, HeaderComment.GetText());
            if (ClassComment != null) Template = Template.Replace(classCommentToken, ClassComment.GetText());
            if (HttpHeaderFunctionComment != null) Template = Template.Replace(httpHeaderFunctionCommentToken, HttpHeaderFunctionComment.GetText());
            if (ConstructorComment != null) Template = Template.Replace(constructorCommentToken, ConstructorComment.GetText());
            if (Extends != null) Template = Template.Replace(extendsToken, $" extends {Extends}");
            if (ConstructorParamters.Any()) Template = Template.Replace(ConstructorParamterTag, ConstructorParamters.Aggregate((a, b) => a + "," + b));
            //class name
            Template = Template.Replace(classNameToken, Name);
            //Properties
            if (jsProperties.Any()) Template = Template.Replace(PropertiesToken, jsProperties.Select(x => x.GetText()).Aggregate((a, b) => a + "\r\n" + b));
            //Methods
            if (Methods.Any()) Template = Template.Replace(MethodToken, Methods.Select(x => x.GetText()).Aggregate((a, b) => a + "\r\n" + b));
            //Exports
            if (this.Export != null) Template = Template.Replace(ExportToken, this.Export.GetText());
            //remove unusedTokens
            CleanTemplateUp();
            return Template;
        }

    }
}
