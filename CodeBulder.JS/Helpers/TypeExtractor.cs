using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Helpers
{
    public static class TypeExtractor
    {
        public static List<TypeStructure> GetTypes(TypeStructure typeStructure)
        {
            return
                typeStructure.
                Properties.
                Where(x => !x.IsSytemType).
                ObjectDistinct(x => x.TypeName).
                ToList();
        }

        public static List<TypeStructure> GetTypes(ClassStructure classStructure)
        {
            var resultTypes =
                classStructure.
                Methods.
                Where(x => !x.Result.IsSytemType).
                SelectMany(x => getAllPOCOs(x.Result)).
                ToList();

            resultTypes.
                AddRange(
                classStructure.
                Methods.
                SelectMany(
                    method
                    =>
                    method.
                    Parameters.
                    Where(x => !x.IsSytemType).
                    SelectMany(x => getAllPOCOs(x))
                    )
                );

            return
                resultTypes.
                ObjectDistinct(x => x.TypeName).
                ToList();
        }

        private static List<TypeStructure> getAllPOCOs(TypeStructure typeStructure)
        {
            var result = new List<TypeStructure>() { typeStructure };
            if (typeStructure.Properties != null)
            {
                foreach (var property in typeStructure.Properties)
                {
                    if (!property.IsSytemType)
                    {
                        result.Add(property);
                        result.AddRange(getAllPOCOs(property));
                    }
                }
            }
            return result;
        }
    }
}
