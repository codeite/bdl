using FluentAssertions;
using NUnit.Framework;

namespace bdl.Tests
{
    [TestFixture]
    public class TokenizerTestFixture
    {
        [TestCase("10", new[] { "10" })]
        [TestCase("(10)", new[] { "(", "10", ")" })]
        [TestCase("5+10", new[] { "5", "+", "10" })]
        public void TestTokenizer(string query, string[] expectedTokens)
        {
            // Arrange

            // Act
            var tokens = BdlParser.Tokenize(query);

            // Assert
            tokens.Should().Equal(expectedTokens);
        }
    }
}