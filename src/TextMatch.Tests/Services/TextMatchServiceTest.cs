using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextMatch.Tests.Services
{
    using System.Runtime.CompilerServices;
    using TextMatch.Services;

    [TestClass]
    public class TextMatchServiceTest
    {
        private string _inputText = "Polly put the kettle on, polly put the kettle on, polly put the kettle on we’ll all have tea";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "inputText")]
        public void FindInputNullMatch()
        {
            // Arrange
            var subText = "Polly";
            var service = new TextMatchService();
            var expected = string.Empty;

            // Act
            var actual = service.FindMatches(null, subText);

            // Assert

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "subText")]
        public void FindSubTextNullMatch()
        {
            // Arrange
            var service = new TextMatchService();
            var expected = string.Empty;

            // Act
            var actual = service.FindMatches(_inputText, null);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "subText")]
        public void FindBothNullMatch()
        {
            // Arrange
            var service = new TextMatchService();
            var expected = string.Empty;

            // Act
            var actual = service.FindMatches(null, null);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindInputEmptyMatch()
        {
            // Arrange
            var subText = "Polly";
            var service = new TextMatchService();
            var expected = string.Empty;

            // Act
            var actual = service.FindMatches(string.Empty, subText);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindSubTextEmptyMatch()
        {
            // Arrange
            var service = new TextMatchService();
            var expected = string.Empty;

            // Act
            var actual = service.FindMatches(_inputText, "");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSubTextWordMatch()
        {
            // Arrange
            var subText = "Polly";
            var service = new TextMatchService();
            var expected = "1,26,51";

            // Act
            var actual = service.FindMatches(_inputText, subText);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSubTextSpansWordMatch()
        {
            // Arrange
            var subText = " on, polly put ";
            var service = new TextMatchService();
            var expected = "21,46";

            // Act
            var actual = service.FindMatches(_inputText, subText);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSubTextPartialWordMatch()
        {
            // Arrange
            var subText = "ll";
            var service = new TextMatchService();
            var expected = "3,28,53,78,82";

            // Act
            var actual = service.FindMatches(_inputText, subText);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSubTextNotPresentMatch()
        {
            // Arrange
            var subText = "X";
            var service = new TextMatchService();
            var expected = string.Empty;

            // Act
            var actual = service.FindMatches(_inputText, subText);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestPartialSubTextMatch()
        {
            // Arrange
            var subText = "Polx";
            var service = new TextMatchService();
            var expected = string.Empty;

            // Act
            var actual = service.FindMatches(_inputText, subText);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSubTextOverRunMatch()
        {
            // Arrange
            var subText = "Teal";
            var service = new TextMatchService();
            var expected = string.Empty;

            // Act
            var actual = service.FindMatches(_inputText, subText);

            // Assert
            Assert.AreEqual(expected, actual);
        } 

        [TestMethod]
        public void CharMatchCaseInsenitive()
        {
            // Arrange
            var service = new TextMatchService();

            // Act
            // Assert

            // Test A == B
            Assert.IsFalse(service.CharMatchCaseInsenitive('A', 'B'));
            // Test a == b
            Assert.IsFalse(service.CharMatchCaseInsenitive('a', 'b'));
            // Test a == A
            Assert.IsTrue(service.CharMatchCaseInsenitive('a', 'A'));
            // Test B == B
            Assert.IsTrue(service.CharMatchCaseInsenitive('B', 'B'));
            // Test C == c
            Assert.IsTrue(service.CharMatchCaseInsenitive('C', 'c'));
        } 
    }
}
