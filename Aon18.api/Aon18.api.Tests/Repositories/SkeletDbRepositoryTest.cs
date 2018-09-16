using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aon18.data.Context;
using Aon18.data.DomainClasses;
using Aon18.data.Interfaces;
using Aon18.data.Repository;
using Moq;
using NUnit.Framework;

namespace Aon18.api.Tests.Repositories
{
    [TestFixture]
   public class SkeletDbRepositoryTest
    {
        private IQueryable<Skelet> _data;
        private Mock<DbSet<Skelet>> _mockSet;
        private Mock<ExamDbContext> _mockContext;
        private ISkeletDbRepository _skeletDbRepository;
        private ExamDbContext _context;
        [SetUp]
        public void SetUp()
        {

            _data = new List<Skelet>
            {
                new Skelet { Id = 666, FileName="demo1"},
                new Skelet { Id = 1, FileName="demo2"},
                new Skelet { Id = 2, FileName="demo3"},
            }.AsQueryable();
            _mockSet = new Mock<DbSet<Skelet>>();
            _mockSet.As<IQueryable<Skelet>>().Setup(m => m.Provider).Returns(_data.Provider);
            _mockSet.As<IQueryable<Skelet>>().Setup(m => m.Expression).Returns(_data.Expression);
            _mockSet.As<IQueryable<Skelet>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            _mockSet.As<IQueryable<Skelet>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());
            _mockContext = new Mock<ExamDbContext>();
            _mockContext.Setup(c => c.Skelets).Returns(_mockSet.Object);
            _skeletDbRepository = new SkeletDbRepository(_mockContext.Object);

            _context = new ExamDbContext();

        }

        [Test]
        public void AddSkeletTest()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Skelet>>();
            var mockContext = new Mock<ExamDbContext>();
            mockContext.Setup(m => m.Skelets).Returns(mockSet.Object);
            // Result
            var service = new SkeletDbRepository(mockContext.Object);
            service.AddSkelet(_data.ElementAt(0));

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Skelet>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void DeleteByIdTest()
        {
            // Arrange
            var data = _data.ToList<Skelet>();
            _mockSet.Setup(m => m.Remove(It.IsAny<Skelet>())).Callback<Skelet>((entity) => data.Remove(entity));
            _mockContext.Setup(x => x.Skelets).Returns(_mockSet.Object);
            ISkeletDbRepository dataAccess = new SkeletDbRepository(_mockContext.Object);

            //Act
            int idToDelete = 666;
            dataAccess.DeleteById(idToDelete);

            //Assert
            Assert.AreEqual(data.Count, 3);
        }

        [Test]
        public void GetAllSkeletTest()
        {
            // Arrange


            // Act
            IEnumerable<Skelet> skelets = _skeletDbRepository.GetAllSkelets();

            // Assert
            Assert.AreEqual(_data.ElementAt(0), skelets.ElementAt(0));
            Assert.AreEqual(_data.ElementAt(1), skelets.ElementAt(1));
            Assert.AreEqual(_data.ElementAt(2), skelets.ElementAt(2));
        }

        [Test]
        public void GetByIdTest()
        {
            // Arrange
            int id = 666;
            Skelet expectedEntity = _data.ElementAt(0);
            DbSet<Skelet> dbSet = Mock.Of<DbSet<Skelet>>(set => set.Find((id)) == expectedEntity);
            ExamDbContext context = new ExamDbContext();
            context.Skelets = dbSet;
            ISkeletDbRepository repository = new SkeletDbRepository(context);

            // Act
            var result = repository.GetById(id);

            // Assert
            Assert.AreSame(expectedEntity, result);
        }
    }
}
