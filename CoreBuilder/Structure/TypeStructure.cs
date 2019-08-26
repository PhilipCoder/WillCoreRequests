using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.Structure
{
    /// <summary>
    /// Base class to define the structure a user defined or system type.
    /// </summary>
    public class TypeStructure
    {
        /// <summary>
        /// The name of the property or parameter
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Basic TypeName
        /// </summary>
        public String TypeName { get; set; }
        /// <summary>
        /// The type of the object
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// Indicates if the type is form the system namespace
        /// </summary>
        public bool IsSytemType { get; set; }
        /// <summary>
        /// Indicates if type is an array or IEnumarble
        /// </summary>
        public bool IsArray { get; set; }
        /// <summary>
        /// Attributes on the type
        /// </summary>
        public Dictionary<String, Attribute> Attributes { get; set; }
        /// <summary>
        /// List of public, instance properties on the type
        /// </summary>
        public List<TypeStructure> Properties { get; set; }
        public TypeStructure()
        {
            Properties = new List<TypeStructure>();
        }
        public TypeStructure(TypeStructure values)
        {
            Name = values.Name;
            Type = values.Type;
            IsSytemType = values.IsSytemType;
            TypeName = values.TypeName;
            IsArray = values.IsArray;
            Attributes = values.Attributes;
        }
    }
}
