using System;
using System.Collections.Generic;

namespace ICodeBuilder
{
    /// <summary>
    /// Base class to define and contain the classes and methods the code walker will include.
    /// 
    /// Author: Philip Schoeman
    /// </summary>
    /// <typeparam name="T">Classes that inherit from this type will be included.</typeparam>
    public abstract class ClassContainter
    {

        public List<ClassStructure> Classes { get; set; }
        public Dictionary<string, TypeStructure> Models { get; set; }
        public virtual IConfiguration Config { get; set; }
        public RecursionConfiguration recursionConfiguration { get; set; } = new RecursionConfiguration();
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
