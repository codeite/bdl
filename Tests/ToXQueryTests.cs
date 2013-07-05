using FluentAssertions;
using NUnit.Framework;

namespace bdl.Tests
{
    public class ToXQueryTests
    {
        [TestCase("1", "$id = 1")]
        [TestCase("1.2", "$id = 1 or $id = 2")]
        [TestCase("1+2", "$id = 1 and $id = 2")]
        [TestCase("1+2+3", "$id = 1 and $id = 2 and $id = 3")]
        [TestCase("(1.2)+3", "($id = 1 or $id = 2) and $id = 3")]
        [TestCase("1.(2+3)", "$id = 1 or ($id = 2 and $id = 3)")]
        public void CanconvertToXQuery(string queryText, string expectedXQuery)
        {
            // Arrange
            var parser = new BdlParser(queryText);

            // Act
            var xQuery = parser.Query.ConvertToXQuery("$id");

            // Assert
            xQuery.Should().Be(expectedXQuery);
        }
    }
}