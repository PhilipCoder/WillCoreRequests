using CodeBulder.IBuilder;
using CodeBulder.JS.Types;
using CodeBuilder.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBulder.JS.Builder
{
    public partial class JSClass
    {
        private void assignLocalProperties(TypeStructure typeStructure)
        {
            ClassComment = JSBuilderIOCContainer.Instance.CreateComment();
            ClassComment.Description = $"Result class {typeStructure.TypeName}.";
            Name = typeStructure.TypeName;
            Export = JSBuilderIOCContainer.Instance.CreateExport();
            Export.Modules = new String[] { typeStructure.TypeName };
        }

        private void createPOCOClassProperties(TypeStructure typeStructure)
        {
            var properties = (List<IJSProperty>)jsProperties;
            foreach (var property in typeStructure.Properties)
            {
                var jsProperty = JSBuilderIOCContainer.Instance.CreateProperty();
                jsProperty.Comment = JSBuilderIOCContainer.Instance.CreateComment();
                jsProperty.Name = property.Name;
                jsProperty.Comment.Type = JSTypeMapping.GetJSType(property);
                jsProperty.Assignable = JSBuilderIOCContainer.Instance.CreateAssignable();
                if (property.IsSytemType)
                {
                    jsProperty.Assignable.ObjectAssignment = $"typeof(data.{property.Name}) !== \"undefined\" ? data.{property.Name} : null";
                }
                else
                {
                    if (property.IsArray)
                    {
                        jsProperty.Assignable.ObjectAssignment = $"typeof(data.{property.Name}) !== \"undefined\" ? data.{property.Name}.map(dataRow => new {property.Name}(dataRow)) : null";
                    }
                    else
                    {
                        jsProperty.Assignable.ObjectAssignment = $"typeof(data.{property.Name}) !== \"undefined\" ? new {property.Name}(dataRow) : null";
                    }
                }
                properties.Add(jsProperty);
                if (!property.IsSytemType)
                {
                    var import = JSBuilderIOCContainer.Instance.CreateImport();
                    import.Modules = new string[] { property.TypeName };
                    import.URL = $"./{property.TypeName}.js";
                    ((List<IImport>)Imports).Add(import);
                }
            }
        }

        private void createPOCOClassConstructorComment()
        {
            ConstructorParamters = new List<string>() { "data" };
            ConstructorComment = JSBuilderIOCContainer.Instance.CreateComment();
            ConstructorComment.Description = $"Creates Instance Of The Result Class.";
            ConstructorComment.Params = new Dictionary<string, JSType> { { "data", new JSObject() } };
        }
    }
}
