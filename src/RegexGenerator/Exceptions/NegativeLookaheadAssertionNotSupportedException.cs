using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class NegativeLookaheadAssertionNotSupportedException
        : RegexLanguageNotSupportedException
    {
        protected const string _message = "Negative lookahead assertion is not supported by {0}.";

        public NegativeLookaheadAssertionNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        public NegativeLookaheadAssertionNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }
    }
}
