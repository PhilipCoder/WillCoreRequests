using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder.Comments;
using CodeBuilder.JS.Properties;
using CodeBuilder.JS.Types;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Builder.Properties
{
    public class NewDirectAssignment : JSRenderble, IDirectAssignmentProperty
    {
        private const string propertyNameTag = "propertyName";
        private const string typeTag = "type";
        private const string parameterTag = "parameters";

        public NewDirectAssignment(TypeStructure typeStructure, Types.JSClass classType)
            : base("DirectAssignmentProperty")
        {
            childRenderbles.Add((JSRenderble)DI.Get<IPropertyComment>(typeStructure));
            base.tagValues = new Dictionary<string, string> {
                { propertyNameTag, typeStructure.Name },
                { typeTag, Configuration.Instance.ModelsNameFactory(typeStructure.TypeName) },
                { parameterTag,typeStructure.Properties.Any() ? typeStructure.Properties.Select(x=>$"{typeStructure.Name}.{x.Name}").Aggregate((a,b)=>$"{a},{b}"): ""  }
            };
        }
    }
}
