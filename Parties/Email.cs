using System.Text.RegularExpressions;

namespace Parties
{
    /// <summary>
    /// Email "validator" straight for the "horse"
    /// http://msdn.microsoft.com/en-us/library/01escwtf.aspx
    /// </summary>
    public class Email
    {
        public bool IsValid
        {
            get
            {
                return Regex.IsMatch(email, 
              @"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + 
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            }
        }

        public string Mailbox
        {
            get
            {
                return mailBox;
            }
        }

        public string Host { get { return host; } }

        public string Text
        {
            get
            {
                return string.Format("{0}@{1}", mailBox, host);
            }
        }

        public Email() :  this(string.Empty)
        {
        }

        public Email(string email)
        {
            this.email = email;
            string[] parts = email.Split('@');

            if (parts.Length == 2)
            {
                mailBox = parts[0];
                host = parts[1];
            }
        }

        private string email = string.Empty;
        private string mailBox = string.Empty;
        private string host = string.Empty;
    }
}
