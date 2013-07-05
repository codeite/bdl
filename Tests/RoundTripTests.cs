using FluentAssertions;
using NUnit.Framework;

namespace bdl.Tests
{
    [TestFixture]
    public class RoundTripTests
    {
        [TestCase("1")]
        [TestCase("(1)")]
        [TestCase("((1))")]
        [TestCase("1+2")]
        [TestCase("1.2")]
        [TestCase("1+2.3")]
        [TestCase("1.2+3")]
        [TestCase("1+2+3")]
        [TestCase("1.2.3")]
        [TestCase("1+2.3+4")]
        [TestCase("(1+2).(3+4)")]
        [TestCase("1.2+3.4")]
        [TestCase("1.(2+3).4")]
        [TestCase("1.2.3+4")]
        [TestCase("1.2.(3+4)")]
        [TestCase("1.(2.(3+4))")]
        [TestCase("1+2+3.4")]
        [TestCase("(1+2+3).4")]
        [TestCase("((1+2)+3.4)")]
        [TestCase("((1+2)+3).4")]
        [TestCase("1+2.3.4+5")]
        [TestCase("(1+2).3.(4+5)")]
        public void TestRoundTrip(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);
            var asString = parser.Query.ConvertToString();

            // Assert
            asString.Should().Be(queryText);
        }
    }
}