using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class ConditionalNotSupportedException
        : RegexLanguageNotSupportedException
    {
        protected const string _message = "Conditional is not supported by {0}.";

        public ConditionalNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        public ConditionalNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }
    }
}
