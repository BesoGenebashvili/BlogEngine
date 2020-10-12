using System.Linq;
using NUnit.Framework;
using BlogEngine.Server.Extensions;
using BlogEngine.Shared.DTOs;

namespace BlogEngine.Tests.BlogEngine.Server.Extensions
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
            Assert.That(() => IEnumerableExtensions.Paginate(Enumerable.Range(0, 0), null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void Paginate_WhenCalled_ReturnsPaginatedIenumerable()
        {
            var enumerable = Enumerable.Range(0, 10);
            var paginationDTO = new PaginationDTO(2, 5);

            var result = IEnumerableExtensions.Paginate(enumerable, paginationDTO);

            Assert.That(result, Is.EquivalentTo(new[] { 5, 6, 7, 8, 9 }));
        }
    }
}