using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PartiesTests
{
    public class SSNMapper
   {
        public const string SSN_Pattern = @"^(\d{4})$|^(\d{3}-\d{2}-\d{4})$|(^\d{9})$";
        public const string InvalidFormat = "SSN must be either the last 4 digits or XXX-XX-XXXX or full 9 digits";

        public string SSN { get; protected set; }
        public IList<string> ValidationMessages { get; protected set; }

        public SSNMapper(Dictionary<string, object> recordFields)
        {
            ValidationMessages = new List<string>();
            if (recordFields.ContainsKey(Student.SSN_KEY))
                parseSSN(recordFields[Student.SSN_KEY].ToString());
        }

        public bool IsValid { get; protected set; }

        private void parseSSN(string ssn)
        {
            if (string.IsNullOrEmpty(ssn.Trim()))
            {
                return;
            }

            if (!Regex.IsMatch(ssn, SSN_Pattern))
            {
                ValidationMessages.Add(InvalidFormat);
                return;
            }

            int start = ssn.Length - 4;
            SSN = ssn.Substring(start);
        }
    }
}