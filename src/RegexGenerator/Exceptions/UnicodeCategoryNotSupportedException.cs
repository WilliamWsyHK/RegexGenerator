using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class UnicodeCategoryNotSupportedException
        : RegexLanguageNotSupportedException
    {
        protected const string _message = "Unicode category is not supported by {0}.";

        public UnicodeCategoryNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        public UnicodeCategoryNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }
    }
}
