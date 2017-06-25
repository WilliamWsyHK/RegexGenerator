using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class InlineGroupOptionsNotSupportedException
        : RegexLanguageNotSupportedException
    {
        protected const string _message = "Inline group options is not supported by {0}.";

        public InlineGroupOptionsNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        public InlineGroupOptionsNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }
    }
}
