using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder.Comments;
using CodeBuilder.JS.Properties;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Builder.Properties
{
    public class DirectAssignment : JSRenderble, IDirectAssignmentProperty
    {
        private const string propertyNameTag = "propertyName";

        public DirectAssignment(TypeStructure typeStructure, Types.JSDate type) : this(typeStructure) { }
        public DirectAssignment(TypeStructure typeStructure, Types.JSDateArray type) : this(typeStructure) { }
        public DirectAssignment(TypeStructure typeStructure, Types.JSNumber type) : this(typeStructure) { }
        public DirectAssignment(TypeStructure typeStructure, Types.JSNumberArray type) : this(typeStructure) { }
        public DirectAssignment(TypeStructure typeStructure, Types.JSObject type) : this(typeStructure) { }
        public DirectAssignment(TypeStructure typeStructure, Types.JSObjectArray type) : this(typeStructure) { }
        public DirectAssignment(TypeStructure typeStructure, Types.JSString type) : this(typeStructure) { }
        public DirectAssignment(TypeStructure typeStructure, Types.JSStringArray type) : this(typeStructure) { }
        public DirectAssignment(TypeStructure typeStructure, Types.JSBool type) : this(typeStructure) { }
        public DirectAssignment(TypeStructure typeStructure, Types.JSBoolArray type) : this(typeStructure) { }

        private DirectAssignment(TypeStructure typeStructure) : base("DirectAssignmentProperty")
        {
            base.tagValues = new Dictionary<string, string> {
                { propertyNameTag, typeStructure.Name },
            };
            childRenderbles.Add((JSRenderble)DI.Get<IPropertyComment>(typeStructure));
        }
    }
}
