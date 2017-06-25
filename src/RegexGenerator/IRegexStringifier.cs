using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public interface IRegexStringifier
    {
        RegexLanguage RegexLanguage
        {
            get;
        }

        bool IsNamedCapturingGroupSupported
        {
            get;
        }

        bool IsInlineGroupOptionsSupported
        {
            get;
        }

        bool IsAtomicGroupSupported
        {
            get;
        }

        bool IsPositiveLookaheadAssertionSupported
        {
            get;
        }

        bool IsNegativeLookaheadAssertionSupported
        {
            get;
        }

        bool IsPositiveLookbehindAssertionSupported
        {
            get;
        }

        bool IsNegativeLookbehindAssertionSupported
        {
            get;
        }

        bool IsUnicodeCategorySupported
        {
            get;
        }

        bool IsConditionalSupported
        {
            get;
        }

        string ToString(RegexNode node);
        string AddQuantifier(string pattern, int? min, int? max, RegexQuantifierOption quantifierOption);
        string ToQuantifierOptionString(RegexQuantifierOption quantifierOption);
        string ToTokenString(RegexToken token);
        string ToNodeString(RegexNode node);
        string ToGroupString(RegexGroupNode group);
        string ToAtomicGroupString(RegexAtomicGroupNode atomicGroup);
        string ToPositiveLookaheadAssertionString(RegexPositiveLookaheadAssertionNode positiveLookaheadAssertion);
        string ToNegativeLookaheadAssertionString(RegexNegativeLookaheadAssertionNode negativeLookaheadAssertion);
        string ToPositiveLookbehindAssertionString(RegexPositiveLookbehindAssertionNode positiveLookbehindAssertion);
        string ToNegativeLookbehindAssertionString(RegexNegativeLookbehindAssertionNode negativeLookbehindAssertion);
        string ToConditionalString(RegexConditionalNode conditionalNode);
        string ToUnicodeCategoryString(RegexUnicodeCategoryNode unicodeCategoryNode);
    }
}
