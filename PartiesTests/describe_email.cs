using Parties;
using NSpec;

namespace PartiesTests
{
    public class describe_email : nspec
    {
        public string test_string;
        public Email email;

        void parsing_email()
        {
            context["when parsing me@there.com"] = () =>
            {
                before = () =>
                {
                    test_string = "me@there.com";
                    email = new Email(test_string);
                };

                it["should be valid"] = () =>
                {
                    email.IsValid.should_be_true();
                };

                it["should have mailbox of 'me'"] = () =>
                {
                    email.Mailbox.should_be("me");
                };

                it["should have a host of ''"] = () =>
                {
                    email.Host.should_be("there.com");
                };

                it["should display 'me@there.com'"] = () =>
                {
                    email.Text.should_be("me@there.com");
                };
            };

            context["email without an @ is invalid"] = () =>
            {
                before = () =>
                {
                    test_string = "methere.com";
                    email = new Email(test_string);
                };
                it["should not be valid"] = () =>
                {
                    email.IsValid.should_be_false();
                };
            };
        }
    }
}