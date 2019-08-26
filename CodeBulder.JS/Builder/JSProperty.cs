using CodeBulder.IBuilder;
using CodeBulder.JS;
using CodeBulder.JS.Helpers;
using CodeBulder.JS.Properties;
using CodeBuilder.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeBulder.JS.Builder
{
    public class JSProperty : JSRenderble, IJSProperty
    {
        private const string PropertyCommentTag = "<< property[comment] >>";
        private const string PropertyNameTag = "<< property[name] >>";
        private const string PropertyAssignTag = "<< property[assignable] >>";

        public String Name { get; set; }
        public IAssignAble Assignable { get; set; }
        public IComment Comment { get; set; }

        public JSProperty() : base(Resources.property)
        {
            Assignable = JSBuilderIOCContainer.Instance.CreateAssignable();
        }

        public JSProperty(MethodStructure methodStructure) : base(Resources.property)
        {
            createPropertyComment(methodStructure);
            setPropertyName(methodStructure);
            createPropertyAssignable(methodStructure);
        }

        private void createPropertyAssignable(MethodStructure methodStructure)
        {
            Assignable = JSBuilderIOCContainer.Instance.CreateAssignable();
            Assignable.NewInstanceParamters = new List<string> {
                "this._baseUrl",
                $"\"{methodStructure.URL}\"",
                $"\"{HttpHelpers.GetHTTPMethod(methodStructure)}\"",
                getRequestParametersSourceObject(methodStructure),
                !methodStructure.Result.IsSytemType && methodStructure.Result.TypeName != null ? methodStructure.Result.TypeName :  "null"
            };
            Assignable.NewInstanceType = "request";
        }

        private void setPropertyName(MethodStructure methodStructure)
        {
            Name = $"_{(methodStructure.IsRPC ? methodStructure.Name : NamingHelpers.GetRestfullMethodName(methodStructure))}";
        }

        private void createPropertyComment(MethodStructure methodStructure)
        {
            Comment = JSBuilderIOCContainer.Instance.CreateComment();
            Comment.Description = $"Reqeust {methodStructure.URL}";
        }

        private string getRequestParametersSourceObject(MethodStructure methodStructure)
        {
            return methodStructure.Parameters.Any()
                ?  "{"+methodStructure.Parameters.Select(x => getSourceParameters(methodStructure, x)).Aggregate((a, b) => a + "," + b)+"}" 
                : "{}";
        }

        private string getSourceParameters(MethodStructure methodStructure, TypeStructure x)
        {
            return $"{x.Name}:{(x.Attributes.ContainsKey("FromBodyAttribute") ? "\"BODY\"" : methodStructure.URL.Contains($"{{{x.Name}}}") ? "\"URL\"" : "\"QUERY\"")}";
        }

        public override String GetText()
        {
            if (Name == null)
            {
                throw new InvalidDataException("Can't initialize a property without a name");
            }
            if (Comment != null)
            {
                Template = Template.Replace(PropertyCommentTag, Comment.GetText());
            }
            var assignText = Assignable == null ? "null" : Assignable.GetText();
            Template = Template.Replace(PropertyNameTag, Name);
            Template = Template.Replace(PropertyAssignTag, assignText);
            return Template;
        }
    }
}
