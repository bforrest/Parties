using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace PartiesTests
{
    public enum NamePart
    {
        [Description("First Name")]
        First = 1,

        [Description("Middle Name")]
        Middle = 2,

        [Description("Last Name")]
        Last = 3
    }
}
