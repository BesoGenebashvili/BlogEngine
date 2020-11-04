using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using BlogEngine.Core.Services.Implementations;
using Moq;
using BlogEngine.Core.Common.Exceptions;
using BlogEngine.Core.Data.Entities.Common;

namespace BlogEngine.Tests.BlogEngine.Core.Services
{
    [TestFixture]
    public partial class RepositoryTests
    {
        private Mock<DbContext> _context;
        private Mock<DbSet<BaseEntity>> _dbSet;
        private BaseEntity _baseEntity;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<DbContext>();
            _dbSet = new Mock<DbSet<BaseEntity>>();
            _baseEntity = new BaseEntity();
        }

        [Test]
        public void GetById_WhenCalled_ProperMethodCalled()
        {
            _context.Setup(c => c.Set<BaseEntity>()).Returns(_dbSet.Object);
            _dbSet.Setup(d => d.Find(It.IsAny<int>())).Returns(_baseEntity);

            var repository = new Repository<BaseEntity>(_context.Object);
            repository.GetById(default);

            _context.Verify(c => c.Set<BaseEntity>(), Times.Once);
            _dbSet.Verify(d => d.Find(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetAll_WhenCalled_ProperMethodCalled()
        {
            var enumerable = new List<BaseEntity>() { _baseEntity }
                .AsEnumerable();
            _dbSet.As<IQueryable<BaseEntity>>().Setup(q => q.Provider).Returns(enumerable.AsQueryable().Provider);
            _dbSet.As<IQueryable<BaseEntity>>().Setup(q => q.Expression).Returns(enumerable.AsQueryable().Expression);
            _dbSet.As<IQueryable<BaseEntity>>().Setup(q => q.ElementType).Returns(enumerable.AsQueryable().ElementType);
            _dbSet.As<IQueryable<BaseEntity>>().Setup(q => q.GetEnumerator()).Returns(enumerable.AsQueryable().GetEnumerator());
            _context.Setup(c => c.Set<BaseEntity>()).Returns(_dbSet.Object);

            var repository = new Repository<BaseEntity>(_context.Object);
            var result = repository.GetAll();

            Assert.That(result.ToList(), Is.EqualTo(enumerable.ToList()));
        }

        [Test]
        public void Insert_EntityIsNull_ThrowArgumentNullException()
        {
            var repository = new Repository<BaseEntity>(_context.Object);

            Assert.That(() => repository.Insert(null),
                Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GetByRawSql_QueryIsNotValid_ThrowArgumentNullException(string query)
        {
            var repository = new Repository<BaseEntity>(_context.Object);

            Assert.That(() => repository.GetByRawSql(query),
                Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Insert_ValidEntityPassed_ProperMethodCalled()
        {
            _context.Setup(x => x.Set<BaseEntity>()).Returns(_dbSet.Object);
            _dbSet.Setup(x => x.Add(It.IsAny<BaseEntity>()));

            var repository = new Repository<BaseEntity>(_context.Object);
            repository.Insert(_baseEntity);

            _context.Verify(c => c.Set<BaseEntity>(), Times.Once);
            _dbSet.Verify(d => d.Add(_baseEntity), Times.Once);
        }

        [Test]
        public void Update_EntityIsNull_ThrowArgumentNullException()
        {
            var repository = new Repository<BaseEntity>(_context.Object);

            Assert.That(() => repository.Update(null),
                Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Update_ValidEntityPassed_ProperMethodCalled()
        {
            _context.Setup(x => x.Set<BaseEntity>()).Returns(_dbSet.Object);
            _dbSet.Setup(d => d.Update(It.IsAny<BaseEntity>()));

            var repository = new Repository<BaseEntity>(_context.Object);
            repository.Update(_baseEntity);

            _context.Verify(c => c.Set<BaseEntity>(), Times.Once);
            _dbSet.Verify(d => d.Update(_baseEntity), Times.Once);
        }

        [Test]
        public void Delete_EntityNotFound_ThrowEntityNotFoundException()
        {
            _context.Setup(x => x.Set<BaseEntity>()).Returns(_dbSet.Object);
            _dbSet.Setup(d => d.Find(It.IsAny<int>())).Returns<BaseEntity>(null);

            var repository = new Repository<BaseEntity>(_context.Object);

            Assert.That(() => repository.Delete(default),
                Throws.Exception.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void Delete_WhenCalled_ProperMethodCalled()
        {
            _baseEntity.ID = 1;
            _context.Setup(c => c.Set<BaseEntity>()).Returns(_dbSet.Object);
            _dbSet.Setup(d => d.Find(1)).Returns(_baseEntity);

            var repository = new Repository<BaseEntity>(_context.Object);
            repository.Delete(1);

            _context.Verify(c => c.Set<BaseEntity>(), Times.Once);
            _dbSet.Verify(d => d.Remove(It.Is<BaseEntity>(b => b.Equals(_baseEntity))), Times.Once);
        }
    }
}