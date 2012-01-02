using System;
using System.Collections.Generic;
using System.Linq;
using NSpec;
using UnconstrainedMelody;

namespace PartiesTests
{
    public class describe_name_parser :nspec
    {
        Dictionary<string, object> recordFields;
        PersonNameParser mapper;

        void before_each()
        {
            recordFields = new Dictionary<string, object>();
        }

        void parsing_firstName()
        {
            var requiredNotProvided = string.Format(PersonNameParser.NameRequiredMessage, ((NamePart)NamePart.First).GetDescription());

            var invalidCharacters = string.Format(PersonNameParser.NameCharacters, ((NamePart)NamePart.First).GetDescription());

            var invalidLength = string.Format(PersonNameParser.NameLengthMessage, ((NamePart)NamePart.First).GetDescription());
            
            context["when parsing two characters"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.FirstName_Key, "ab");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have no validation errors"] = () =>
                {
                    foreach (var msg in mapper.ValidationMessages)
                    {
                        Console.WriteLine(msg);
                    }
                    mapper.ParseName(NamePart.First);
                    mapper.ValidationMessages.Count.should_be(0);
                };
            };


            context["when a the name key is not supplied"] = () =>  
            {
                before = () =>
                {
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have a First Name is required message"] = () =>
                {
                    mapper.ParseName(NamePart.First);
                    mapper.ValidationMessages.should_contain(requiredNotProvided);
                };
            };

            context["when parsing an empty string"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.FirstName_Key, string.Empty);
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have an invalid import message"] = () =>
                {
                    mapper.ParseName(NamePart.First);
                    mapper.ValidationMessages.should_contain(requiredNotProvided);
                };
            };

            context["when parsing a name that is too short"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.FirstName_Key, "A");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have invalid length message"] = () =>
                {
                    mapper.ParseName(NamePart.First);
                    mapper.ValidationMessages.should_contain(invalidLength);
                };
            };

            context["when parsing a name that is too long"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.FirstName_Key, "abcdefghijklmnopqrstuvwxyz0123456789");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have invalid length message"] = () =>
                {
                    mapper.ParseName(NamePart.First);
                    mapper.ValidationMessages.should_contain(invalidLength);
                };
            };

            context["when parsing disallowed characters"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.FirstName_Key, "ab!@");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have invalid character message"] = () =>
                {
                    mapper.ParseName(NamePart.First);
                    mapper.ValidationMessages.should_contain(invalidCharacters);
                };
            };

            // letters, numbers, apostrophes, dashes or quotes
            context["allows letters, numbers, apostrophes, dashes and quotes"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.FirstName_Key, @"a1'-""");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have no errors"] = () =>
                {
                    mapper.ParseName(NamePart.First);
                    mapper.ValidationMessages.Count.should_be(0);
                };
            };
        }

        void parsing_middleName()
        {
            var invalidCharacters = string.Format(PersonNameParser.NameCharacters, ((NamePart)NamePart.Middle).GetDescription());

            var invalidLength = string.Format(PersonNameParser.NameLengthMessage, ((NamePart)NamePart.Middle).GetDescription());

            context["when a middle name key is not provided"] = () =>
            {
                before = () =>
                {
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have no errors"] = () =>
                {
                    mapper.ParseName(NamePart.Middle);
                    mapper.ValidationMessages.Count.should_be(0);
                };
            };


            context["when parsing an empty string"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.MiddleName_Key, string.Empty);
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have an invalid import message"] = () =>
                {
                    mapper.ParseName(NamePart.Middle);
                    mapper.ValidationMessages.Count.should_be(0);
                };
            };

            context["when parsing a name that is too short"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.MiddleName_Key, "Z");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have invalid length message"] = () =>
                {
                    mapper.ParseName(NamePart.Middle);
                    mapper.ValidationMessages.should_contain(invalidLength);
                };
            };

            context["when parsing disallowed characters"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.MiddleName_Key, "ab!@");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have invalid character message"] = () =>
                {
                    mapper.ParseName(NamePart.Middle);
                    mapper.ValidationMessages.should_contain(invalidCharacters);
                };
            };

            // letters, numbers, apostrophes, dashes or quotes
            context["allows letters, numbers, apostrophes, dashes and quotes"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.MiddleName_Key, @"a1'-""");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have no errors"] = () =>
                {
                    mapper.ParseName(NamePart.Middle);
                    mapper.ValidationMessages.Count.should_be(0);
                };
            };
        }

        void parsing_lastName()
        {
            var requiredNotProvided = string.Format(PersonNameParser.NameRequiredMessage, ((NamePart)NamePart.Last).GetDescription());

            var invalidCharacters = string.Format(PersonNameParser.NameCharacters, ((NamePart)NamePart.Last).GetDescription());

            var invalidLength = string.Format(PersonNameParser.NameLengthMessage, ((NamePart)NamePart.Last).GetDescription());

            context["when a the name key is not supplied"] = () =>
            {
                before = () =>
                {
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have a Last Name is required message"] = () =>
                {
                    mapper.ParseName(NamePart.Last);
                    mapper.ValidationMessages.should_contain(requiredNotProvided);
                };
            };

            context["when parsing an empty string"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.LastName_Key, string.Empty);
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have an invalid import message"] = () =>
                {
                    mapper.ParseName(NamePart.Last);
                    mapper.ValidationMessages.should_contain(requiredNotProvided);
                };
            };

            context["when parsing a name that is too short"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.LastName_Key, "A");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have invalid length message"] = () =>
                {
                    mapper.ParseName(NamePart.Last);
                    mapper.ValidationMessages.should_contain(invalidLength);
                };
            };

            context["when parsing a name that is too long"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.LastName_Key, "abcdefghijklmnopqrstuvwxyz0123456789");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have invalid length message"] = () =>
                {
                    mapper.ParseName(NamePart.Last);
                    mapper.ValidationMessages.should_contain(invalidLength);
                };
            };

            context["when parsing two characters"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.LastName_Key, "ab");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have no validation errors"] = () =>
                {
                    mapper.ParseName(NamePart.Last);
                    mapper.ValidationMessages.Count.should_be(0);
                };
            };

            context["when parsing disallowed characters"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.LastName_Key, "ab!@");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have invalid character message"] = () =>
                {
                    mapper.ParseName(NamePart.Last);
                    mapper.ValidationMessages.should_contain(invalidCharacters);
                };
            };

            // letters, numbers, apostrophes, dashes or quotes
            context["allows letters, numbers, apostrophes, dashes and quotes"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.LastName_Key, @"a1'-""");
                    mapper = new PersonNameParser(recordFields);
                };

                it["should have no errors"] = () =>
                {
                    mapper.ParseName(NamePart.Last);
                    mapper.ValidationMessages.Count.should_be(0);
                };
            };
        }
    }
}