using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class PositiveLookaheadAssertionNotSupportedException
        : RegexLanguageNotSupportedException
    {
        protected const string _message = "Positive lookahead assertion is not supported by {0}.";

        public PositiveLookaheadAssertionNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        public PositiveLookaheadAssertionNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }
    }
}
