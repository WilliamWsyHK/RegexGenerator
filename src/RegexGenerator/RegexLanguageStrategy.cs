using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public enum RegexLanguage
    {
        DotNet,
        ECMAScript
    }

    public sealed class RegexLanguageStrategy
    {
        public RegexLanguageStrategy(RegexLanguage regexLanguage)
        {
            RegexLanguage = regexLanguage;

            switch (RegexLanguage)
            {
                case RegexLanguage.DotNet:
                    Stringifier = new DotNetRegexStringifier();
                    break;
                case RegexLanguage.ECMAScript:
                    Stringifier = new ECMAScriptRegexStringifier();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public RegexLanguage RegexLanguage
        {
            get;
            private set;
        }

        public IRegexStringifier Stringifier
        {
            get;
            private set;
        }
    }
}
