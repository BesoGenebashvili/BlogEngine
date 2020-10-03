using System;
using System.Text;
using BlogEngine.Core.Services.Implementations;
using NUnit.Framework;

namespace BlogEngine.Tests.BlogEngine.Core.Services
{
    [TestFixture]
    public class ReadingTimeEstimatorTests
    {
        private ReadingTimeEstimator _readingTimeEstimator;

        [SetUp]
        public void SetUp()
        {
            _readingTimeEstimator = new ReadingTimeEstimator();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GetEstimatedReadingTime_InvalidrawHtmlContent_ThrowArgumentNullException(string rawHtmlContent)
        {
            Assert.That(() => _readingTimeEstimator.GetEstimatedReadingTime(rawHtmlContent),
                Throws.ArgumentNullException);
        }

        [Test]
        public void GetEstimatedReadingTime_WhenCalled_ReturnEstimatedReadingTime()
        {
            string[] words = { "Foo ", "Bar ", "Baz " };
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                stringBuilder.Append(words[random.Next(0, 2)]);
            }

            int result = _readingTimeEstimator.GetEstimatedReadingTime(stringBuilder.ToString());

            Assert.That(result, Is.EqualTo(5));
        }
    }
}