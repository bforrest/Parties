using NSpec;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PartiesTests
{
    public class describe_ssn_parser : nspec
    {
        Dictionary<string, object> recordFields;
        SSNMapper mapper;

        void before_each()
        {
            recordFields = new Dictionary<string, object>();
        }

        void parsing_ssn()
        {
            context["when parsing an empty string"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(Student.SSN_KEY, string.Empty);
                    mapper = new SSNMapper(recordFields);
                };

                it["should have null value for SSN"] = () =>
                {
                    Assert.Null(mapper.SSN);
                };
                it["should have no validation errors"] = () =>
                {
                    mapper.ValidationMessages.should_be_empty();
                };
            };

            context["when parsing less than 4 characters (123)"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(Student.SSN_KEY, "123");
                    mapper = new SSNMapper(recordFields);
                };
                it["should have null SSN"] = () =>
                {
                    Assert.Null(mapper.SSN);
                };
                it["should have validation message"] = () =>
                {
                    mapper.ValidationMessages.should_not_be_empty();
                };
                it["should contain invalid format message"] = () =>
                {
                    mapper.ValidationMessages.should_contain(Student.InvalidFormat);
                };
            };

            context["when parsing non-numeric values (AB122)"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(Student.SSN_KEY, "AB122");
                    mapper = new SSNMapper(recordFields);
                };
                it["should contain invalid format message"] = () =>
                {
                    mapper.ValidationMessages.should_contain(Student.InvalidFormat);
                };
            };

            context["when parsing 4 characters (1234)"] = () =>
            {
                string passed = "1234";
                before = () =>
                {
                    recordFields.Add(Student.SSN_KEY, passed);
                    mapper = new SSNMapper(recordFields);
                };
                it["should have no warnings"] = () =>
                {
                    mapper.ValidationMessages.should_be_empty();
                };
                it["should SSN value that was passed"] = () =>
                {
                    mapper.SSN.should_be(passed);
                };
            };

            context["when parsing XXX-XX-XXXX (123-12-1234)"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(Student.SSN_KEY, "123-12-1234");
                    mapper = new SSNMapper(recordFields);
                };
                it["should SSN value that was passed"] = () =>
                {
                    mapper.SSN.should_be("1234");
                };
            };

            context["when parsing 9 digits (999991234)"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(Student.SSN_KEY, "999991234");
                    mapper = new SSNMapper(recordFields);
                };
                it["should SSN value that was passed"] = () =>
                {
                    mapper.SSN.should_be("1234");
                };
            };
            context["when parsing 10 digits (0123456789)"] = () =>
            {
                before = () =>
                {
                    recordFields.Add(Student.SSN_KEY, "0123456789");
                    mapper = new SSNMapper(recordFields);
                };
                it["should contain invalid format message"] = () =>
                {
                    Console.WriteLine("SSN '{0}'", mapper.SSN);
                    mapper.ValidationMessages.should_contain(Student.InvalidFormat);
                };
            };
        }
    }
}