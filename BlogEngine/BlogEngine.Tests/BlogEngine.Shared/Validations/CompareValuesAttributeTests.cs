using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using BlogEngine.Shared.Validations;

namespace BlogEngine.Tests.BlogEngine.Shared.Validations
{
    #region Mock

    public class TestClass
    {
        [CompareValues(nameof(ConfirmProperty))]
        public string PropertyToValidate { get; set; }

        public string ConfirmProperty { get; set; }
    }

    #endregion

    [TestFixture]
    public class CompareValuesAttributeTests
    {
        [Test]
        [TestCase("", "foo", false)]
        [TestCase("Bar", "bar", false)]
        [TestCase("Bar", "foo", false)]
        [TestCase("Bar", "Bar", true)]
        public void IsValid_WhenCalled_ReturnValidationResult(string propertyToValidate, string confirmProperty, bool expectedResult)
        {
            var testClass = new TestClass()
            {
                PropertyToValidate = propertyToValidate,
                ConfirmProperty = confirmProperty,
            };

            var result = Validator.TryValidateObject(testClass, new ValidationContext(testClass), null, true);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}