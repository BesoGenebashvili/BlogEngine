using NUnit.Framework;
using BlogEngine.Core.Extensions;

namespace BlogEngine.Tests.BlogEngine.Core.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void StripHtmlTagsWithRegex_NullOrWhiteSpaceRawHtmlContent_ThrowArgumentNullException(string rawHtmlContent)
        {
            Assert.That(() => StringExtensions.StripHtmlTagsWithRegex(rawHtmlContent),
                Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("<h1>Bar</h1>", "Bar")]
        [TestCase("<br> <h1>Foo</h1>", " Foo")]
        [TestCase("<p>Baz</p> <h1>Foo</h1>", "Baz Foo")]
        public void StripHtmlTagsWithRegex_ValidRawHtmlContent_StripsHtmlTagsWithRegex(string rawHtmlContent, string expectedResult)
        {
            var result = StringExtensions.StripHtmlTagsWithRegex(rawHtmlContent);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void StripHtmlTagsWithCharArray_NullOrWhiteSpaceRawHtmlContent_ThrowArgumentNullException(string rawHtmlContent)
        {
            Assert.That(() => StringExtensions.StripHtmlTagsWithCharArray(rawHtmlContent),
                Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("<h1>Bar</h1>", "Bar")]
        [TestCase("<br> <h1>Foo</h1>", " Foo")]
        [TestCase("<p>Baz</p> <h1>Foo</h1>", "Baz Foo")]
        public void StripHtmlTagsWithCharArray_ValidRawHtmlContent_StripsHtmlTagsWithCharArray(string rawHtmlContent, string expectedResult)
        {
            var result = StringExtensions.StripHtmlTagsWithCharArray(rawHtmlContent);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}