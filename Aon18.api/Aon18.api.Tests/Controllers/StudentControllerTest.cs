using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Aon18.api.Controllers;
using Aon18.api.Tests.Builders;
using Aon18.data.DomainClasses;
using Aon18.data.Interfaces;
using Moq;
using NUnit.Framework;

namespace Aon18.api.Tests.Controllers
{
    [TestFixture]
   public class StudentControllerTest
    {
        private Mock<IStudentDbRepository> _repoMock;
        private StudentController _controller;

        [SetUp]
        public void SetUp()
        {
            _repoMock = new Mock<IStudentDbRepository>();
            _controller = new StudentController(_repoMock.Object);
        }

        [Test]
        public void Post_AddStudentShallAddStudentToDb()
        {
            //Arrange
            var studentToAdd = new StudentBuilder().Build();

            _repoMock.Setup(repo => repo.AddStudent(It.IsAny<Student>())).Returns(() =>
            {
                studentToAdd.Id = new Random().Next(1, int.MaxValue);
                return studentToAdd;
            });

            //Act
            var result = _controller.Post_AddStudent(studentToAdd) as OkNegotiatedContentResult<Student>;

            //Assert
            _repoMock.Verify(repo => repo.AddStudent(studentToAdd), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(result.Content, studentToAdd);
            Assert.That(result.Content.Id, Is.GreaterThan(0));

        }

        [Test]
        public void Post_AddStudentInvalidModelShallReturnBadRequest()
        {
            //Arrange
            _controller.ModelState.AddModelError("StudentNames", "The name is required");
            var studentToAdd = new StudentBuilder().WithEmptyName().Build();
            //Act
            var result = _controller.Post_AddStudent(studentToAdd) as BadRequestResult;
            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Delete_RemovesStudentFromRepository()
        {
            //Arrange
            var studentToDelete = new StudentBuilder().WithId().Build();

            _repoMock.Setup(repo => repo.GetById(studentToDelete.Id)).Returns(studentToDelete);

            //Act
            var result = _controller.Delete_StudentById(studentToDelete.Id) as OkResult;
            //Assert
            Assert.That(result, Is.Not.Null);
            _repoMock.Verify(repo => repo.GetById(studentToDelete.Id), Times.Once);
            _repoMock.Verify(repo => repo.DeleteById(studentToDelete.Id), Times.Once);
        }

        [Test]
        public void Delete_NonExistingStudentReturnsNotFound()
        {
            //Arrange
            _repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(() => null);
            var randomId = new Random().Next(1, int.MaxValue);
            //Act
            var result = _controller.Delete_StudentById(randomId) as NotFoundResult;
            //Assert
            Assert.NotNull(result);
            _repoMock.Verify(repo => repo.GetById(randomId), Times.Once);
            _repoMock.Verify(repo => repo.DeleteById(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void GetAll_ShallReturnListOfStudentsFromRepository()
        {
            //Arrange
            var listOfStudents = new List<Student>
            {
                new StudentBuilder().Build()
            };

            _repoMock.Setup(repo => repo.GetAllStudents()).Returns(() => listOfStudents);
            //Act
            var result = _controller.GetAll() as OkNegotiatedContentResult<IEnumerable<Student>>;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.EquivalentTo(listOfStudents));
            _repoMock.Verify(repo => repo.GetAllStudents(), Times.Once);
        }

        [Test]
        public void Get_ShallReturnStudentIfExist()
        {
            //Arrange
            var studentToGet = new StudentBuilder().WithId().Build();
            _repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(() => studentToGet);
            //Act
            var result = _controller.GetById(studentToGet.Id) as OkNegotiatedContentResult<Student>;
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.EqualTo(studentToGet));
            _repoMock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_ReturnsNotFoundIfStudentsNotExist()
        {
            //Arrange
            _repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(() => null);
            var randomId = new Random().Next(1, int.MaxValue);
            //Act
            var result = _controller.GetById(randomId) as NotFoundResult;
            //Assert
            Assert.That(result, Is.Not.Null);
            _repoMock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Once);
        }
    }
}
