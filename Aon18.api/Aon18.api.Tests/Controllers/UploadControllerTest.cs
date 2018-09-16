using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Mvc;
using Aon18.api.Controllers;
using Aon18.data.DomainClasses;
using Aon18.data.Interfaces;
using Moq;
using NUnit.Framework;

namespace Aon18.api.Tests.Controllers
{
    [TestFixture]
    public class UploadControllerTest
    {
        private UploadController _controller;
  
        private Mock<IExamDbRepository> _examDbRepositoryMock;
        private Mock<IStudentDbRepository> _studentDbRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _examDbRepositoryMock = new Mock<IExamDbRepository>();
            _studentDbRepositoryMock = new Mock<IStudentDbRepository>();
            _controller = new UploadController(_examDbRepositoryMock.Object, _studentDbRepositoryMock.Object);

          
        }

        [Test]
        public void PostReturnsBadRequestIfUploadDoesNotSucceeds()
        {
            //Arrange

            var httpRequest = new HttpRequest("", "http://stackoverflow/", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);

            HttpContext.Current = new HttpContext(httpRequest, httpResponse);



            _examDbRepositoryMock.Setup(repo => repo.AddExam(It.IsAny<Exam>())).Returns(() => null);
            _studentDbRepositoryMock.Setup(repo => repo.AddStudent(It.IsAny<Student>())).Returns(() => null);

            //Act
            var result = _controller.Post() as BadRequestErrorMessageResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            _examDbRepositoryMock.Verify(repo => repo.AddExam(It.IsAny<Exam>()), Times.Never);
            _studentDbRepositoryMock.Verify(repo => repo.AddStudent(It.IsAny<Student>()), Times.Never);
        }

       
    }
}
