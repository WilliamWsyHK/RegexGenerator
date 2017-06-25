using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class RegexPositiveLookaheadAssertionNode
        : RegexLookaroundAssertionNode
    {
        /// <summary>
        /// Initialize an instance of <see cref="RegexPositiveLookaheadAssertionNode"/>.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        public RegexPositiveLookaheadAssertionNode(string pattern, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(pattern, min, max, quantifierOption)
        {
        }

        /// <summary>
        /// Initialize an instance of <see cref="RegexPositiveLookaheadAssertionNode"/>, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="innerNode">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        public RegexPositiveLookaheadAssertionNode(RegexNode innerNode, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(innerNode, min, max, quantifierOption)
        {
        }
    }
}
