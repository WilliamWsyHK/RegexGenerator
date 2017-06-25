using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class NamedCapturingGroupNotSupportedException
        : RegexLanguageNotSupportedException
    {
        protected const string _message = "Named capturing group is not supported by {0}.";

        public NamedCapturingGroupNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        public NamedCapturingGroupNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }
    }
}
