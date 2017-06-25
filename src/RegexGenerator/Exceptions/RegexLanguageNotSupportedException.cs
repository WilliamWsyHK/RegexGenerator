using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class RegexLanguageNotSupportedException
        : NotSupportedException
    {
        public RegexLanguage RegexLanguage
        {
            get;
        }

        public RegexLanguageNotSupportedException(RegexLanguage regexLanguage)
            : base()
        {
            RegexLanguage = regexLanguage;
        }

        public RegexLanguageNotSupportedException(RegexLanguage regexLanguage, string message)
            : base(message)
        {
            RegexLanguage = regexLanguage;
        }

        public RegexLanguageNotSupportedException(RegexLanguage regexLanguage, string message, Exception innerException)
            : base(message, innerException)
        {
            RegexLanguage = regexLanguage;
        }
    }
}
