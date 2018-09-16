using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Aon18.api.Controllers;
using Aon18.api.Services;
using Aon18.api.Tests.Builders;
using Aon18.data.DomainClasses;
using Aon18.data.Interfaces;
using Aon18.data.Repository;
using Moq;
using NUnit.Framework;

namespace Aon18.api.Tests.Controllers
{
    [TestFixture]
    public class ExportControllerTest
    {
        private Mock<IStudentDbRepository> _studentRepoMock;
        private ExportController _controller;

        [SetUp]
        public void SetUp()
        {
            _studentRepoMock = new Mock<IStudentDbRepository>();
            _controller = new ExportController(_studentRepoMock.Object);
        }

        [Test]
        public void GetWillCallZipperMethodsAndDbMethods()
        {
            //Arrange
            var allStudents = new List<Student>
            {
                new StudentBuilder().Build()
            };

            _studentRepoMock.Setup(repo => repo.GetAllStudents()).Returns(allStudents);

            //Act
            var result = _controller.Get() as SkeletResponse;

            //Assert
            Assert.That(result, Is.Not.Null);
            _studentRepoMock.Verify(repo=>repo.GetAllStudents(), Times.Once);

        } 
    }
}
