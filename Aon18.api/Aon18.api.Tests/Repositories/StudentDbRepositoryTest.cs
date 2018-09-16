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
   public class StudentDbRepositoryTest
    {
        private IQueryable<Student> _studentsdata;
        private Mock<DbSet<Student>> _mockSet;
        private Mock<ExamDbContext> _mockContext;
        private IStudentDbRepository _studentDbRepository;
        private ExamDbContext _context;
        [SetUp]
        public void SetUp()
        {

            _studentsdata = new List<Student>
            {
                new Student{ Id = 1, FirstName="sergey", Name="pupinin"},
                new Student{ Id = 2, FirstName="Ahmet", Name="Kocyigit"},
                new Student{ Id = 666, FirstName="Lion", Name="gelders"},
            }.AsQueryable();

            _mockSet = new Mock<DbSet<Student>>();
            _mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(_studentsdata.Provider);
            _mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(_studentsdata.Expression);
            _mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(_studentsdata.ElementType);
            _mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(() => _studentsdata.GetEnumerator());
            _mockContext = new Mock<ExamDbContext>();
            _mockContext.Setup(c => c.Students).Returns(_mockSet.Object);
            _studentDbRepository = new StudentDbRepository(_mockContext.Object);

            _context = new ExamDbContext();

        }

        [Test]
        public void AddStudentTest()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Student>>();
            var mockContext = new Mock<ExamDbContext>();
            mockContext.Setup(m => m.Students).Returns(mockSet.Object);
            // Result
            var service = new StudentDbRepository(mockContext.Object);
            service.AddStudent(_studentsdata.ElementAt(0));

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Student>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void DeleteByIdTest()
        {
            // Arrange
            var data = _studentsdata.ToList<Student>();
            _mockSet.Setup(m => m.Remove(It.IsAny<Student>())).Callback<Student>((entity) => data.Remove(entity));
            _mockContext.Setup(x => x.Students).Returns(_mockSet.Object);
            IStudentDbRepository dataAccess = new StudentDbRepository(_mockContext.Object);

            //Act
            int idToDelete = 666;
            dataAccess.DeleteById(idToDelete);

            //Assert
            Assert.AreEqual(data.Count, 3);
        }

        [Test]
        public void GetAllStudentTest()
        {
            // Act
            IEnumerable<Student> students = _studentDbRepository.GetAllStudents();

            // Assert
            Assert.AreEqual(_studentsdata.ElementAt(0), students.ElementAt(0));
            Assert.AreEqual(_studentsdata.ElementAt(1), students.ElementAt(1));
            Assert.AreEqual(_studentsdata.ElementAt(2), students.ElementAt(2));
        }

        [Test]
        public void GetByIdTest()
        {
            // Arrange
            int id = 666;
            Student expectedEntity = _studentsdata.ElementAt(0);
            DbSet<Student> dbSet = Mock.Of<DbSet<Student>>(set => set.Find((id)) == expectedEntity);
            ExamDbContext context = new ExamDbContext();
            context.Students = dbSet;
            IStudentDbRepository repository = new StudentDbRepository(context);

            // Act
            var result = repository.GetById(id);

            // Assert
            Assert.AreSame(expectedEntity, result);
        }
    }
}
