using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder.Comments;
using CodeBuilder.JS.IBuilder.Properties;
using CodeBuilder.JS.Properties;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Builder.Properties
{
    public class NewAssignmentFromObj : JSRenderble, IFromObjectAssignmentProperty
    {
        private const string propertyNameTag = "propertyName";
        private const string typeTag = "type";

        public NewAssignmentFromObj(TypeStructure typeStructure, Types.JSClass classType)
           : base("FromObjectAssignmentProperty")
        {
            childRenderbles.Add((JSRenderble)DI.Get<IPropertyComment>(typeStructure));
            base.tagValues = new Dictionary<string, string> {
                { propertyNameTag, typeStructure.Name },
                { typeTag, Configuration.Instance.ModelsNameFactory(typeStructure.TypeName) }
            };
        }
    }
}
