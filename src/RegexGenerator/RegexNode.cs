using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class RegexNode
    {
        private RegexNode _innerNode;
        private string _pattern;

        /// <summary>
        /// Initialize an instance of <see cref="RegexNode"/>.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        public RegexNode(string pattern, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            SetPattern(pattern);
            Minimum = min;
            Maximum = max;
            RegexQuantifierOption = quantifierOption;
        }

        /// <summary>
        /// Initialize an instance of <see cref="RegexNode"/>, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="innerNode">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        public RegexNode(RegexNode innerNode, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            SetInnerNode(innerNode);
            Minimum = min;
            Maximum = max;
            RegexQuantifierOption = quantifierOption;
        }

        /// <summary>
        /// Determine if the inner node is included.
        /// </summary>
        public bool IsInnerNodeIncluded
        {
            get;
            private set;
        }

        /// <summary>
        /// The included inner node.
        /// </summary>
        public RegexNode InnerNode
        {
            get
            {
                if (!IsInnerNodeIncluded)
                {
                    throw new NotSupportedException();
                }
                return _innerNode;
            }
        }

        /// <summary>
        /// Normal regex pattern.
        /// </summary>
        public string Pattern
        {
            get
            {
                if (IsInnerNodeIncluded)
                {
                    throw new NotSupportedException();
                }
                return _pattern;
            }
        }

        /// <summary>
        /// Set this <see cref="RegexNode"/> to include an inner node.
        /// </summary>
        /// <param name="value">An existing <see cref="RegexNode"/> to be included.</param>
        /// <returns><see cref="RegexNode"/></returns>
        /// <exception cref="InvalidOperationException"><see cref="value"/> cannot be the same as <see cref="this"/>.</exception>
        public RegexNode SetInnerNode(RegexNode value)
        {
            if (value == this)
            {
                throw new InvalidOperationException();
            }

            IsInnerNodeIncluded = true;
            _pattern = null;
            _innerNode = value;

            return this;
        }

        /// <summary>
        /// Set this <see cref="RegexNode"/> to use pattern string.
        /// </summary>
        /// <param name="value">Pattern string.</param>
        /// <returns><see cref="RegexNode"/></returns>
        public RegexNode SetPattern(string value)
        {
            IsInnerNodeIncluded = false;
            _innerNode = null;
            _pattern = value;

            return this;
        }

        /// <summary>
        /// Minimum number of occurance.
        /// </summary>
        public int? Minimum
        {
            get;
            set;
        }

        /// <summary>
        /// Maximum number of occurance.
        /// </summary>
        public int? Maximum
        {
            get;
            set;
        }

        /// <summary>
        /// Quantifier options, such as Greedy, Lazy, or Possessive.
        /// </summary>
        public RegexQuantifierOption RegexQuantifierOption
        {
            get;
            set;
        }

        public virtual string ToString(IRegexStringifier stringifier)
        {
            if (stringifier == null)
            {
                throw new ArgumentNullException(nameof(stringifier));
            }

            return stringifier.ToString(this);
        }
    }
}
