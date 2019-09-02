using CodeBuilder.IBuilder;
using CodeBuilder.JS.Builder.Properties;
using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder;
using CodeBuilder.JS.IBuilder.Comments;
using CodeBuilder.JS.IBuilder.Requests;
using CodeBuilder.JS.Properties;
using CodeBuilder.JS.Types;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Builder
{
    public class JSRequestClass : JSRenderble, IJSClass
    {
        private const string nameTag = "name";
        private const string constructorParameterTag = "constructorParameters";
        private const string baseUrlVariableName = "baseUrl";

        public JSRequestClass(ClassStructure classStructure) : base("JSRequestClass")
        {
            buildImports(classStructure);
            buildConstructorComment(classStructure);
            buildDirectAssignmentProperties();
            buildTagValues(classStructure);
            addRequestRunProperties(classStructure);
            addRequestRunMethods(classStructure);
            buildExport(classStructure);
        }

        private void addRequestRunProperties(ClassStructure classStructure)
        {
            multiplyTags(classStructure.Methods.Count, ((JSRenderble)DI.Get<IRunRequestProperty>()).Name);
            childRenderbles.AddRange(classStructure.Methods.Select(methodStructure => (JSRenderble)DI.Get<IRunRequestProperty>(methodStructure)));
        }

        private void addRequestRunMethods(ClassStructure classStructure)
        {
            multiplyTags(classStructure.Methods.Count, ((JSRenderble)DI.Get<IRunRequestMethod>()).Name);
            childRenderbles.AddRange(classStructure.Methods.Select(methodStructure => (JSRenderble)DI.Get<IRunRequestMethod>(methodStructure, classStructure.Name)));
        }

        private void buildExport(ClassStructure classStructure)
        {
            childRenderbles.Add((JSRenderble)DI.Get<IExport>(classStructure));
        }

        private void buildTagValues(ClassStructure classStructure)
        {
            classStructure.Name = Configuration.Instance.RequestContextNameFactory(classStructure.Name);
            base.tagValues = new Dictionary<string, string> {
                { nameTag, classStructure.Name },
                { constructorParameterTag, baseUrlVariableName }
            };
        }

        private void buildConstructorComment(ClassStructure classStructure)
        {
            childRenderbles.Add((JSRenderble)DI.Get<IClassConstructorComment>(classStructure));
        }

        private void buildDirectAssignmentProperties()
        {
            childRenderbles.Add((JSRenderble)DI.Get<IDirectAssignmentProperty>(new TypeStructure { IsSytemType = true, Name = baseUrlVariableName, Type = typeof(string) }, new JSString()));
        }

        private void buildImports(ClassStructure classStructure)
        {
            var imports = TypeExtractor.GetTypes(classStructure);
            multiplyTags(imports.Count, ((JSRenderble)DI.Get<IRequestContextImport>()).Name);
            childRenderbles.AddRange(imports.Select(importTypeStructure => (JSRenderble)DI.Get<IRequestContextImport>(importTypeStructure)));
        }
    }
}
