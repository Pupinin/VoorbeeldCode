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
    public class ExamDbRepositoryTest
    {
        private Byte[] _dataBytes;
        private Byte[] _dataBytes1;
        private Byte[] _dataBytes2;
        private IQueryable<Exam> _data;
        private Mock<DbSet<Exam>> _mockSet;
        private Mock<ExamDbContext> _mockContext;
        private IExamDbRepository _examDbRepository;
        private ExamDbContext _context;
        [SetUp]
        public void SetUp()
        {
            _dataBytes = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            _dataBytes1 = new byte[] { 0x10, 0x20, 0x10, 0x20, 0x10, 0x20, 0x10 };
            _dataBytes2 = new byte[] { 0x20, 0x10, 0x20, 0x10, 0x10, 0x20, 0x20 };

            _data = new List<Exam>
            {
                new Exam { Id = 666, Bytes = _dataBytes, Md5 = "grst" },
                new Exam {  Bytes = _dataBytes1 , Md5 ="pqr"},
                new Exam {  Bytes = _dataBytes2 , Md5="tvt"},
            }.AsQueryable();
            _mockSet = new Mock<DbSet<Exam>>();
            _mockSet.As<IQueryable<Exam>>().Setup(m => m.Provider).Returns(_data.Provider);
            _mockSet.As<IQueryable<Exam>>().Setup(m => m.Expression).Returns(_data.Expression);
            _mockSet.As<IQueryable<Exam>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            _mockSet.As<IQueryable<Exam>>().Setup(m => m.GetEnumerator()).Returns(() => _data.GetEnumerator());
            _mockContext = new Mock<ExamDbContext>();
            _mockContext.Setup(c => c.Exams).Returns(_mockSet.Object);
            _examDbRepository = new ExamDbRepository(_mockContext.Object);

            _context = new ExamDbContext();

        }

        [Test]
        public void AddExamTest()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Exam>>();
            var mockContext = new Mock<ExamDbContext>();
            mockContext.Setup(m => m.Exams).Returns(mockSet.Object);
            // Result
            IExamDbRepository service = new ExamDbRepository(mockContext.Object);
            service.AddExam(_data.ElementAt(0));

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Exam>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void DeleteByIdTest()
        {
            // Arrange
            var data = _data.ToList<Exam>();
            _mockSet.Setup(m => m.Remove(It.IsAny<Exam>())).Callback<Exam>((entity) => data.Remove(entity));
            _mockContext.Setup(x => x.Exams).Returns(_mockSet.Object);
            IExamDbRepository dataAccess = new ExamDbRepository(_mockContext.Object);

            //Act
            int idToDelete = 666;
            dataAccess.DeleteById(idToDelete);

            //Assert
            _mockContext.VerifyGet(x => x.Exams, Times.Exactly(2));
            _mockContext.Verify(x => x.SaveChanges(), Times.Once());
            Assert.AreEqual(data.Count, 3);
        }

        [Test]
        public void GetAllExamsTest()
        {
            // Arrange


            // Act
            IEnumerable<Exam> exams = _examDbRepository.GetAllExams();

            // Assert
            Assert.AreEqual(_dataBytes, exams.ElementAt(0).Bytes);
            Assert.AreEqual(_dataBytes1, exams.ElementAt(1).Bytes);
            Assert.AreEqual(_dataBytes2, exams.ElementAt(2).Bytes);
        }

        [Test]
        public void GetByIdTest()
        {
            // Arrange
            int id = 666;
            Exam expectedEntity = _data.ElementAt(0);
            DbSet<Exam> dbSet = Mock.Of<DbSet<Exam>>(set => set.Find((id)) == expectedEntity);
            ExamDbContext context = new ExamDbContext();
            context.Exams = dbSet;
            IExamDbRepository repository = new ExamDbRepository(context);

            // Act
            var result = repository.GetById(id);

            // Assert
            Assert.AreSame(expectedEntity, result);
        }
    }
}
