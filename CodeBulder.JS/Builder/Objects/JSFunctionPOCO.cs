using CodeBuilder.IBuilder;
using CodeBuilder.JS.Types;
using CodeBuilder.Structure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CodeBuilder.JS.Builder
{
    public partial class JSFunction
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
            var firstProperty = typeStructure.Properties.Any() ? typeStructure.Properties[0] : null;
            foreach (var property in typeStructure.Properties)
            {
                var jsProperty = JSBuilderIOCContainer.Instance.CreateProperty();
                jsProperty.Comment = JSBuilderIOCContainer.Instance.CreateComment();
                jsProperty.Name = property.Name;
                jsProperty.Comment.Type = JSTypeMapping.GetJSType(property);
                jsProperty.Assignable = JSBuilderIOCContainer.Instance.CreateAssignable();
                if (property.IsSytemType)
                {
                    jsProperty.Assignable.ObjectAssignment = mapSystemType(firstProperty, property);
                }
                else
                {
                    if (property.IsArray)
                    {
                        jsProperty.Assignable.ObjectAssignment = mapComplexArray(firstProperty, property);
                    }
                    else
                    {
                        jsProperty.Assignable.ObjectAssignment = mapComplexObject(firstProperty, property);
                    }
                }
                properties.Add(jsProperty);
            }
        }

        void createPOCOClassConstructorComment(ref IEnumerable<String> constructorParamters, IComment comment, TypeStructure typeStructure)
        {
            constructorParamters = typeStructure.Properties.Select(x => x.Name).ToList();
            comment = JSBuilderIOCContainer.Instance.CreateComment();
            comment.Description = $"Creates Instance Of The Result Class.";
            comment.Params = typeStructure.Properties.ToDictionary(a => a.Name, b => JSTypeMapping.GetJSType(b)); new Dictionary<string, JSType> { { "data", new JSObject() } };
        }

        private void createPOCOClassConstructorComment(TypeStructure typeStructure)
        {
            ConstructorParamters = typeStructure.Properties.Select(x => x.Name).ToList();
            ConstructorComment = JSBuilderIOCContainer.Instance.CreateComment();
            ConstructorComment.Description = $"Creates Instance Of The Result Class.";
            ConstructorComment.Params = typeStructure.Properties.ToDictionary(a => a.Name, b => JSTypeMapping.GetJSType(b)); new Dictionary<string, JSType> { { "data", new JSObject() } };
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
