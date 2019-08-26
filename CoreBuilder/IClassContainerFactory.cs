using CodeBuilder.CoreBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder
{
    public interface IClassContainerFactory
    {
        ClassContainter<T> GetClassContainerInstace<T>();
    }
}
