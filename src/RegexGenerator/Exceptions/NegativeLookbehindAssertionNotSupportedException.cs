using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class NegativeLookbehindAssertionNotSupportedException
        : RegexLanguageNotSupportedException
    {
        protected const string _message = "Negative lookbehind assertion is not supported by {0}.";

        public NegativeLookbehindAssertionNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        public NegativeLookbehindAssertionNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }
    }
}
