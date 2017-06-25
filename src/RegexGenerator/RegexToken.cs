using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public enum RegexToken
    {
        Any,
        GroupOpen,
        GroupNonCapturing,
        GroupOptionStart,
        GroupNameOpen,
        GroupNameClose,
        GroupOptionEnd,
        GroupClose,
        ConditionalOpen,
        ConditionalClose,
        AtomicGroupOpen,
        AtomicGroupClose,
        PositiveLookaheadAssertionOpen,
        PositiveLookaheadAssertionClose,
        NegativeLookaheadAssertionOpen,
        NegativeLookaheadAssertionClose,
        PositiveLookbehindAssertionOpen,
        PositiveLookbehindAssertionClose,
        NegativeLookbehindAssertionOpen,
        NegativeLookbehindAssertionClose,
        CharacterGroupOpen,
        CharacterGroupNegative,
        CharacterGroupClose,
        Alternation,
        ZeroOrOne,
        ZeroOrMore,
        OneOrMore,
        Lazy,
        Possessive,
        QuantifierOpen,
        QuantifierSeparator,
        QuantifierClose,
        StringStartAnchor,
        StringEndAnchor,
        UnicodeCategoryOpen,
        UnicodeCategoryClose,
        NegativeUnicodeCategoryOpen,
        NegativeUnicodeCategoryClose
    }
}
