using FluentAssertions;
using NUnit.Framework;

namespace bdl.Tests
{
    [TestFixture]
    class EvaluationTests
    {
        [TestCase("1")]
        [TestCase("(1)")]
        [TestCase("((1))")]
        public void SingleValueShouldBeValue(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(true);

            parser.Query.Evaluate(100).Should().Be(false);
            parser.Query.Evaluate().Should().Be(false);
        }

        [TestCase("!1")]
        [TestCase("!(1)")]
        [TestCase("!!!1")]
        public void TestNot(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(false);

            parser.Query.Evaluate(100).Should().Be(true);
            parser.Query.Evaluate().Should().Be(true);
        }

        [TestCase("1+2")]
        public void TestAnd(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(false);
            parser.Query.Evaluate(2).Should().Be(false);
            parser.Query.Evaluate(1, 2).Should().Be(true);

            parser.Query.Evaluate(100).Should().Be(false);
            parser.Query.Evaluate().Should().Be(false);
        }

        [TestCase("!1+!2")]
        [TestCase("!(1.2)")]
        public void TestNotAnd(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(false);
            parser.Query.Evaluate(2).Should().Be(false);
            parser.Query.Evaluate(1, 2).Should().Be(false);

            parser.Query.Evaluate(100).Should().Be(true);
            parser.Query.Evaluate().Should().Be(true);
        }

        [TestCase("1.2")]
        public void TestOr(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(true);
            parser.Query.Evaluate(2).Should().Be(true);
            parser.Query.Evaluate(1, 2).Should().Be(true);

            parser.Query.Evaluate(100).Should().Be(false);
            parser.Query.Evaluate().Should().Be(false);
        }

        [TestCase("1+2.3")]
        public void TestAndThenOr(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(false);
            parser.Query.Evaluate(2).Should().Be(false);
            parser.Query.Evaluate(3).Should().Be(true);

            parser.Query.Evaluate(1, 2).Should().Be(true);
            parser.Query.Evaluate(2, 3).Should().Be(true);
            parser.Query.Evaluate(3, 1).Should().Be(true);

            parser.Query.Evaluate(1, 2, 3).Should().Be(true);

            parser.Query.Evaluate(100).Should().Be(false);
            parser.Query.Evaluate().Should().Be(false);
        }

        [TestCase("1.2+3")]
        public void TestOrThenAnd(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(true);
            parser.Query.Evaluate(2).Should().Be(false);
            parser.Query.Evaluate(3).Should().Be(false);

            parser.Query.Evaluate(1, 2).Should().Be(true);
            parser.Query.Evaluate(2, 3).Should().Be(true);
            parser.Query.Evaluate(3, 1).Should().Be(true);

            parser.Query.Evaluate(1, 2, 3).Should().Be(true);

            parser.Query.Evaluate(100).Should().Be(false);
            parser.Query.Evaluate().Should().Be(false);
        }

        [TestCase("1+2+3")]
        public void TestDoubleAnd(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(false);
            parser.Query.Evaluate(2).Should().Be(false);
            parser.Query.Evaluate(3).Should().Be(false);

            parser.Query.Evaluate(1, 2).Should().Be(false);
            parser.Query.Evaluate(2, 3).Should().Be(false);
            parser.Query.Evaluate(3, 1).Should().Be(false);

            parser.Query.Evaluate(1, 2, 3).Should().Be(true);

            parser.Query.Evaluate(100).Should().Be(false);
            parser.Query.Evaluate().Should().Be(false);
        }

        [TestCase("1.2.3")]
        public void TestDoubleOr(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(true);
            parser.Query.Evaluate(2).Should().Be(true);
            parser.Query.Evaluate(3).Should().Be(true);

            parser.Query.Evaluate(1, 2).Should().Be(true);
            parser.Query.Evaluate(2, 3).Should().Be(true);
            parser.Query.Evaluate(3, 1).Should().Be(true);

            parser.Query.Evaluate(1, 2, 3).Should().Be(true);

            parser.Query.Evaluate(100).Should().Be(false);
            parser.Query.Evaluate().Should().Be(false);
        }

        [TestCase("1+2.3+4")]
        [TestCase("(1+2).(3+4)")]
        public void TestAndOrAnd(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(false);
            parser.Query.Evaluate(2).Should().Be(false);
            parser.Query.Evaluate(3).Should().Be(false);
            parser.Query.Evaluate(4).Should().Be(false);

            parser.Query.Evaluate(1, 2).Should().Be(true);
            parser.Query.Evaluate(2, 3).Should().Be(false);
            parser.Query.Evaluate(3, 4).Should().Be(true);
            parser.Query.Evaluate(4, 1).Should().Be(false);

            parser.Query.Evaluate(1, 2, 3).Should().Be(true);
            parser.Query.Evaluate(2, 3, 4).Should().Be(true);
            parser.Query.Evaluate(3, 4, 1).Should().Be(true);
            parser.Query.Evaluate(4, 1, 2).Should().Be(true);

            parser.Query.Evaluate(1, 2, 3, 4).Should().Be(true);

            parser.Query.Evaluate(100).Should().Be(false);
            parser.Query.Evaluate().Should().Be(false);
        }

