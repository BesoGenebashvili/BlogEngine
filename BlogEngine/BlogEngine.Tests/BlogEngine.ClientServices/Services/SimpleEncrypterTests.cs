using NUnit.Framework;
using BlogEngine.ClientServices.Services.Implementations;

namespace BlogEngine.Tests.BlogEngine.ClientServices.Services
{
    [TestFixture]
    public class SimpleEncrypterTests
    {
        private SimpleEncrypter _simpleEncrypter;

        [SetUp]
        public void SetUp()
        {
            _simpleEncrypter = new SimpleEncrypter();
        }

        [Test]
        public void Encrypt_TextIsNull_ThrowArgumentNullException()
        {
            Assert.That(() => _simpleEncrypter.Encrypt(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void EncryptAsync_TextIsNull_ThrowArgumentNullException()
        {
            Assert.That(async () => await _simpleEncrypter.EncryptAsync(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void Decrypt_TextIsNull_ThrowArgumentNullException()
        {
            Assert.That(() => _simpleEncrypter.Decrypt(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void DecryptAsync_TextIsNull_ThrowArgumentNullException()
        {
            Assert.That(async () => await _simpleEncrypter.DecryptAsync(null),
                Throws.ArgumentNullException);
        }
    }
}