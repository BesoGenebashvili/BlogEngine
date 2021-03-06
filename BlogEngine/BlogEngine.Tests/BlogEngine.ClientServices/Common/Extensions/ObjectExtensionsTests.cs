﻿using BlogEngine.ClientServices.Common.Extensions;
using NUnit.Framework;

namespace BlogEngine.Tests.BlogEngine.ClientServices.Common.Extensions
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [Test]
        public void ToQueryString_ObjectIsNull_ThrowArgumentNullException()
        {
            Assert.That(() => ObjectExtensions.ToQueryString(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void ToQueryString_WhenCalled_ReturnsQueryString()
        {
            var obj = new { Bar = "Bar", Foo = "Foo" };

            string result = ObjectExtensions.ToQueryString(obj);

            Assert.That(result, Is.EqualTo("Bar=Bar&Foo=Foo").IgnoreCase);
        }
    }
}