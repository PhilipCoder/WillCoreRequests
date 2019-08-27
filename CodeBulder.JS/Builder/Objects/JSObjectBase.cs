using CodeBuilder.Structure;
using CodeBulder.IBuilder;
using CodeBulder.JS.Properties;
using CodeBulder.JS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBulder.JS.Builder
{
    public abstract class JSObjectBase : JSRenderble
    {
        protected JSObjectBase(string template) : base(template) { }

        protected string mapComplexObject(TypeStructure firstProperty, TypeStructure property)
        {
            return $"{firstProperty.Name} && {firstProperty.Name}.{property.Name} ? new {property.TypeName}({firstProperty.Name}.{property.Name}) : {property.Name} ? new {property.TypeName}({property.Name}) : null";
        }

        protected string mapComplexArray(TypeStructure firstProperty, TypeStructure property)
        {
            return $"{firstProperty.Name}._singleParameter && {firstProperty.Name}.{property.Name} ? {firstProperty.Name}.{property.Name}.map(dataRow => new {property.TypeName}(dataRow)) : {property.Name} ? {property.Name}.map(dataRow => new {property.TypeName}(dataRow)) : null";
        }

        protected string mapSystemType(TypeStructure firstProperty, TypeStructure property)
        {
            return $"typeof({firstProperty.Name}._singleParameter) !== \"undefined\" ? {firstProperty.Name}.{property.Name} : {property.Name}";
        }
       

        protected void generateImportForType(List<IImport> imports,TypeStructure property)
        {
            var import = JSBuilderIOCContainer.Instance.CreateImport();
            import.Modules = new string[] { property.TypeName };
            import.URL = $"./{property.TypeName}.js";
            imports.Add(import);
        }
    }
}
