using CodeBulder.IBuilder;
using CodeBulder.JS.Types;
using CodeBuilder.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBulder.JS.Builder
{
    public partial class JSFunction
    {
        private void createFieldsAndMethods(ClassStructure classStructure)
        {
            var properties = (List<IJSProperty>)jsProperties;
            properties.Add(createBaseURLProperty());

            var isRPCMode = classStructure.URL.Contains("[action]");
            foreach (var method in classStructure.Methods)
            {
                handleMethod(properties, isRPCMode, method);
            }
        }

        private void handleMethod(List<IJSProperty> properties, bool isRPCMode, MethodStructure method)
        {
            method.IsRPC = isRPCMode;
            properties.Add(JSBuilderIOCContainer.Instance.CreatePropertyFromMethod(method));
            ((List<IRenderble>)Methods).Add((IRenderble)JSBuilderIOCContainer.Instance.CreateRunMethodRequestFromMethod(method));
        }

        private IJSProperty createBaseURLProperty()
        {
            var baseUrlProperty = JSBuilderIOCContainer.Instance.CreateProperty();
            baseUrlProperty.Name = "_baseUrl";
            baseUrlProperty.Assignable = JSBuilderIOCContainer.Instance.CreateAssignable();
            baseUrlProperty.Assignable.ObjectAssignment = "baseUrl";

            baseUrlProperty.Comment = JSBuilderIOCContainer.Instance.CreateComment();
            baseUrlProperty.Comment.Description = "The base URL used for all requests on the class.";
            baseUrlProperty.Comment.Type = new JSString();
            return baseUrlProperty;
        }

        private void assignLocalFields(ClassStructure classStructure)
        {
            Name = $"{classStructure.Name}RequestContainer";
            ConstructorParamters = new List<string>() { "baseUrl" };
            Export = JSBuilderIOCContainer.Instance.CreateExport();
            Export.Modules = new String[] { Name };
        }

        private void createConstructorComment(ClassStructure classStructure)
        {
            ConstructorComment = JSBuilderIOCContainer.Instance.CreateComment();
            ConstructorComment.Description = $"Creates Instance Of {classStructure.Name}Context.";
            ConstructorComment.Params = new Dictionary<string, JSType> { { "baseURL", new JSString() } };
        }

        private void createHttpHeaderFunctionComment(ClassStructure classStructure)
        {
            HttpHeaderFunctionComment = JSBuilderIOCContainer.Instance.CreateComment();
            HttpHeaderFunctionComment.Description = $"Sets the request headers for all requests.";
            HttpHeaderFunctionComment.Params = new Dictionary<string, JSType>() { { "headerObject", new JSObject() } };
        }
    }
}
