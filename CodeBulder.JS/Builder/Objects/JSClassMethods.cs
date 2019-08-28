using CodeBuilder.IBuilder;
using CodeBuilder.JS.Types;
using CodeBuilder.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Builder
{
    public partial class JSClass
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
            buildResultTypeImports(method);
            buildParameterTypeImports(method);
        }

        private void buildParameterTypeImports(MethodStructure method)
        {
            foreach (var parameter in method.Parameters)
            {
                if (!parameter.IsSytemType && !Imports.Any(x => x.Modules.Any(m => m == parameter.TypeName)))
                {
                    var import = JSBuilderIOCContainer.Instance.CreateImport();
                    ((List<String>)import.Modules).Add(parameter.TypeName);
                    import.URL = $"./{Configuration.Instance.ModelsFolder}/{parameter.TypeName}.js";
                    ((List<IImport>)Imports).Add(import);
                    var export = JSBuilderIOCContainer.Instance.CreateExport();
                    ((List<string>)Export.Modules).Add(parameter.TypeName);
                }
            }
        }

        private void buildResultTypeImports(MethodStructure method)
        {
            if (!method.Result.IsSytemType && !Imports.Any(x => x.Modules.Any(m => m == method.Result.TypeName)) && method.Result.TypeName != null)
            {
                var import = JSBuilderIOCContainer.Instance.CreateImport();
                ((List<String>)import.Modules).Add(method.Result.TypeName);
                import.URL = $"./{Configuration.Instance.ModelsFolder}/{method.Result.TypeName}.js";
                ((List<IImport>)Imports).Add(import);
                ((List<string>)Export.Modules).Add(method.Result.TypeName);
            }
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
            Export.Modules = new List<string> { Name };
        }

        private void createConstructorComment(ClassStructure classStructure)
        {
            ConstructorComment = JSBuilderIOCContainer.Instance.CreateComment();
            ConstructorComment.Description = $"Creates Instance Of {classStructure.Name}Context.";
            ConstructorComment.Params = new Dictionary<string, JSType> { { "baseURL", new JSString() } };
        }

        private void createClassComment(ClassStructure classStructure)
        {
            ClassComment = JSBuilderIOCContainer.Instance.CreateComment();
            ClassComment.Description = $"{classStructure.Name}Context. Used to make requests to the {classStructure.Name} controller.";
        }

        private void createHttpHeaderFunctionComment(ClassStructure classStructure)
        {
            HttpHeaderFunctionComment = JSBuilderIOCContainer.Instance.CreateComment();
            HttpHeaderFunctionComment.Description = $"Sets the request headers for all requests.";
            HttpHeaderFunctionComment.Params = new Dictionary<string, JSType>() { { "headerObject", new JSObject() } };
        }

        private void createRequestImport()
        {
            var import = JSBuilderIOCContainer.Instance.CreateImport();
            ((List<String>)import.Modules).Add("request");
            ((List<String>)import.Modules).Add("globalTokens");
            import.URL = "./request/request.js";
            ((List<IImport>)Imports).Add(import);
        }
    }
}
