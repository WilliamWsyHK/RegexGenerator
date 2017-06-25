using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public enum NamedCaptureGroupBracketOption
    {
        AngleBracket,
        Apostrophe
    }

    public interface IMultipleNamedCaptureGroupSyntaxRegexStringifier
        : IRegexStringifier
    {
        NamedCaptureGroupBracketOption NamedCaptureGroupBracketOption
        {
            get;
        }

        bool SupportsAngleBracketNaming
        {
            get;
        }

        bool SupportsApostropheNaming
        {
            get;
        }
    }
}
