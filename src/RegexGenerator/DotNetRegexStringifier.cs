using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public sealed class DotNetRegexStringifier
        : RegexStringifier, IMultipleNamedCaptureGroupSyntaxRegexStringifier
    {
        public DotNetRegexStringifier()
            : base()
        {
        }

        public override RegexLanguage RegexLanguage => RegexLanguage.DotNet;

        public override bool IsNamedCapturingGroupSupported => true;

        public override bool IsInlineGroupOptionsSupported => true;

        public override bool IsAtomicGroupSupported => true;

        public override bool IsPositiveLookaheadAssertionSupported => true;

        public override bool IsNegativeLookaheadAssertionSupported => true;

        public override bool IsPositiveLookbehindAssertionSupported => true;

        public override bool IsNegativeLookbehindAssertionSupported => true;

        public override bool IsUnicodeCategorySupported => true;

        public override bool IsConditionalSupported => true;

        public bool SupportsAngleBracketNaming => true;

        public bool SupportsApostropheNaming => true;

        public NamedCaptureGroupBracketOption NamedCaptureGroupBracketOption
        {
            get;
            set;
        }

        public override string ToTokenString(RegexToken token)
        {
            switch (token)
            {
                case RegexToken.GroupNameOpen:
                    if (NamedCaptureGroupBracketOption == NamedCaptureGroupBracketOption.AngleBracket)
                    {
                        return "<";
                    }
                    else if (NamedCaptureGroupBracketOption == NamedCaptureGroupBracketOption.Apostrophe)
                    {
                        return "'";
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(NamedCaptureGroupBracketOption));
                    }
                case RegexToken.GroupNameClose:
                    if (NamedCaptureGroupBracketOption == NamedCaptureGroupBracketOption.AngleBracket)
                    {
                        return ">";
                    }
                    else if (NamedCaptureGroupBracketOption == NamedCaptureGroupBracketOption.Apostrophe)
                    {
                        return "'";
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(NamedCaptureGroupBracketOption));
                    }
                default:
                    return base.ToTokenString(token);
            }
        }

        public override string ToGroupString(RegexGroupNode group)
        {
            if (!string.IsNullOrWhiteSpace(group.Name) && !string.IsNullOrWhiteSpace(group.Options))
            {
                throw new NotSupportedException($"Due to constraint from dotnet, property {nameof(group.Name)} and {nameof(group.Options)} of {nameof(RegexGroupNode)} cannot be set at the same time.");
            }

            return base.ToGroupString(group);
        }
    }
}
