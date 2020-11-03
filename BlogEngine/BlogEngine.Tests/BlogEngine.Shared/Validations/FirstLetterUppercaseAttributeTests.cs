using System;
using BlogEngine.Shared.Validations;
using NUnit.Framework;

namespace BlogEngine.Tests.BlogEngine.Shared.Validations
{
    [TestFixture]
    public class FirstLetterUppercaseAttributeTests
    {
        [Test]
        [TestCase("", true)]
        [TestCase(" ", true)]
        [TestCase("foo", false)]
        [TestCase("Foo", true)]
        public void IsValid_WhenCalled_ReturnValidationResult(string value, bool expectedResult)
        {
            var firstLetterUppercaseAttribute = new FirstLetterUppercaseAttribute();

            var result = firstLetterUppercaseAttribute.IsValid(value);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void IsValid_NullValue_ThrowNullReferenceException()
        {
            var firstLetterUppercaseAttribute = new FirstLetterUppercaseAttribute();

            Assert.That(() => firstLetterUppercaseAttribute.IsValid(null),
                Throws.Exception.TypeOf<NullReferenceException>());
        }
    }
}