        [TestCase("1.2+3.4")]
        [TestCase("1.(2+3).4")]
        public void TestOrAndOr(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(true);
            parser.Query.Evaluate(2).Should().Be(false);
            parser.Query.Evaluate(3).Should().Be(false);
            parser.Query.Evaluate(4).Should().Be(true);

            parser.Query.Evaluate(1, 2).Should().Be(true);
            parser.Query.Evaluate(2, 3).Should().Be(true);
            parser.Query.Evaluate(3, 4).Should().Be(true);
            parser.Query.Evaluate(4, 1).Should().Be(true);

            parser.Query.Evaluate(1, 2, 3).Should().Be(true);
            parser.Query.Evaluate(2, 3, 4).Should().Be(true);
            parser.Query.Evaluate(3, 4, 1).Should().Be(true);
            parser.Query.Evaluate(4, 1, 2).Should().Be(true);

            parser.Query.Evaluate(1, 2, 3, 4).Should().Be(true);

            parser.Query.Evaluate(100).Should().Be(false);
            parser.Query.Evaluate().Should().Be(false);
        }

        [TestCase("1.2.3+4")]
        [TestCase("1.2.(3+4)")]
        [TestCase("1.(2.(3+4))")]
        public void TestOrOrAnd(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(true);
            parser.Query.Evaluate(2).Should().Be(true);
            parser.Query.Evaluate(3).Should().Be(false);
            parser.Query.Evaluate(4).Should().Be(false);

            parser.Query.Evaluate(1, 2).Should().Be(true);
            parser.Query.Evaluate(2, 3).Should().Be(true);
            parser.Query.Evaluate(3, 4).Should().Be(true);
            parser.Query.Evaluate(4, 1).Should().Be(true);

            parser.Query.Evaluate(1, 2, 3).Should().Be(true);
            parser.Query.Evaluate(2, 3, 4).Should().Be(true);
            parser.Query.Evaluate(3, 4, 1).Should().Be(true);
            parser.Query.Evaluate(4, 1, 2).Should().Be(true);

            parser.Query.Evaluate(1, 2, 3, 4).Should().Be(true);

            parser.Query.Evaluate(100).Should().Be(false);
            parser.Query.Evaluate().Should().Be(false);
        }

        [TestCase("1+2+3.4")]
        [TestCase("(1+2+3).4")]
        [TestCase("((1+2)+3.4)")]
        [TestCase("((1+2)+3).4")]
        public void TestAndAndOr(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(false);
            parser.Query.Evaluate(2).Should().Be(false);
            parser.Query.Evaluate(3).Should().Be(false);
            parser.Query.Evaluate(4).Should().Be(true);

            parser.Query.Evaluate(1, 2).Should().Be(false);
            parser.Query.Evaluate(2, 3).Should().Be(false);
            parser.Query.Evaluate(3, 4).Should().Be(true);
            parser.Query.Evaluate(4, 1).Should().Be(true);

            parser.Query.Evaluate(1, 2, 3).Should().Be(true);
            parser.Query.Evaluate(2, 3, 4).Should().Be(true);
            parser.Query.Evaluate(3, 4, 1).Should().Be(true);
            parser.Query.Evaluate(4, 1, 2).Should().Be(true);

            parser.Query.Evaluate(1, 2, 3, 4).Should().Be(true);

            parser.Query.Evaluate(100).Should().Be(false);
            parser.Query.Evaluate().Should().Be(false);
        }

        [TestCase("1+2.3.4+5")]
        [TestCase("(1+2).3.(4+5)")]
        public void TestTrippleOr(string queryText)
        {
            // Arrange

            // Act
            var parser = new BdlParser(queryText);

            // Assert
            parser.Query.Evaluate(1).Should().Be(false);
            parser.Query.Evaluate(2).Should().Be(false);
            parser.Query.Evaluate(3).Should().Be(true);
            parser.Query.Evaluate(4).Should().Be(false);
            parser.Query.Evaluate(5).Should().Be(false);

            parser.Query.Evaluate(1, 2).Should().Be(true);
            parser.Query.Evaluate(1, 3).Should().Be(true);
            parser.Query.Evaluate(1, 4).Should().Be(false);
            parser.Query.Evaluate(1, 5).Should().Be(false);
            parser.Query.Evaluate(2, 3).Should().Be(true);
            parser.Query.Evaluate(2, 4).Should().Be(false);
            parser.Query.Evaluate(2, 5).Should().Be(false);
            parser.Query.Evaluate(3, 4).Should().Be(true);
            parser.Query.Evaluate(3, 5).Should().Be(true);
            parser.Query.Evaluate(4, 5).Should().Be(true);

            parser.Query.Evaluate(100).Should().Be(false);
            parser.Query.Evaluate().Should().Be(false);
        }
    }
}
