using System;
using System.Collections.Generic;
using System.Text;

namespace ICodeBuilder
{
    /// <summary>
    /// Base class to define the structure of a method
    /// </summary>
    public class MethodStructure
    {
        /// <summary>
        /// Name of the method
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// URL to access method
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// Attributes on the method
        /// </summary>
        public Dictionary<string, Attribute> Attributes { get; set; }
        /// <summary>
        /// The result of the method
        /// </summary>
        public TypeStructure Result { get; set; }
        /// <summary>
        /// Paramters of the method
        /// </summary>
        public List<TypeStructure> Parameters { get; set; }
        public bool IsRPC { get; set; }

        public MethodStructure()
        {
            Parameters = new List<TypeStructure>();
        }
    }
}
