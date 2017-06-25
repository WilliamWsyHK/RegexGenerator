using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class RegexConditionalNode
        : RegexNode
    {
        /// <summary>
        /// Initialize an instance of <see cref="RegexConditionalNode"/>.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="trueValue">Value of successful match.</param>
        /// <param name="falseValue">Value of failed match.</param>
        /// <param name="nameOrNumber">Name or number for backtracking.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        public RegexConditionalNode(string pattern, string trueValue, string falseValue, string nameOrNumber = null, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(pattern, min, max, quantifierOption)
        {
            TrueValue = trueValue;
            FalseValue = falseValue;
            NameOrNumber = nameOrNumber;
        }

        /// <summary>
        /// Initialize an instance of <see cref="RegexConditionalNode"/>, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="innerNode">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="trueValue">Value of successful match.</param>
        /// <param name="falseValue">Value of failed match.</param>
        /// <param name="nameOrNumber">Name or number for backtracking.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        public RegexConditionalNode(RegexNode innerNode, string trueValue, string falseValue, string nameOrNumber = null, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(innerNode, min, max, quantifierOption)
        {
            TrueValue = trueValue;
            FalseValue = falseValue;
            NameOrNumber = nameOrNumber;
        }

        public string TrueValue
        {
            get;
            set;
        }

        public string FalseValue
        {
            get;
            set;
        }

        public string NameOrNumber
        {
            get;
            set;
        }
    }
}
