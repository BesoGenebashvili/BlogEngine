using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using BlogEngine.Server.Common.Extensions;

namespace BlogEngine.Tests.BlogEngine.Server.Common.Extensions
{
    [TestFixture]
    public class ObjectResultExtensionsTests
    {
        [Test]
        public void IsSuccessfulResponse_ObjectResultIsNull_ReturnFalse()
        {
            var result = ObjectResultExtensions.IsSuccessfulResponse(null);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsSuccessfulResponse_ObjectResulValuetIsNull_ReturnFalse()
        {
            var objectResult = new ObjectResult(null);

            var result = objectResult.IsSuccessfulResponse();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsSuccessfulResponse_ObjectResultStatusCodeNotStartsWith2_ReturnFalse()
        {
            var objectResult = new ObjectResult(new object());
            objectResult.StatusCode = StatusCodes.Status400BadRequest;

            var result = objectResult.IsSuccessfulResponse();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsSuccessfulResponse_ObjectResultStatusCodeHasNotValue_ReturnTrue()
        {
            var objectResult = new ObjectResult(new object());

            var result = objectResult.IsSuccessfulResponse();

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsSuccessfulResponse_ObjectResultStatusCodeStartsWith2_ReturnTrue()
        {
            var objectResult = new ObjectResult(new object());
            objectResult.StatusCode = StatusCodes.Status200OK;

            var result = objectResult.IsSuccessfulResponse();

            Assert.That(result, Is.EqualTo(true));
        }
    }
}