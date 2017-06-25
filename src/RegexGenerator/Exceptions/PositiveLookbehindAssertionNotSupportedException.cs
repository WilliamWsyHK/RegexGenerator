using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class PositiveLookbehindAssertionNotSupportedException
        : RegexLanguageNotSupportedException
    {
        protected const string _message = "Positive lookbehind assertion is not supported by {0}.";

        public PositiveLookbehindAssertionNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        public PositiveLookbehindAssertionNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }
    }
}
