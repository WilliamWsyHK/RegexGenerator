using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace WilliamWsy.RegexGenerator.Test
{
    public class DotNetRegexGeneratorMatchTest
    {
        [Fact]
        public void SimplePatternMatchTest()
        {
            // Arrange
            var text = "5";
            var regex = new Regex(@"\d");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .Add(@"\d");

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
        }

        [Fact]
        public void SimpleNonCapturingGroupMatchTest()
        {
            // Arrange
            var text = "abcde";
            var regex = new Regex(@"(?:abcde)");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddGroup(@"abcde", false);

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
        }

        [Fact]
        public void SimpleCapturingGroupWithoutNameMatchTest()
        {
            // Arrange
            var text = "abcde";
            var regex = new Regex(@"(abcde)");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddGroup(@"abcde", true);

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
        }

        [Fact]
        public void SimpleCapturingGroupWithAngleBracketedNameMatchTest()
        {
            // Arrange
            var text = "abcde";
            var regex = new Regex(@"(?<word>abcde)");
            var expectedMatch = regex.Match(text);
            var expectedGroup = expectedMatch.Groups["word"];

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddGroup(@"abcde", name: "word");

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);
            var actualGroup = actualMatch.Groups["word"];

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedGroup.Value, actualGroup.Value);
        }

        [Fact]
        public void SimpleCapturingGroupWithApostrophedNameMatchTest()
        {
            // Arrange
            var text = "abcde";
            var regex = new Regex(@"(?'word'abcde)");
            var expectedMatch = regex.Match(text);
            var expectedGroup = expectedMatch.Groups["word"];

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddGroup(@"abcde", name: "word");
            // Important!
            (regexGenerator.RegexLanguageStrategy.Stringifier as DotNetRegexStringifier).NamedCaptureGroupBracketOption = NamedCaptureGroupBracketOption.Apostrophe;

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);
            var actualGroup = actualMatch.Groups["word"];

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedGroup.Value, actualGroup.Value);
        }

        [Fact]
        public void CombinedGroupsMatchTest()
        {
            // Arrange
            var text = "asd13579thIs";
            var regex = new Regex(@"(?:[a-z]+?)(?<odd>13579)(?i:THIS)(\d+)?");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddGroup("[a-z]+?", false)
                .AddGroup("13579", true, "odd")
                .AddGroup("THIS", false, options: "i") // capturing group option does not matter.
                .AddGroup(@"\d+", true, min: 0, max: 1);

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
            Assert.Equal(expectedMatch.Groups.Count, actualMatch.Groups.Count);
            for (var i = 0; i < expectedMatch.Groups.Count; i++)
            {
                Assert.Equal(expectedMatch.Groups[i].Name, actualMatch.Groups[i].Name);
                Assert.Equal(expectedMatch.Groups[i].Value, actualMatch.Groups[i].Value);
            }
        }

        [Fact]
        public void SimpleAtomicGroupMatchTest()
        {
            // Arrange
            var text = "aaad";
            var regex = new Regex(@"(?>(\w)\1+).\b");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddAtomicGroup(@"(\w)\1+")
                .Add(@".\b");

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
        }

        [Fact]
        public void SimpleConditionalMatchTrueValueTest()
        {
            // Arrange
            var text = "yess";
            var regex = new Regex(@"(?(?=yes)yess|no)");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddConditional("yes", "yess", "no");

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
        }

        [Fact]
        public void SimpleConditionalMatchFalseValueTest()
        {
            // Arrange
            var text = "no";
            var regex = new Regex(@"(?(?=yes)yess|no)");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddConditional("yes", "yess", "no");

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
        }

        [Fact]
        public void SimplePositiveLookaheadMatchTest()
        {
            // Arrange
            var text = "The dog is a Malamute.";
            var regex = new Regex(@"\b\w+(?=\sis\b)");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .Add(@"\b\w+")
                .AddPositiveLookaheadAssertion(@"\sis\b");

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
        }

        [Fact]
        public void SimpleNegativeLookaheadMatchTest()
        {
            // Arrange
            var text = "unite one";
            var regex = new Regex(@"\b(?!un)\w+\b");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .Add(@"\b")
                .AddNegativeLookaheadAssertion(@"un")
                .Add(@"\w+\b");

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
        }

        [Fact]
        public void SimplePositiveLookbehindMatchTest()
        {
            // Arrange
            var text = "1999 2010";
            var regex = new Regex(@"(?<=\b20)\d{2}\b");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddPositiveLookbehindAssertion(@"\b20")
                .Add(@"\d", min: 2, max: 2)
                .Add(@"\b");

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
        }

        [Fact]
        public void SimpleNegativeLookbehindMatchTest()
        {
            // Arrange
            var text = "Monday February 1, 2010";
            var regex = new Regex(@"(?<!(Saturday|Sunday) )\b\w+ \d{1,2}, \d{4}\b");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddNegativeLookbehindAssertion(@"(Saturday|Sunday) ")
                .Add(@"\b\w+ \d{1,2}, \d{4}\b");

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
        }

        [Fact]
        public void SimpleUnicodeCategoryByFlagMatchTest()
        {
            // Arrange
            var text = "中文";
            var regex = new Regex(@"\p{Lo}+");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddUnicodeCategory(RegexUnicodeCategoryFlag.Lo, min: 1);

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
        }

        [Fact]
        public void UnicodeCategoryByFlagMatchesTest()
        {
            var textDict = new Dictionary<RegexUnicodeCategoryFlag, string>()
            {
                { RegexUnicodeCategoryFlag.Lu, "A" }, // U+0041
                { RegexUnicodeCategoryFlag.Ll, "a" }, // U+0061
                { RegexUnicodeCategoryFlag.Lt, "ǅ" }, // U+01C5
                //{ RegexUnicodeCategoryFlag.LC, "Aaǅ" },
                { RegexUnicodeCategoryFlag.Lm, "ʰ" }, // U+02B0
                { RegexUnicodeCategoryFlag.Lo, "ƻ" }, // U+01BB
                //{ RegexUnicodeCategoryFlag.L, "Aaǅʰƻ" }.
                { RegexUnicodeCategoryFlag.Mn, "\u033A" }, // U+033A
                { RegexUnicodeCategoryFlag.Mc, "\u0903" }, // U+0903
                { RegexUnicodeCategoryFlag.Me, "\u20E0" }, // U+20E0
                //{ RegexUnicodeCategoryFlag.M, "\u033A\U0001D16F\u20E0" },
                { RegexUnicodeCategoryFlag.Nd, "0" }, // U+0030
                { RegexUnicodeCategoryFlag.Nl, "〡" }, // U+3021
                { RegexUnicodeCategoryFlag.No, "¼" }, // U+00BC
                //{ RegexUnicodeCategoryFlag.N, "0〡¼" },
                { RegexUnicodeCategoryFlag.Pc, "_" }, // U+005F
                { RegexUnicodeCategoryFlag.Pd, "-" }, // U+002D
                { RegexUnicodeCategoryFlag.Ps, "(" }, // U+0028
                { RegexUnicodeCategoryFlag.Pe, ")" }, // U+0029
                { RegexUnicodeCategoryFlag.Pi, "«" }, // U+00AB
                { RegexUnicodeCategoryFlag.Pf, "»" }, // U+00BB
                { RegexUnicodeCategoryFlag.Po, "!" }, // U+0021
                //{ RegexUnicodeCategoryFlag.P, "_-()«»!" },
                { RegexUnicodeCategoryFlag.Sm, "+" }, // U+002B
                { RegexUnicodeCategoryFlag.Sc, "$" }, // U+0024
                { RegexUnicodeCategoryFlag.Sk, "^" }, // U+005E
                { RegexUnicodeCategoryFlag.So, "⚡" }, // U+26A1
                //{ RegexUnicodeCategoryFlag.S, "+$^⚡" },
                { RegexUnicodeCategoryFlag.Zs, " " }, // U+0020
                { RegexUnicodeCategoryFlag.Zl, "\u2028" }, // U+2028
                { RegexUnicodeCategoryFlag.Zp, "\u2029" }, // U+2029
                //{ RegexUnicodeCategoryFlag.Z, " \u2028\u2029" },
                { RegexUnicodeCategoryFlag.Cc, "\u0000" }, // U+0000
                { RegexUnicodeCategoryFlag.Cf, "\u00AD" }, // U+00AD
                { RegexUnicodeCategoryFlag.Cs, "\uD800" }, // U+D800
                { RegexUnicodeCategoryFlag.Co, "\uE000" }, // U+E000
                //{ RegexUnicodeCategoryFlag.Cn, "" }, // nothing
                //{ RegexUnicodeCategoryFlag.C, "\u0000\u00AD\uD800\uE000" }
            };

            for (RegexUnicodeCategoryFlag flag = RegexUnicodeCategoryFlag.Lu; flag < RegexUnicodeCategoryFlag.Cn; flag = (RegexUnicodeCategoryFlag)((int)flag << 1))
            {
                // Arrange
                var text = textDict[flag];
                var regex = new Regex($@"\p{{{flag}}}+");
                var expectedMatch = regex.Match(text);

                var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                    .AddUnicodeCategory(flag, min: 1);

                // Act
                var generatedRegex = regexGenerator.Create();
                var actualMatch = generatedRegex.Match(text);

                // Assert
                Assert.True(expectedMatch.Success);
                Assert.True(actualMatch.Success);
                Assert.Equal(expectedMatch.Value, actualMatch.Value);
            }
        }

        [Fact]
        public void SimpleUnicodeCategoryByStringMatchTest()
        {
            // Arrange
            var text = "中文";
            var regex = new Regex(@"\p{Lo}+");
            var expectedMatch = regex.Match(text);

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddUnicodeCategory("Lo", min: 1);

            // Act
            var generatedRegex = regexGenerator.Create();
            var actualMatch = generatedRegex.Match(text);

            // Assert
            Assert.True(expectedMatch.Success);
            Assert.True(actualMatch.Success);
            Assert.Equal(expectedMatch.Value, actualMatch.Value);
        }
    }
}
