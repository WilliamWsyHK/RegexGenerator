using System;
using System.Text.RegularExpressions;
using Xunit;

namespace WilliamWsy.RegexGenerator.Test
{
    public class RegexGeneratorFunctionalityTest
    {
        [Fact]
        public void AddNodeTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.Add(@"a");

            // Assert
            Assert.Equal(@"a", regexGenerator.ToString());
        }

        [Fact]
        public void AddCapturingGroupTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddGroup(@"a");

            // Assert
            Assert.Equal(@"(a)", regexGenerator.ToString());
        }

        [Fact]
        public void AddNamedCapturingGroupTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddGroup(@"a", name: "a");

            // Assert
            Assert.Equal(@"(?<a>a)", regexGenerator.ToString());
        }

        [Fact]
        public void AddNonCapturingGroupTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddGroup(@"a", capturing: false);

            // Assert
            Assert.Equal(@"(?:a)", regexGenerator.ToString());
        }

        [Fact]
        public void AddAtomicGroupTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddAtomicGroup(@"a");

            // Assert
            Assert.Equal(@"(?>a)", regexGenerator.ToString());
        }

        [Fact]
        public void AddConditionalTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddConditional(@"a", "b", "c");

            // Assert
            Assert.Equal(@"(?(?=a)b|c)", regexGenerator.ToString());
        }

        [Fact]
        public void AddPositiveLookaheadAssertionTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddPositiveLookaheadAssertion(@"a");

            // Assert
            Assert.Equal(@"(?=a)", regexGenerator.ToString());
        }

        [Fact]
        public void AddNegativeLookaheadAssertionTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddNegativeLookaheadAssertion(@"a");

            // Assert
            Assert.Equal(@"(?!a)", regexGenerator.ToString());
        }

        [Fact]
        public void AddPositiveLookbehindAssertionTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddPositiveLookbehindAssertion(@"a");

            // Assert
            Assert.Equal(@"(?<=a)", regexGenerator.ToString());
        }

        [Fact]
        public void AddNegativeLookbehindAssertionTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddNegativeLookbehindAssertion(@"a");

            // Assert
            Assert.Equal(@"(?<!a)", regexGenerator.ToString());
        }

        [Fact]
        public void AddUnicodeCategoryByFlagTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddUnicodeCategory(RegexUnicodeCategoryFlag.L);

            // Assert
            Assert.Equal(@"\p{L}", regexGenerator.ToString());
        }

        [Fact]
        public void AddUnicodeCategoryByStringTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddUnicodeCategory("L");

            // Assert
            Assert.Equal(@"\p{L}", regexGenerator.ToString());
        }

        [Theory]
        [InlineData(RegexQuantifierOption.Greedy)]
        [InlineData(RegexQuantifierOption.Lazy)]
        [InlineData(RegexQuantifierOption.Possessive)]
        public void AddNodeQuantifierTest(RegexQuantifierOption quantifierOption)
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.Add(@"a", min: 1, max: 10, quantifierOption: quantifierOption);

            // Assert
            Assert.Equal($@"a{{1,10}}{regexGenerator.RegexLanguageStrategy.Stringifier.ToQuantifierOptionString(quantifierOption)}", regexGenerator.ToString());
        }

        [Fact]
        public void SimpleAddNestedNodeTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();
            var node = new RegexNode(@"a");

            // Act
            regexGenerator.AddGroup(node);

            // Assert
            Assert.Equal(@"(a)", regexGenerator.ToString());
        }

        [Fact]
        public void CompositeAddNestedNodeTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();
            var node1 = new RegexNode(@"[abc]");
            var nested1 = new RegexGroupNode(node1, capturing: false, min: 1, max: null, quantifierOption: RegexQuantifierOption.Lazy);
            var node2 = new RegexConditionalNode(@"a", "b", "c");
            var nested2 = new RegexGroupNode(node2, capturing: true, name: "a");

            // Act
            regexGenerator.AddNegativeLookbehindAssertion(nested1)
                .AddGroup(nested2);

            // Assert
            Assert.Equal(@"(?<!(?:[abc])+?)(?<a>(?(?=a)b|c))", regexGenerator.ToString());
        }
    }
}
