using System;
using System.Collections.Generic;
using System.Linq;

namespace PartiesTests
{
    public class Person : IPersonName
    {
        public string First { get; set; }

        public string Last { get; set; }

        public string Middle { get; set; }
    }
}