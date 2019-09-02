using CodeBuilder.IBuilder;
using CodeBuilder.JS.Builder.Properties;
using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder.Comments;
using CodeBuilder.JS.IBuilder.Properties;
using CodeBuilder.JS.Properties;
using CodeBuilder.JS.Types;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Builder
{
    public class JSPOCOClass : JSRenderble, IJSClass
    {
        private const string nameTag = "name";
        private const string constructorParameterTag = "constructorParameters";

        public JSPOCOClass(TypeStructure typeStructure) : base("JSPOCOClass")
        {
            buildImports(typeStructure);
            buildConstructorComment(typeStructure);
            buildDirectAssignmentProperties(typeStructure);
            buildTagValues(typeStructure);
            buildFromObjectAssignmentProperties(typeStructure);
            buildExport(typeStructure);
        }

        private void buildExport(TypeStructure typeStructure)
        {
            childRenderbles.Add((JSRenderble)DI.Get<IExport>(typeStructure));
        }

        private void buildTagValues(TypeStructure typeStructure)
        {
            base.tagValues = new Dictionary<string, string> {
                { nameTag, Configuration.Instance.ModelsNameFactory(typeStructure.TypeName) },
                { constructorParameterTag, typeStructure.Properties.Select(x=>x.Name).Aggregate((a,b)=>$"{a},{b}") }
            };
        }

        private void buildConstructorComment(TypeStructure typeStructure)
        {
            childRenderbles.Add((JSRenderble)DI.Get<IClassConstructorComment>(typeStructure));
        }

        private void buildDirectAssignmentProperties(TypeStructure typeStructure)
        {
            multiplyTags(typeStructure.Properties.Count, getDirectAssignedPropertyTagName(typeStructure));
            childRenderbles.AddRange(typeStructure.Properties.Select(property => (JSRenderble)DI.Get<IDirectAssignmentProperty>(property, JSTypeMapping.GetJSType(property))));
        }

        private string getDirectAssignedPropertyTagName(TypeStructure typeStructure)
        {
            return ((JSRenderble)DI.Get<IDirectAssignmentProperty>(typeStructure, new JSNumber())).Name;
        }

        private void buildFromObjectAssignmentProperties(TypeStructure typeStructure)
        {
            multiplyTags(typeStructure.Properties.Count, getFromObjectPropertyTagName(typeStructure));
            childRenderbles.AddRange(typeStructure.Properties.Select(property => (JSRenderble)DI.Get<IFromObjectAssignmentProperty>(property, JSTypeMapping.GetJSType(property))));
        }

        private string getFromObjectPropertyTagName(TypeStructure typeStructure)
        {
            return ((JSRenderble)DI.Get<IFromObjectAssignmentProperty>(typeStructure, new JSNumber())).Name;
        }

        private void buildImports(TypeStructure typeStructure)
        {
            var imports = TypeExtractor.GetTypes(typeStructure);
            multiplyTags(imports.Count, ((JSRenderble)DI.Get<IImport>(typeStructure)).Name);
            childRenderbles.AddRange(imports.Select(importTypeStructure => (JSRenderble)DI.Get<IImport>(importTypeStructure)));
        }
    }
}
