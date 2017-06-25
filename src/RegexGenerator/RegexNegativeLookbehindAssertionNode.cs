using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class RegexNegativeLookbehindAssertionNode
        : RegexLookaroundAssertionNode
    {
        /// <summary>
        /// Initialize an instance of <see cref="RegexNegativeLookbehindAssertionNode"/>.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        public RegexNegativeLookbehindAssertionNode(string pattern, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(pattern, min, max, quantifierOption)
        {
        }

        /// <summary>
        /// Initialize an instance of <see cref="RegexNegativeLookbehindAssertionNode"/>, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="innerNode">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        public RegexNegativeLookbehindAssertionNode(RegexNode innerNode, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(innerNode, min, max, quantifierOption)
        {
        }
    }
}
