using System;
using System.Collections.Generic;
using System.Text;

namespace ContractExtractor.Models
{
    public class ItemType
    {
        public bool IsSystem { get; set; }
        public bool IsArray { get; set; }
        public Type Type { get; set; }
        public override string ToString()
        {
            var displayType = IsArray ? $"{Type.Name}[]" : Type.Name;
            return $"Is System Type: {IsSystem}, Is Array: {IsArray}, Type: {Type}, Display Type: {displayType}";
        }
    }
}
