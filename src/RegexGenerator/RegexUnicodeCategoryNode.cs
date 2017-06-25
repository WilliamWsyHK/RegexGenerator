using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public class RegexUnicodeCategoryNode
        : RegexNode
    {
        /// <summary>
        /// Initialize an instance of <see cref="RegexUnicodeCategoryNode"/> with flag <see cref="RegexUnicodeCategoryFlag"/>.
        /// <para>Internally call <see cref="Enum.ToString()"/>. Therefore, only use one value from <see cref="RegexUnicodeCategoryFlag"/> while using this constructor;
        /// Use <see cref="RegexUnicodeCategoryNode(string, bool, int?, int?, RegexQuantifierOption)"/> instead for named unicode block.</para>
        /// <seealso cref="RegexUnicodeCategoryNode(string, bool, int?, int?, RegexQuantifierOption)"/>
        /// </summary>
        /// <remarks>
        /// <code>
        /// (RegexUnicodeCategoryFlag.Lu | RegexUnicodeCategoryFlag.Ll).ToString() // will generate "Lu, Ll", which is a invalid form.
        /// </code>
        /// </remarks>
        /// <param name="unicodeCategory">Value from <see cref="RegexUnicodeCategoryFlag"/>.</param>
        /// <param name="negative">Optional value to determine if negate the category.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        public RegexUnicodeCategoryNode(RegexUnicodeCategoryFlag unicodeCategory, bool negative = false, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(unicodeCategory.ToString(), min, max, quantifierOption)
        {
            Negative = negative;
        }

        /// <summary>
        /// Initialize an instance of <see cref="RegexUnicodeCategoryNode"/> with unicode designation or name.
        /// </summary>
        /// <param name="unicodeDesignation">Unicode designation or named block.</param>
        /// <param name="negative">Optional value to determine if negate the category.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        public RegexUnicodeCategoryNode(string unicodeDesignation, bool negative = false, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(unicodeDesignation, min, max, quantifierOption)
        {
            Negative = negative;
        }

        /// <summary>
        /// Determine if negate the category.
        /// </summary>
        public bool Negative
        {
            get;
            set;
        }
    }
}
