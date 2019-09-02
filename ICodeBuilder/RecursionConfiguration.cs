using System;
using System.Collections.Generic;
using System.Text;

namespace ICodeBuilder
{
    public class RecursionConfiguration
    {
        /// <summary>
        /// The type of controller to extract
        /// </summary>
        public Type ControllerType { get; set; }
        /// <summary>
        /// Sets the maximum level of child result classes the recursion will allow
        /// </summary>
        public int MaxRecursiveDepth { get; set; } = 10;
        /// <summary>
        /// Only classes with attributes in this array will be returned. 
        /// Can be empty to return all classes.
        /// </summary>
        public virtual List<Type> ClassIncludeFilterAttributes { get; set; }
        /// <summary>
        /// Only attributes in this array will be returned.
        /// </summary>
        public virtual List<Type> AttributeIncludeFilterAttributes { get; set; }
        /// <summary>
        /// Only methods with attributes in this array will be returned
        /// Can be empty to return all.
        /// </summary>
        public virtual List<Type> MethodIncludeFilterAttributes { get; set; }
        /// <summary>
        /// Only methods with attributes in this array will be returned
        /// Can be empty to return all.
        /// </summary>
        public virtual List<Type> ParameterIncludeFilterAttributes { get; set; }
        /// <summary>
        /// Only properties with attributes in this array will be returned
        /// Can be empty to return all.
        /// </summary>
        public virtual List<Type> PropertyIncludeFilterAttributes { get; set; }
        /// <summary>
        /// Only classes with attributes not in this array will be returned. 
        /// </summary>
        public virtual List<Type> ClassExcludeFilterAttributes { get; set; }
        /// <summary>
        /// Only attributes in this array will be returned.
        /// </summary>
        public virtual List<Type> AttributeExcludeFilterAttributes { get; set; }
        /// <summary>
        /// Only methods with attributes not in this array will be returned
        /// </summary>
        public virtual List<Type> MethodExcludeFilterAttributes { get; set; }
        /// <summary>
        /// Only methods with attributes not in this array will be returned
        /// Can be empty to return all.
        /// </summary>
        public virtual List<Type> ParameterExcludeFilterAttributes { get; set; }
        /// <summary>
        /// Only properties with attributes not in this array will be returned
        /// Can be empty to return all.
        /// </summary>
        public virtual List<Type> PropertyExcludeFilterAttributes { get; set; }
    }
}
