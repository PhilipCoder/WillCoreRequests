using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.Structure
{
    /// <summary>
    /// Base class to define the structure of a method
    /// </summary>
    public class MethodStructure
    {
        /// <summary>
        /// Name of the method
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// URL to access method
        /// </summary>
        public String URL { get; set; }
        /// <summary>
        /// Attributes on the method
        /// </summary>
        public Dictionary<String, Attribute> Attributes { get; set; }
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
