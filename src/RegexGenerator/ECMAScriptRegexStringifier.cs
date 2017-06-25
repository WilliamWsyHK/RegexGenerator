using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class ECMAScriptRegexStringifier
        : RegexStringifier
    {
        public ECMAScriptRegexStringifier()
            : base()
        {
        }

        public override RegexLanguage RegexLanguage => RegexLanguage.ECMAScript;

        public override bool IsNamedCapturingGroupSupported => false;

        public override bool IsInlineGroupOptionsSupported => false;

        public override bool IsAtomicGroupSupported => false;

        public override bool IsPositiveLookaheadAssertionSupported => true;

        public override bool IsNegativeLookaheadAssertionSupported => true;

        public override bool IsPositiveLookbehindAssertionSupported => false;

        public override bool IsNegativeLookbehindAssertionSupported => false;

        public override bool IsUnicodeCategorySupported => false;

        public override bool IsConditionalSupported => false;

        public override string ToString(RegexNode node)
        {
            return $"/{base.ToString(node)}/";
        }
    }
}
