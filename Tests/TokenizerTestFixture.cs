using FluentAssertions;
using NUnit.Framework;

namespace bdl.Tests
{
    [TestFixture]
    public class TokenizerTestFixture
    {
        [TestCase("10", new[] { "10" })]
        [TestCase("(10)", new[] { "(", "10", ")" })]
        [TestCase("50+10", new[] { "50", "+", "10" })]
        [TestCase("50a10", new[] { "50", "a", "10" })]
        [TestCase("500and100", new[] { "500", "and", "100" })]
        [TestCase("(50+10).60", new[] { "(", "50", "+", "10", ")", ".", "60" })]
        public void TestTokenizer(string query, string[] expectedTokens)
        {
            // Arrange
            var tokenizer = new BdlLexicalScanner();

            // Act
            var tokens = tokenizer.Tokenize(query);

            // Assert
            tokens.Should().Equal(expectedTokens);
        }
    }
}