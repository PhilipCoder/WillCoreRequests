using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb.Models
{
    public class Receipt
    {
        public Guid Id { get; set; }
        public byte ByteValue { get; set; }
        public sbyte SByteValue { get; set; }
        public char CharValue { get; set; }
        public Decimal DecimalValue { get; set; }
        public Double DoubleValue { get; set; }
        public float FloatValue { get; set; }
        public int IntValue { get; set; }
        public uint UintValue { get; set; }
        public Int64 LongValue { get; set; }
        public String StringValue { get; set; }
        public Product[] Products { get; set; }
    }
}
