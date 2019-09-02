using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ICodeBuilder
{
    /// <summary>
    /// Base class to define the structure of a class
    /// </summary>
    public class ClassStructure
    {
        /// <summary>
        /// The name of the class
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Contains the filtered attributes for the specified class
        /// </summary>
        public Dictionary<string, Attribute> Attributes { get; set; }
        /// <summary>
        /// URL to access controller
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// Methods found
        /// </summary>
        public List<MethodStructure> Methods { get; set; }
        public ClassStructure()
        {
            Methods = new List<MethodStructure>();
        }
    }
}
