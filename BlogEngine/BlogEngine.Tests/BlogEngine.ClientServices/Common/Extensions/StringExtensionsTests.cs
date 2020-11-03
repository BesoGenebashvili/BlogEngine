using NUnit.Framework;
using BlogEngine.ClientServices.Common.Extensions;

namespace BlogEngine.Tests.BlogEngine.ClientServices.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        [TestCase(null, default, null)]
        [TestCase("", default, "")]
        [TestCase("", 20, "")]
        [TestCase("Bar", 20, "Bar")]
        [TestCase("Bar", -20, "Bar")]
        [TestCase("FooBarBaz", 6, "Foo...")]
        public void GetStringBrief_WhenCalled_ReturnStringBrief(string s, int count, string expectedResult)
        {
            var result = StringExtensions.GetStringBrief(s, count);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}