using System;
using System.Collections.Generic;

namespace PartiesTests
{
    public class Student : Person
    {
        public const string SSN_KEY = "SSN";

        public const string InvalidFormat = "SSN must be either the last 4 digits or XXX-XX-XXXX or full 9 digits";

        public Student(Dictionary<string, object> recordFields)
        {
            validationMessages = new List<string>();

            parseRequiredFields(recordFields);
        }

        public IList<string> ValidationMessages
        {
            get
            {
                return validationMessages;
            }
        }
        public string SSN { get; protected set; }

        private void parseRequiredFields(Dictionary<string, object> recordFields)
        {
            if (!recordFields.ContainsKey(PersonNameParser.FirstName_Key))
            {
                this.ValidationMessages.Add(PersonNameParser.NameRequiredMessage);
            }
        }

        private List<string> validationMessages;
    }
}