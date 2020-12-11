using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork1
{
    class Operands
    {
        //for binary operations
        public double? A { get; set; } = null;
        public double? B { get; set; } = null;

        //for unary and binary bitwise operations
        public int? X { get; set; } = null;
        public int? Y { get; set; } = null;

        //for factorial operation
        public uint? F { get; set; } = null;

        public bool error { get; set; } = false;
    }
}
