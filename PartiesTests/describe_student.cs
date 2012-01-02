using NSpec;
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace PartiesTests
{
    public class describe_student_mapper : nspec
    {
        Dictionary<string, object> recordFields;
        Student mapper;

        void before_each()
        {
            recordFields = new Dictionary<string, object>();
        }
    
        void parsing_firstName()
        {
            context["when parsing an empty string"] = () =>
            {
                before = () =>
                    {
                        recordFields.Add( PersonNameParser.FirstName_Key, string.Empty );
                        mapper = new Student(recordFields);
                    };

                it["should have null value for First Name"] = () =>
                    {
                        Assert.Null(mapper.First);
                    };
                it["should have an invalid import message"] = () =>
                    {
                        mapper.ValidationMessages.should_contain(PersonNameParser.NameRequiredMessage);
                    };
            };

            context["when parsing name with invalid characters"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(PersonNameParser.FirstName_Key, "");
                    mapper = new Student(recordFields);
                };

                it["should contain invalid character message"] = () =>
                {
                    mapper.ValidationMessages.should_contain(PersonNameParser.NameCharacters);
                };
            };
        }

        void parsing_lastname()
        {
            context["when parsing empty string"] = () =>
            {

            };
        }
    }
}