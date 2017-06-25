using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WilliamWsy.RegexGenerator
{
    public class RegexGenerator
    {
        public readonly IList<RegexNode> List;

        public RegexGenerator()
            : this(RegexLanguage.DotNet)
        {
        }

        public RegexGenerator(RegexLanguage regexLanguage)
        {
            SetRegexLanguage(regexLanguage);
            List = new List<RegexNode>();
        }

        public RegexLanguage RegexLanguage
        {
            get;
            private set;
        }

        public RegexLanguageStrategy RegexLanguageStrategy
        {
            get;
            private set;
        }

        public RegexGenerator SetRegexLanguage(RegexLanguage regexLanguage)
        {
            RegexLanguage = regexLanguage;
            RegexLanguageStrategy = new RegexLanguageStrategy(RegexLanguage);

            return this;
        }

        public bool IsNamedCapturingGroupSupported => RegexLanguageStrategy.Stringifier.IsNamedCapturingGroupSupported;

        public bool IsInlineGroupOptionsSupported => RegexLanguageStrategy.Stringifier.IsInlineGroupOptionsSupported;

        public bool IsAtomicGroupSupported => RegexLanguageStrategy.Stringifier.IsAtomicGroupSupported;

        public bool IsPositiveLookaheadAssertionSupported => RegexLanguageStrategy.Stringifier.IsPositiveLookaheadAssertionSupported;

        public bool IsNegativeLookaheadAssertionSupported => RegexLanguageStrategy.Stringifier.IsNegativeLookaheadAssertionSupported;

        public bool IsPositiveLookbehindAssertionSupported => RegexLanguageStrategy.Stringifier.IsPositiveLookbehindAssertionSupported;

        public bool IsNegativeLookbehindAssertionSupported => RegexLanguageStrategy.Stringifier.IsNegativeLookbehindAssertionSupported;

        public bool IsUnicodeCategorySupported => RegexLanguageStrategy.Stringifier.IsUnicodeCategorySupported;

        public bool IsConditionalSupported => RegexLanguageStrategy.Stringifier.IsConditionalSupported;

        #region Add

        /// <summary>
        /// Create and add <see cref="RegexNode"/> to the generator.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        public RegexGenerator Add(string pattern, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var node = new RegexNode(pattern, min, max, quantifierOption);
            return Add(node);
        }

        /// <summary>
        /// Create and add <see cref="RegexNode"/> to the generator, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="innerNode">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        public RegexGenerator Add(RegexNode innerNode, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var node = new RegexNode(innerNode, min, max, quantifierOption);
            return Add(node);
        }

        /// <summary>
        /// Add an existing <see cref="RegexNode"/> to the generator.
        /// </summary>
        /// <param name="node"><see cref="RegexNode"/> to be added.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is null.</exception>
        public RegexGenerator Add(RegexNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            List.Add(node);

            return this;
        }

        #endregion

        #region AddGroup

        /// <summary>
        /// Create and add <see cref="RegexGroupNode"/> to the generator.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="capturing">Defines the group to be capturing or not.</param>
        /// <param name="name">Optional name of capture group.</param>
        /// <param name="options">Optional inline options for the group.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="NamedCapturingGroupNotSupportedException">Named capturing group is not supported by <see cref="RegexLanguage"/>.</exception>
        /// <exception cref="InlineGroupOptionsNotSupportedException">Inline group option is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddGroup(string pattern, bool capturing = true, string name = null, string options = null, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var groupNode = new RegexGroupNode(pattern, capturing, name, options, min, max, quantifierOption);
            return AddGroup(groupNode);
        }

        /// <summary>
        /// Create and add <see cref="RegexGroupNode"/> to the generator, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="node">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="capturing">Defines the group to be capturing or not.</param>
        /// <param name="name">Optional name of capture group.</param>
        /// <param name="options">Optional inline options for the group.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="NamedCapturingGroupNotSupportedException">Named capturing group is not supported by <see cref="RegexLanguage"/>.</exception>
        /// <exception cref="InlineGroupOptionsNotSupportedException">Inline group option is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddGroup(RegexNode node, bool capturing = true, string name = null, string options = null, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var groupNode = new RegexGroupNode(node.ToString(RegexLanguageStrategy.Stringifier), capturing, name, options, min, max, quantifierOption);
            return AddGroup(groupNode);
        }

        /// <summary>
        /// Add an existing <see cref="RegexGroupNode"/> to the generator.
        /// </summary>
        /// <param name="group"><see cref="RegexGroupNode"/> to be added.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="NamedCapturingGroupNotSupportedException">Named capturing group is not supported by <see cref="RegexLanguage"/>.</exception>
        /// <exception cref="InlineGroupOptionsNotSupportedException">Inline group option is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddGroup(RegexGroupNode group)
        {
            if (!IsNamedCapturingGroupSupported && !string.IsNullOrWhiteSpace(group.Name))
            {
                throw new NamedCapturingGroupNotSupportedException(RegexLanguage);
            }
            if (!IsInlineGroupOptionsSupported && !string.IsNullOrWhiteSpace(group.Options))
            {
                throw new InlineGroupOptionsNotSupportedException(RegexLanguage);
            }

            return Add(group);
        }

        #endregion

        #region AddAtomicGroup

        /// <summary>
        /// Create and add <see cref="RegexAtomicGroupNode"/> to the generator.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="AtomicGroupNotSupportedException">Atomic group is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddAtomicGroup(string pattern, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var atomicGroup = new RegexAtomicGroupNode(pattern, min, max, quantifierOption);
            return AddAtomicGroup(atomicGroup);
        }

        /// <summary>
        /// Create and add <see cref="RegexAtomicGroupNode"/> to the generator, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="innerNode">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="AtomicGroupNotSupportedException">Atomic group is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddAtomicGroup(RegexNode innerNode, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var atomicGroup = new RegexAtomicGroupNode(innerNode, min, max, quantifierOption);
            return AddAtomicGroup(atomicGroup);
        }

        /// <summary>
        /// Add an existing <see cref="RegexAtomicGroupNode"/> to the generator.
        /// </summary>
        /// <param name="group"><see cref="RegexAtomicGroupNode"/> to be added.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="AtomicGroupNotSupportedException">Atomic group is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddAtomicGroup(RegexAtomicGroupNode atomicGroup)
        {
            if (!IsAtomicGroupSupported)
            {
                throw new AtomicGroupNotSupportedException(RegexLanguage);
            }

            return Add(atomicGroup);
        }

        #endregion

        #region AddPositiveLookaheadAssertion

        /// <summary>
        /// Create and add <see cref="RegexPositiveLookaheadAssertionNode"/> to the generator.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="PositiveLookaheadAssertionNotSupportedException">Positive lookahead assertion is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddPositiveLookaheadAssertion(string pattern, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var positiveLookaheadAssertion = new RegexPositiveLookaheadAssertionNode(pattern, min, max, quantifierOption);
            return AddPositiveLookaheadAssertion(positiveLookaheadAssertion);
        }

        /// <summary>
        /// Create and add <see cref="RegexPositiveLookaheadAssertionNode"/> to the generator, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="innerNode">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="PositiveLookaheadAssertionNotSupportedException">Positive lookahead assertion is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddPositiveLookaheadAssertion(RegexNode innerNode, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var positiveLookaheadAssertion = new RegexPositiveLookaheadAssertionNode(innerNode, min, max, quantifierOption);
            return AddPositiveLookaheadAssertion(positiveLookaheadAssertion);
        }

        /// <summary>
        /// Add an existing <see cref="RegexPositiveLookaheadAssertionNode"/> to the generator.
        /// </summary>
        /// <param name="group"><see cref="RegexPositiveLookaheadAssertionNode"/> to be added.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="PositiveLookaheadAssertionNotSupportedException">Positive lookahead assertion is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddPositiveLookaheadAssertion(RegexPositiveLookaheadAssertionNode positiveLookaheadAssertion)
        {
            if (!IsPositiveLookaheadAssertionSupported)
            {
                throw new PositiveLookaheadAssertionNotSupportedException(RegexLanguage);
            }

            return Add(positiveLookaheadAssertion);
        }

        #endregion

        #region AddNegativeLookaheadAssertion

        /// <summary>
        /// Create and add <see cref="RegexNegativeLookaheadAssertionNode"/> to the generator.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="NegativeLookaheadAssertionNotSupportedException">Negative lookahead assertion is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddNegativeLookaheadAssertion(string pattern, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var negativeLookaheadAssertion = new RegexNegativeLookaheadAssertionNode(pattern, min, max, quantifierOption);
            return AddNegativeLookaheadAssertion(negativeLookaheadAssertion);
        }

        /// <summary>
        /// Create and add <see cref="RegexNegativeLookaheadAssertionNode"/> to the generator, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="innerNode">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="NegativeLookaheadAssertionNotSupportedException">Negative lookahead assertion is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddNegativeLookaheadAssertion(RegexNode innerNode, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var negativeLookaheadAssertion = new RegexNegativeLookaheadAssertionNode(innerNode, min, max, quantifierOption);
            return AddNegativeLookaheadAssertion(negativeLookaheadAssertion);
        }

        /// <summary>
        /// Add an existing <see cref="RegexNegativeLookaheadAssertionNode"/> to the generator.
        /// </summary>
        /// <param name="group"><see cref="RegexNegativeLookaheadAssertionNode"/> to be added.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="NegativeLookaheadAssertionNotSupportedException">Negative lookahead assertion is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddNegativeLookaheadAssertion(RegexNegativeLookaheadAssertionNode negativeLookaheadAssertion)
        {
            if (!IsNegativeLookaheadAssertionSupported)
            {
                throw new NegativeLookaheadAssertionNotSupportedException(RegexLanguage);
            }

            return Add(negativeLookaheadAssertion);
        }

        #endregion

        #region AddPositiveLookbehindAssertion

        /// <summary>
        /// Create and add <see cref="RegexPositiveLookbehindAssertionNode"/> to the generator.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="PositiveLookbehindAssertionNotSupportedException">Positive lookbehind assertion is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddPositiveLookbehindAssertion(string pattern, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var positiveLookbehnidAssertion = new RegexPositiveLookbehindAssertionNode(pattern, min, max, quantifierOption);
            return AddPositiveLookbehindAssertion(positiveLookbehnidAssertion);
        }

        /// <summary>
        /// Create and add <see cref="RegexPositiveLookbehindAssertionNode"/> to the generator, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="innerNode">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="PositiveLookbehindAssertionNotSupportedException">Positive lookbehind assertion is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddPositiveLookbehindAssertion(RegexNode innerNode, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var positiveLookbehnidAssertion = new RegexPositiveLookbehindAssertionNode(innerNode, min, max, quantifierOption);
            return AddPositiveLookbehindAssertion(positiveLookbehnidAssertion);
        }

        /// <summary>
        /// Add an existing <see cref="RegexPositiveLookbehindAssertionNode"/> to the generator.
        /// </summary>
        /// <param name="group"><see cref="RegexPositiveLookbehindAssertionNode"/> to be added.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="PositiveLookbehindAssertionNotSupportedException">Positive lookbehind assertion is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddPositiveLookbehindAssertion(RegexPositiveLookbehindAssertionNode positiveLookbehnidAssertion)
        {
            if (!IsPositiveLookbehindAssertionSupported)
            {
                throw new PositiveLookbehindAssertionNotSupportedException(RegexLanguage);
            }

            return Add(positiveLookbehnidAssertion);
        }

        #endregion

        #region AddNegativeLookbehindAssertion

        /// <summary>
        /// Create and add <see cref="RegexNegativeLookbehindAssertionNode"/> to the generator.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="NegativeLookbehindAssertionNotSupportedException">Negative lookbehind assertion is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddNegativeLookbehindAssertion(string pattern, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var negativeLookbehindAssertion = new RegexNegativeLookbehindAssertionNode(pattern, min, max, quantifierOption);
            return AddNegativeLookbehindAssertion(negativeLookbehindAssertion);
        }

        /// <summary>
        /// Create and add <see cref="RegexNegativeLookbehindAssertionNode"/> to the generator, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="innerNode">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="NegativeLookbehindAssertionNotSupportedException">Negative lookbehind assertion is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddNegativeLookbehindAssertion(RegexNode innerNode, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var negativeLookbehindAssertion = new RegexNegativeLookbehindAssertionNode(innerNode, min, max, quantifierOption);
            return AddNegativeLookbehindAssertion(negativeLookbehindAssertion);
        }

        /// <summary>
        /// Add an existing <see cref="RegexNegativeLookbehindAssertionNode"/> to the generator.
        /// </summary>
        /// <param name="group"><see cref="RegexNegativeLookbehindAssertionNode"/> to be added.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="NegativeLookbehindAssertionNotSupportedException">Negative lookbehind assertion is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddNegativeLookbehindAssertion(RegexNegativeLookbehindAssertionNode negativeLookbehindAssertion)
        {
            if (!IsNegativeLookbehindAssertionSupported)
            {
                throw new NegativeLookbehindAssertionNotSupportedException(RegexLanguage);
            }

            return Add(negativeLookbehindAssertion);
        }

        #endregion

        #region AddConditional

        /// <summary>
        /// Create and add <see cref="RegexConditionalNode"/> to the generator.
        /// </summary>
        /// <param name="pattern">Regex pattern.</param>
        /// <param name="trueValue">Value of successful match.</param>
        /// <param name="falseValue">Value of failed match.</param>
        /// <param name="nameOrNumber">Name or number for backtracking.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="ConditionalNotSupportedException">Conditional is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddConditional(string pattern, string trueValue, string falseValue, string nameOrNumber = null, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var conditionalNode = new RegexConditionalNode(pattern, trueValue, falseValue, nameOrNumber, min, max, quantifierOption);
            return AddConditional(conditionalNode);
        }

        /// <summary>
        /// Create and add <see cref="RegexConditionalNode"/> to the generator, and include another <see cref="RegexNode"/> inside.
        /// </summary>
        /// <param name="innerNode">An existing <see cref="RegexNode"/> to be included.</param>
        /// <param name="trueValue">Value of successful match.</param>
        /// <param name="falseValue">Value of failed match.</param>
        /// <param name="nameOrNumber">Name or number for backtracking.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="ConditionalNotSupportedException">Conditional is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddConditional(RegexNode innerNode, string trueValue, string falseValue, string nameOrNumber = null, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var conditionalNode = new RegexConditionalNode(innerNode, trueValue, falseValue, nameOrNumber, min, max, quantifierOption);
            return AddConditional(conditionalNode);
        }

        /// <summary>
        /// Add an existing <see cref="RegexConditionalNode"/> to the generator.
        /// </summary>
        /// <param name="conditional"><see cref="RegexConditionalNode"/> to be added.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="ConditionalNotSupportedException">Conditional is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddConditional(RegexConditionalNode conditional)
        {
            if (!IsConditionalSupported)
            {
                throw new ConditionalNotSupportedException(RegexLanguage);
            }

            return Add(conditional);
        }

        #endregion

        #region AddUnicodeCategory

        /// <summary>
        /// Create and add <see cref="RegexUnicodeCategoryNode"/> to the generator.
        /// <seealso cref="RegexUnicodeCategoryNode(RegexUnicodeCategoryFlag, bool, int?, int?, RegexQuantifierOption)"/>
        /// <seealso cref="AddUnicodeCategory(RegexUnicodeCategoryNode)"/>
        /// </summary>
        /// <param name="unicodeCategory">Value from <see cref="RegexUnicodeCategoryFlag"/>.</param>
        /// <param name="negative">Optional value to determine if negate the category.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="UnicodeCategoryNotSupportedException">Unicode category is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddUnicodeCategory(RegexUnicodeCategoryFlag unicodeCategory, bool negative = false, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var unicodeCategoryNode = new RegexUnicodeCategoryNode(unicodeCategory, negative, min, max, quantifierOption);
            return AddUnicodeCategory(unicodeCategoryNode);
        }

        /// <summary>
        /// Create and add <see cref="RegexUnicodeCategoryNode"/> to the generator.
        /// <seealso cref="RegexUnicodeCategoryNode(string, bool, int?, int?, RegexQuantifierOption)"/>
        /// <seealso cref="AddUnicodeCategory(RegexUnicodeCategoryNode)"/>
        /// </summary>
        /// <param name="unicodeDesignation">Unicode designation or named block.</param>
        /// <param name="negative">Optional value to determine if negate the category.</param>
        /// <param name="min">Optional minimum number of occurance.</param>
        /// <param name="max">Optional maximum number of occurance.</param>
        /// <param name="quantifierOption">Optional quantifier option.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="UnicodeCategoryNotSupportedException">Unicode category is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddUnicodeCategory(string unicodeDesignation, bool negative = false, int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var unicodeCategoryNode = new RegexUnicodeCategoryNode(unicodeDesignation, negative, min, max, quantifierOption);
            return AddUnicodeCategory(unicodeCategoryNode);
        }

        /// <summary>
        /// Add an existing <see cref="RegexUnicodeCategoryNode"/> to the generator.
        /// <seealso cref="RegexUnicodeCategoryNode"/>
        /// </summary>
        /// <param name="unicodeCategory"><see cref="RegexUnicodeCategoryNode"/> to be added.</param>
        /// <returns><see cref="RegexGenerator"/></returns>
        /// <exception cref="UnicodeCategoryNotSupportedException">Unicode category is not supported by <see cref="RegexLanguage"/>.</exception>
        public RegexGenerator AddUnicodeCategory(RegexUnicodeCategoryNode unicodeCategory)
        {
            if (!IsUnicodeCategorySupported)
            {
                throw new UnicodeCategoryNotSupportedException(RegexLanguage);
            }

            return Add(unicodeCategory);
        }

        #endregion

        /// <summary>
        /// Clear the <see cref="List"/>.
        /// </summary>
        /// <returns><see cref="RegexGenerator"/></returns>
        public RegexGenerator Clear()
        {
            List.Clear();

            return this;
        }

        /// <summary>
        /// Convert all nodes to string base on <see cref="RegexLanguage"/>.
        /// </summary>
        /// <returns>Regex string for <see cref="RegexLanguage"/></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var node in List)
            {
                sb.Append(node.ToString(RegexLanguageStrategy.Stringifier));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Create a <see cref="Regex"/> instance, with optional parameters.
        /// <seealso cref="Regex"/>
        /// </summary>
        /// <param name="options">Optional <see cref="RegexOptions"/> parameter for creating <see cref="Regex"/>.</param>
        /// <param name="matchTimeout">Optional <see cref="TimeSpan"/> parameter for creating <see cref="Regex"/>.</param>
        /// <returns>New <see cref="Regex"/> object.</returns>
        /// <exception cref="NotSupportedException"><see cref="RegexLanguage"/> is not <see cref="RegexLanguage.DotNet"/></exception>
        public Regex Create(RegexOptions? options = null, TimeSpan? matchTimeout = null)
        {
            if (RegexLanguage != RegexLanguage.DotNet)
            {
                throw new RegexLanguageNotSupportedException(RegexLanguage, $"Only {RegexLanguage.DotNet} {nameof(Regex)} object can be created.");
            }

            if (options != null)
            {
                if (matchTimeout != null)
                {
                    return new Regex(ToString(), options.Value, matchTimeout.Value);
                }
                else
                {
                    return new Regex(ToString(), options.Value);
                }
            }
            return new Regex(ToString());
        }
    }
}
