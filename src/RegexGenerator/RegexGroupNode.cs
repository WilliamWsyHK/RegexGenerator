using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class RegexGroupNode
        : RegexNode
    {
        /// <summary>
        /// Initialize an instance of <see cref="RegexGroupNode"/>.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="capturing">Defines the group to be capturing or not.</param>
        /// <param name="name">Optional name of capture group.</param>
        /// <param name="options">Optional inline options for the group.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        public RegexGroupNode(string pattern, bool capturing = true, string name = null, string options = null, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(pattern, min, max, quantifierOption)
        {
            SetName(name);
            SetIsCapturingGroup(capturing);
            Options = options;
        }

        /// <summary>
        /// Initialize an instance of <see cref="RegexGroupNode"/>, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="innerNode">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="capturing">Defines the group to be capturing or not.</param>
        /// <param name="name">Optional name of capture group.</param>
        /// <param name="options">Optional inline options for the group.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        public RegexGroupNode(RegexNode innerNode, bool capturing = true, string name = null, string options = null, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(innerNode, min, max, quantifierOption)
        {
            SetName(name);
            SetIsCapturingGroup(capturing);
            Options = options;
        }

        public bool IsCapturingGroup
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string Options
        {
            get;
            set;
        }

        /// <summary>
        /// Set if <see cref="RegexGroupNode"/> is capturing.
        /// </summary>
        /// <remarks>If <see cref="Name"/> is not null, <paramref name="value"/> will be ignored.</remarks>
        /// <param name="value"></param>
        /// <returns><see cref="RegexGroupNode"/></returns>
        public RegexGroupNode SetIsCapturingGroup(bool value)
        {
            IsCapturingGroup = !string.IsNullOrWhiteSpace(Name) || value;

            return this;
        }

        /// <summary>
        /// Set name of capturing group.
        /// </summary>
        /// <remarks><see cref="IsCapturingGroup"/> is set to <see cref="true"/>.</remarks>
        /// <param name="value"></param>
        /// <returns><see cref="RegexGroupNode"/></returns>
        public RegexGroupNode SetName(string value)
        {
            IsCapturingGroup = value != null;
            Name = value;

            return this;
        }
    }
}
