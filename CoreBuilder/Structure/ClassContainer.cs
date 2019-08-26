using CodeBuilder.Structure;
using System;
using System.Collections.Generic;

namespace CodeBuilder.CoreBuilder
{
    /// <summary>
    /// Base class to define and contain the classes and methods the code walker will include.
    /// 
    /// Author: Philip Schoeman
    /// </summary>
    /// <typeparam name="T">Classes that inherit from this type will be included.</typeparam>
    public abstract class ClassContainter<T>
    {
        /// <summary>
        /// Only classes with attributes in this array will be returned. 
        /// Can be empty to return all classes.
        /// </summary>
        public virtual Type[] ClassIncludeFilterAttributes { get; }
        /// <summary>
        /// Only attributes in this array will be returned.
        /// </summary>
        public virtual Type[] AttributeIncludeFilterAttributes { get; }
        /// <summary>
        /// Only methods with attributes in this array will be returned
        /// Can be empty to return all.
        /// </summary>
        public virtual Type[] MethodIncludeFilterAttributes { get; }
        /// <summary>
        /// Only methods with attributes in this array will be returned
        /// Can be empty to return all.
        /// </summary>
        public virtual Type[] ParameterIncludeFilterAttributes { get; }
        /// <summary>
        /// Only properties with attributes in this array will be returned
        /// Can be empty to return all.
        /// </summary>
        public virtual Type[] PropertyIncludeFilterAttributes { get; }
        /// <summary>
        /// Only classes with attributes not in this array will be returned. 
        /// Can be empty to return all classes.
        /// </summary>
        public virtual Type[] ClassExcludeFilterAttributes { get; }
        /// <summary>
        /// Only attributes in this array will be returned.
        /// </summary>
        public virtual Type[] AttributeExcludeFilterAttributes { get; }
        /// <summary>
        /// Only methods with attributes not in this array will be returned
        /// Can be empty to return all.
        /// </summary>
        public virtual Type[] MethodExcludeFilterAttributes { get; }
        /// <summary>
        /// Only methods with attributes not in this array will be returned
        /// Can be empty to return all.
        /// </summary>
        public virtual Type[] ParameterExcludeFilterAttributes { get; }
        /// <summary>
        /// Only properties with attributes not in this array will be returned
        /// Can be empty to return all.
        /// </summary>
        public virtual Type[] PropertyExcludeFilterAttributes { get; }
        /// <summary>
        /// Classes found
        /// </summary>
        public List<ClassStructure> Classes { get; set; }

        public Dictionary<String, TypeStructure> Models { get; set; }

        public ClassContainter()
        {
            Classes = new List<ClassStructure>();
            Models = new Dictionary<string, TypeStructure>();
        }

        public virtual void BuildCode(string path)
        {
            throw new NotImplementedException();
        }

    }
}
