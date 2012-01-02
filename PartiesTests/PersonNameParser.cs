using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnconstrainedMelody;

namespace PartiesTests
{
    public class PersonNameParser
    {
        public PersonNameParser(Dictionary<string, object> recordFields)
        {
            if (recordFields == null)
            {
                throw new ArgumentException("PersonNameParser requires a dictionary<string, object> for construction");
            }
            messages = new HashSet<string>();
            personFields = recordFields;
        }

        public IList<string> ValidationMessages
        { 
            get
            {
                return messages.ToList<string>();
            }
        }

        public void AddMessage( string message)
        {
            messages.Add(message);
        }

        public string FirstName()
        {
            if (string.IsNullOrEmpty(first))
            {
                first = ParseName(NamePart.First);
            }
            return first;
        }

        public string LastName()
        {
            if (string.IsNullOrEmpty(last))
            {
                last = ParseName(NamePart.Last);
            }
            return last;
        }

        public string MiddleName()
        {
            if (string.IsNullOrEmpty(middle))
            {
                middle = ParseName(NamePart.Middle);
            }
            return middle;
        }

        /// <summary>
        /// {0} is required
        /// </summary>
        public const string NameRequiredMessage = "{0} is required";

        /// <summary>
        /// ^[a-zA-Z0-9\""'\-\s]{2,35}$
        /// </summary>
        public const string NameRegex = @"^[a-zA-Z0-9\""'\-\s]{2,35}$";

        /// <summary>
        /// {0} Name must be between 2 and 35 characters in length.
        /// </summary>
        public const string NameLengthMessage = "{0} Name must be between 2 and 35 characters in length.";

        /// <summary>
        /// {0} may only contain letters, numbers, apostrophes, dashes or quotes.
        /// </summary>
        public const string NameCharacters = "{0} may only contain letters, numbers, apostrophes, dashes or quotes.";

        public const string FirstName_Key = "FirstName";

        public const string MiddleName_Key = "MiddleName";

        public const string LastName_Key = "LastName";

        public PersonName Parse()
        {
            first = ParseName(NamePart.First);
            last = ParseName(NamePart.Last);
            middle = ParseName(NamePart.Middle);

            return new PersonName(first, last, middle, ValidationMessages);
        }

        public bool IsCreatable()
        {
            return !string.IsNullOrEmpty(FirstName()) && !string.IsNullOrEmpty(LastName());
        }

        public string ParseName(NamePart whichName)
        {
            string description =  ((NamePart) whichName).GetDescription();
            string key = ParseNameExtracted(whichName);
            bool isRequired = whichName != NamePart.Middle;
            string parsedValue = null;

            if (!personFields.ContainsKey(key))
            {
                if (isRequired)
                {
                   AddMessage(string.Format(PersonNameParser.NameRequiredMessage, description));
                }
                return parsedValue;
            }

            parsedValue = personFields[key].ToString();

            if (string.IsNullOrEmpty(parsedValue))
            {
                if (isRequired)
                {
                    AddMessage(string.Format(PersonNameParser.NameRequiredMessage, description));
                }
                return parsedValue;
            }

            if (parsedValue.Length < 2 || parsedValue.Length > 35)
            {
                AddMessage(string.Format(PersonNameParser.NameLengthMessage, description));
                return parsedValue;
            }

            if(!Regex.IsMatch(parsedValue, NameRegex))
            {
                AddMessage(string.Format(PersonNameParser.NameCharacters, description));
                return parsedValue;
            }

            return parsedValue;
        }

        private static string ParseNameExtracted(NamePart whichName)
        {
            string key = string.Empty;
            switch (whichName)
            {
                case NamePart.First:
                    key = PersonNameParser.FirstName_Key;
                    break;
                case NamePart.Middle:
                    key = PersonNameParser.MiddleName_Key;
                    break;
                case NamePart.Last:
                    key = PersonNameParser.LastName_Key;
                    break;
            }
            return key;
        }

        private readonly Dictionary<string, object> personFields;

        private readonly HashSet<String> messages;

        private string first;
        private string last;
        private string middle;
    }
}