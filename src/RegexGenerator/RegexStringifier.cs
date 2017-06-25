using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    public abstract class RegexStringifier
        : IRegexStringifier
    {
        public abstract RegexLanguage RegexLanguage
        {
            get;
        }

        public abstract bool IsNamedCapturingGroupSupported
        {
            get;
        }

        public abstract bool IsInlineGroupOptionsSupported
        {
            get;
        }

        public abstract bool IsAtomicGroupSupported
        {
            get;
        }

        public abstract bool IsPositiveLookaheadAssertionSupported
        {
            get;
        }

        public abstract bool IsNegativeLookaheadAssertionSupported
        {
            get;
        }

        public abstract bool IsPositiveLookbehindAssertionSupported
        {
            get;
        }

        public abstract bool IsNegativeLookbehindAssertionSupported
        {
            get;
        }

        public abstract bool IsUnicodeCategorySupported
        {
            get;
        }

        public abstract bool IsConditionalSupported
        {
            get;
        }

        public virtual string ToString(RegexNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            switch (node)
            {
                case RegexAtomicGroupNode ag:
                    return ToAtomicGroupString(ag);
                case RegexConditionalNode c:
                    return ToConditionalString(c);
                case RegexPositiveLookaheadAssertionNode pla:
                    return ToPositiveLookaheadAssertionString(pla);
                case RegexNegativeLookaheadAssertionNode nla:
                    return ToNegativeLookaheadAssertionString(nla);
                case RegexPositiveLookbehindAssertionNode plb:
                    return ToPositiveLookbehindAssertionString(plb);
                case RegexNegativeLookbehindAssertionNode nlb:
                    return ToNegativeLookbehindAssertionString(nlb);
                case RegexGroupNode g:
                    return ToGroupString(g);
                case RegexUnicodeCategoryNode un:
                    return ToUnicodeCategoryString(un);
                default:
                    return ToNodeString(node);
            }
        }

        public virtual string AddQuantifier(string pattern, int? min, int? max, RegexQuantifierOption quantifierOption)
        {
            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min));
            }
            if (max < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max));
            }
            var quantifierOptionString = ToQuantifierOptionString(quantifierOption);

            string result = string.Empty;

            if ((!min.HasValue && !max.HasValue) || (min == 1 && max == 1))
            {
                result = pattern;
            }
            else if (min.HasValue && max.HasValue)
            {
                if (min == 0 && max == 1)
                {
                    result = pattern + ToTokenString(RegexToken.ZeroOrOne);
                }
                else if (min == max)
                {
                    result = pattern + ToTokenString(RegexToken.QuantifierOpen) + min.Value + ToTokenString(RegexToken.QuantifierClose);
                }
                else
                {
                    result = pattern + ToTokenString(RegexToken.QuantifierOpen) + min.Value + ToTokenString(RegexToken.QuantifierSeparator) + max.Value + ToTokenString(RegexToken.QuantifierClose);
                }
            }
            else if (min == 0 && !max.HasValue)
            {
                result = pattern + ToTokenString(RegexToken.ZeroOrMore);
            }
            else if (min == 1 && !max.HasValue)
            {
                result = pattern + ToTokenString(RegexToken.OneOrMore);
            }
            else if (min.HasValue && !max.HasValue)
            {
                result = pattern + ToTokenString(RegexToken.QuantifierOpen) + min.Value + ToTokenString(RegexToken.QuantifierSeparator) + ToTokenString(RegexToken.QuantifierClose);
            }
            else if (!min.HasValue && max.HasValue)
            {
                result = pattern + ToTokenString(RegexToken.QuantifierOpen) + ToTokenString(RegexToken.QuantifierSeparator) + max.Value + ToTokenString(RegexToken.QuantifierClose);
            }
            else
            {
                throw new NotImplementedException();
            }
            
            return result + quantifierOptionString;
        }

        public virtual string ToQuantifierOptionString(RegexQuantifierOption quantifierOption)
        {
            switch (quantifierOption)
            {
                case RegexQuantifierOption.Greedy:
                    return "";
                case RegexQuantifierOption.Lazy:
                    return ToTokenString(RegexToken.Lazy);
                case RegexQuantifierOption.Possessive:
                    return ToTokenString(RegexToken.Possessive);
                default:
                    throw new ArgumentOutOfRangeException(nameof(quantifierOption));
            }
        }

        public virtual string ToTokenString(RegexToken token)
        {
            switch (token)
            {
                case RegexToken.Any:
                    return ".";
                case RegexToken.GroupOpen:
                    return "(";
                case RegexToken.GroupNonCapturing:
                    return "?:";
                case RegexToken.GroupOptionStart:
                    if (!IsInlineGroupOptionsSupported)
                    {
                        throw new InlineGroupOptionsNotSupportedException(RegexLanguage);
                    }
                    return "?";
                case RegexToken.GroupNameOpen:
                    if (!IsNamedCapturingGroupSupported)
                    {
                        throw new NamedCapturingGroupNotSupportedException(RegexLanguage);
                    }
                    else
                    {
                        return "<";
                    }
                case RegexToken.GroupNameClose:
                    if (!IsNamedCapturingGroupSupported)
                    {
                        throw new NamedCapturingGroupNotSupportedException(RegexLanguage);
                    }
                    else
                    {
                        return ">";
                    }
                case RegexToken.GroupOptionEnd:
                    if (!IsInlineGroupOptionsSupported)
                    {
                        throw new InlineGroupOptionsNotSupportedException(RegexLanguage);
                    }
                    return ":";
                case RegexToken.GroupClose:
                    return ")";
                case RegexToken.ConditionalOpen:
                    if (!IsConditionalSupported)
                    {
                        throw new ConditionalNotSupportedException(RegexLanguage);
                    }
                    return "(?";
                case RegexToken.ConditionalClose:
                    if (!IsConditionalSupported)
                    {
                        throw new ConditionalNotSupportedException(RegexLanguage);
                    }
                    return ")";
                case RegexToken.AtomicGroupOpen:
                    if (!IsAtomicGroupSupported)
                    {
                        throw new AtomicGroupNotSupportedException(RegexLanguage);
                    }
                    return "(?>";
                case RegexToken.AtomicGroupClose:
                    if (!IsAtomicGroupSupported)
                    {
                        throw new AtomicGroupNotSupportedException(RegexLanguage);
                    }
                    return ")";
                case RegexToken.PositiveLookaheadAssertionOpen:
                    if (!IsPositiveLookaheadAssertionSupported)
                    {
                        throw new PositiveLookaheadAssertionNotSupportedException(RegexLanguage);
                    }
                    return "(?=";
                case RegexToken.PositiveLookaheadAssertionClose:
                    if (!IsPositiveLookaheadAssertionSupported)
                    {
                        throw new PositiveLookaheadAssertionNotSupportedException(RegexLanguage);
                    }
                    return ")";
                case RegexToken.NegativeLookaheadAssertionOpen:
                    if (!IsNegativeLookaheadAssertionSupported)
                    {
                        throw new NegativeLookaheadAssertionNotSupportedException(RegexLanguage);
                    }
                    return "(?!";
                case RegexToken.NegativeLookaheadAssertionClose:
                    if (!IsNegativeLookaheadAssertionSupported)
                    {
                        throw new NegativeLookaheadAssertionNotSupportedException(RegexLanguage);
                    }
                    return ")";
                case RegexToken.PositiveLookbehindAssertionOpen:
                    if (!IsPositiveLookbehindAssertionSupported)
                    {
                        throw new PositiveLookbehindAssertionNotSupportedException(RegexLanguage);
                    }
                    return "(?<=";
                case RegexToken.PositiveLookbehindAssertionClose:
                    if (!IsPositiveLookbehindAssertionSupported)
                    {
                        throw new PositiveLookbehindAssertionNotSupportedException(RegexLanguage);
                    }
                    return ")";
                case RegexToken.NegativeLookbehindAssertionOpen:
                    if (!IsNegativeLookbehindAssertionSupported)
                    {
                        throw new NegativeLookbehindAssertionNotSupportedException(RegexLanguage);
                    }
                    return "(?<!";
                case RegexToken.NegativeLookbehindAssertionClose:
                    if (!IsNegativeLookbehindAssertionSupported)
                    {
                        throw new NegativeLookbehindAssertionNotSupportedException(RegexLanguage);
                    }
                    return ")";
                case RegexToken.CharacterGroupOpen:
                    return "[";
                case RegexToken.CharacterGroupNegative:
                    return "^";
                case RegexToken.CharacterGroupClose:
                    return "]";
                case RegexToken.Alternation:
                    return "|";
                case RegexToken.ZeroOrOne:
                    return "?";
                case RegexToken.ZeroOrMore:
                    return "*";
                case RegexToken.OneOrMore:
                    return "+";
                case RegexToken.Lazy:
                    return "?";
                case RegexToken.Possessive:
                    return "+";
                case RegexToken.QuantifierOpen:
                    return "{";
                case RegexToken.QuantifierSeparator:
                    return ",";
                case RegexToken.QuantifierClose:
                    return "}";
                case RegexToken.StringStartAnchor:
                    return "^";
                case RegexToken.StringEndAnchor:
                    return "$";
                case RegexToken.UnicodeCategoryOpen:
                    if (!IsUnicodeCategorySupported)
                    {
                        throw new UnicodeCategoryNotSupportedException(RegexLanguage);
                    }
                    return @"\p{";
                case RegexToken.UnicodeCategoryClose:
                    if (!IsUnicodeCategorySupported)
                    {
                        throw new UnicodeCategoryNotSupportedException(RegexLanguage);
                    }
                    return "}";
                case RegexToken.NegativeUnicodeCategoryOpen:
                    if (!IsUnicodeCategorySupported)
                    {
                        throw new UnicodeCategoryNotSupportedException(RegexLanguage);
                    }
                    return @"\P{";
                case RegexToken.NegativeUnicodeCategoryClose:
                    if (!IsUnicodeCategorySupported)
                    {
                        throw new UnicodeCategoryNotSupportedException(RegexLanguage);
                    }
                    return "}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(token), "If you think the token enum is not enough, please create a new issue or submit a pull request (PR).");
            }
        }

        public virtual string ToNodeString(RegexNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return node.IsInnerNodeIncluded ?
                AddQuantifier(ToString(node.InnerNode), node.Minimum, node.Maximum, node.RegexQuantifierOption) :
                AddQuantifier(node.Pattern            , node.Minimum, node.Maximum, node.RegexQuantifierOption);
        }

        public virtual string ToGroupString(RegexGroupNode group)
        {
            if (!string.IsNullOrWhiteSpace(group.Name) && !IsNamedCapturingGroupSupported)
            {
                throw new NamedCapturingGroupNotSupportedException(RegexLanguage);
            }
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.GroupOpen));
            if (!string.IsNullOrWhiteSpace(group.Name))
            {
                sb.Append(ToTokenString(RegexToken.GroupOptionStart))
                    .Append(ToTokenString(RegexToken.GroupNameOpen))
                    .Append(group.Name)
                    .Append(ToTokenString(RegexToken.GroupNameClose));
            }
            else if (!string.IsNullOrWhiteSpace(group.Options))
            {
                sb.Append(ToTokenString(RegexToken.GroupOptionStart))
                    .Append(group.Options)
                    .Append(ToTokenString(RegexToken.GroupOptionEnd));
            }
            else if (!group.IsCapturingGroup)
            {
                sb.Append(ToTokenString(RegexToken.GroupNonCapturing));
            }
            sb.Append(group.IsInnerNodeIncluded ? ToString(group.InnerNode) : group.Pattern)
                .Append(ToTokenString(RegexToken.GroupClose));

            return AddQuantifier(sb.ToString(), group.Minimum, group.Maximum, group.RegexQuantifierOption);
        }

        public virtual string ToAtomicGroupString(RegexAtomicGroupNode atomicGroup)
        {
            if (!IsAtomicGroupSupported)
            {
                throw new AtomicGroupNotSupportedException(RegexLanguage);
            }
            if (atomicGroup == null)
            {
                throw new ArgumentNullException(nameof(atomicGroup));
            }

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.AtomicGroupOpen))
                .Append(atomicGroup.IsInnerNodeIncluded ? ToString(atomicGroup.InnerNode) : atomicGroup.Pattern)
                .Append(ToTokenString(RegexToken.AtomicGroupClose));

            return AddQuantifier(sb.ToString(), atomicGroup.Minimum, atomicGroup.Maximum, atomicGroup.RegexQuantifierOption);
        }

        public virtual string ToPositiveLookaheadAssertionString(RegexPositiveLookaheadAssertionNode positiveLookaheadAssertion)
        {
            if (!IsPositiveLookaheadAssertionSupported)
            {
                throw new PositiveLookaheadAssertionNotSupportedException(RegexLanguage);
            }
            if (positiveLookaheadAssertion == null)
            {
                throw new ArgumentNullException(nameof(positiveLookaheadAssertion));
            }

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.PositiveLookaheadAssertionOpen))
                .Append(positiveLookaheadAssertion.IsInnerNodeIncluded ? ToString(positiveLookaheadAssertion.InnerNode) : positiveLookaheadAssertion.Pattern)
                .Append(ToTokenString(RegexToken.PositiveLookaheadAssertionClose));

            return AddQuantifier(sb.ToString(), positiveLookaheadAssertion.Minimum, positiveLookaheadAssertion.Maximum, positiveLookaheadAssertion.RegexQuantifierOption);
        }

        public virtual string ToNegativeLookaheadAssertionString(RegexNegativeLookaheadAssertionNode negativeLookaheadAssertion)
        {
            if (!IsNegativeLookaheadAssertionSupported)
            {
                throw new NegativeLookaheadAssertionNotSupportedException(RegexLanguage);
            }
            if (negativeLookaheadAssertion == null)
            {
                throw new ArgumentNullException(nameof(negativeLookaheadAssertion));
            }

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.NegativeLookaheadAssertionOpen))
                .Append(negativeLookaheadAssertion.IsInnerNodeIncluded ? ToString(negativeLookaheadAssertion.InnerNode) : negativeLookaheadAssertion.Pattern)
                .Append(ToTokenString(RegexToken.NegativeLookaheadAssertionClose));

            return AddQuantifier(sb.ToString(), negativeLookaheadAssertion.Minimum, negativeLookaheadAssertion.Maximum, negativeLookaheadAssertion.RegexQuantifierOption);
        }

        public virtual string ToPositiveLookbehindAssertionString(RegexPositiveLookbehindAssertionNode positiveLookbehindAssertion)
        {
            if (!IsPositiveLookbehindAssertionSupported)
            {
                throw new PositiveLookbehindAssertionNotSupportedException(RegexLanguage);
            }
            if (positiveLookbehindAssertion == null)
            {
                throw new ArgumentNullException(nameof(positiveLookbehindAssertion));
            }

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.PositiveLookbehindAssertionOpen))
                .Append(positiveLookbehindAssertion.IsInnerNodeIncluded ? ToString(positiveLookbehindAssertion.InnerNode) : positiveLookbehindAssertion.Pattern)
                .Append(ToTokenString(RegexToken.PositiveLookbehindAssertionClose));

            return AddQuantifier(sb.ToString(), positiveLookbehindAssertion.Minimum, positiveLookbehindAssertion.Maximum, positiveLookbehindAssertion.RegexQuantifierOption);
        }

        public virtual string ToNegativeLookbehindAssertionString(RegexNegativeLookbehindAssertionNode negativeLookbehindAssertion)
        {
            if (!IsNegativeLookbehindAssertionSupported)
            {
                throw new NegativeLookbehindAssertionNotSupportedException(RegexLanguage);
            }
            if (negativeLookbehindAssertion == null)
            {
                throw new ArgumentNullException(nameof(negativeLookbehindAssertion));
            }

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.NegativeLookbehindAssertionOpen))
                .Append(negativeLookbehindAssertion.IsInnerNodeIncluded ? ToString(negativeLookbehindAssertion.InnerNode) : negativeLookbehindAssertion.Pattern)
                .Append(ToTokenString(RegexToken.NegativeLookbehindAssertionClose));

            return AddQuantifier(sb.ToString(), negativeLookbehindAssertion.Minimum, negativeLookbehindAssertion.Maximum, negativeLookbehindAssertion.RegexQuantifierOption);
        }

        public virtual string ToConditionalString(RegexConditionalNode conditionalNode)
        {
            if (!IsConditionalSupported)
            {
                throw new ConditionalNotSupportedException(RegexLanguage);
            }
            if (conditionalNode == null)
            {
                throw new ArgumentNullException(nameof(conditionalNode));
            }

            var sb = new StringBuilder();
            
            sb.Append(ToTokenString(RegexToken.ConditionalOpen));
            if (string.IsNullOrWhiteSpace(conditionalNode.NameOrNumber))
            {
                sb.Append(ToTokenString(RegexToken.PositiveLookaheadAssertionOpen))
                    .Append(conditionalNode.IsInnerNodeIncluded ? conditionalNode.InnerNode.ToString() : conditionalNode.Pattern)
                    .Append(ToTokenString(RegexToken.PositiveLookaheadAssertionClose));
            }
            else
            {
                sb.Append(ToTokenString(RegexToken.GroupOpen))
                    .Append(conditionalNode.NameOrNumber)
                    .Append(ToTokenString(RegexToken.GroupClose));
            }
            sb.Append(conditionalNode.TrueValue)
                .Append(ToTokenString(RegexToken.Alternation))
                .Append(conditionalNode.FalseValue)
                .Append(ToTokenString(RegexToken.ConditionalClose));

            return AddQuantifier(sb.ToString(), conditionalNode.Minimum, conditionalNode.Maximum, conditionalNode.RegexQuantifierOption);
        }

        public virtual string ToUnicodeCategoryString(RegexUnicodeCategoryNode unicodeCategoryNode)
        {
            if (!IsUnicodeCategorySupported)
            {
                throw new UnicodeCategoryNotSupportedException(RegexLanguage);
            }
            if (unicodeCategoryNode == null)
            {
                throw new ArgumentNullException(nameof(unicodeCategoryNode));
            }

            var sb = new StringBuilder();

            if (!unicodeCategoryNode.Negative)
            {
                sb.Append(ToTokenString(RegexToken.UnicodeCategoryOpen))
                    .Append(unicodeCategoryNode.Pattern)
                    .Append(ToTokenString(RegexToken.UnicodeCategoryClose));
            }
            else
            {
                sb.Append(ToTokenString(RegexToken.NegativeUnicodeCategoryOpen))
                    .Append(unicodeCategoryNode.Pattern)
                    .Append(ToTokenString(RegexToken.NegativeUnicodeCategoryClose));
            }

            return AddQuantifier(sb.ToString(), unicodeCategoryNode.Minimum, unicodeCategoryNode.Maximum, unicodeCategoryNode.RegexQuantifierOption);
        }
    }
}
