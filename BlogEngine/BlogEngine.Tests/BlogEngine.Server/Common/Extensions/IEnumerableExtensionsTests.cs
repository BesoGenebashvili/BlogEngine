using System.Linq;
using NUnit.Framework;
using BlogEngine.Shared.DTOs;
using BlogEngine.Server.Common.Extensions;

namespace BlogEngine.Tests.BlogEngine.Server.Common.Extensions
{
    [TestFixture]
    public class IEnumerableExtensionsTests
    {
        [Test]
        public void Paginate_EnumerableIsNull_ThrowArgumentNullException()
        {
            Assert.That(() => IEnumerableExtensions.Paginate<object>(null, new PaginationDTO()),
                Throws.ArgumentNullException);
        }

        [Test]
        public void Paginate_PaginationDTOIsNull_ThrowArgumentNullException()
        {
            Assert.That(() => Enumerable.Range(0, 0).Paginate(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void Paginate_WhenCalled_ReturnsPaginatedIenumerable()
        {
            var enumerable = Enumerable.Range(0, 10);
            var paginationDTO = new PaginationDTO(2, 5);

            var result = enumerable.Paginate(paginationDTO);

            Assert.That(result, Is.EquivalentTo(new[] { 5, 6, 7, 8, 9 }));
        }
    }
}