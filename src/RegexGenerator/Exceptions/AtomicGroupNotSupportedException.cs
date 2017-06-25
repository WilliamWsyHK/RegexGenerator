using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class AtomicGroupNotSupportedException
        : RegexLanguageNotSupportedException
    {
        protected const string _message = "Atomic group is not supported by {0}.";

        public AtomicGroupNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        public AtomicGroupNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }
    }
}
