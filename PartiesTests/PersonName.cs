using System;
using System.Collections.Generic;
using System.Linq;

namespace PartiesTests
{
    public class PersonName
    {
        public PersonName(string first, string last, string middle, IList<string> validationMessages)
        {
            ValidationMessages = validationMessages;
            Middle = middle;
            Last = last;
            First = first;
        }
        public string First { get; private set; }
        public string Last { get; private set; }
        public string Middle { get; private set; }
        public IList<string> ValidationMessages { get; private set; }
    }
}