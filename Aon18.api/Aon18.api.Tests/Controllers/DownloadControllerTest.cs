using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Aon18.api.Controllers;
using Aon18.api.Services;
using Aon18.api.Tests.Builders;
using Aon18.data.DomainClasses;
using Aon18.data.Interfaces;
using Moq;
using NUnit.Framework;

namespace Aon18.api.Tests.Controllers
{
    [TestFixture]
    public class DownloadControllerTest
    {
        private Mock<IExamDbRepository> _examDbRepositoryMock;
        private Mock<ISkeletDbRepository> _skeletDbRepositoryMock;
        private DownloadController _controller;


        [SetUp]
        public void SetUp()
        {
            _examDbRepositoryMock = new Mock<IExamDbRepository>();
            _skeletDbRepositoryMock = new Mock<ISkeletDbRepository>();
            _controller = new DownloadController(_skeletDbRepositoryMock.Object, _examDbRepositoryMock.Object);
        }

        [Test]
        public void Get_CallsBothGetByIdMethods()
        {
            //Arrange
         
            Exam exampleExam = new Exam
            {
                Id = 1,
                //The Byte array has been assigned a random value simply to avoid an ArgumentNullException
                Bytes = new Byte[256],
                Md5 = ""
            };

            string filename = "AnExampleFile";
            Skelet exampleSkelet = new Skelet
            {
                Id = 1,
                FileName = filename,

                Exam = exampleExam,
                ExamId = exampleExam.Id
            };

            _skeletDbRepositoryMock.Setup(s => s.GetById(It.IsAny<int>())).Returns(exampleSkelet);
            _examDbRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).Returns(exampleExam);

            //Act
            var response = _controller.Get() as SkeletResponse;

            //Assert
            Assert.That(response, Is.Not.Null);
            _skeletDbRepositoryMock.Verify(s => s.GetById(It.IsAny<int>()), Times.Once);
            _examDbRepositoryMock.Verify(e => e.GetById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_WithIdWillGetTheSkeletFileNameAsString()
        {
            //Arrange
            var skeletToFind = new SkeletBuilder().Build();
            var skeletName = skeletToFind.FileName;

            _skeletDbRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(skeletToFind);

            //Act
            var result = _controller.Get(skeletToFind.Id) as OkNegotiatedContentResult<string>;
            //Assert
            Assert.That(result, Is.Not.Null);
            _skeletDbRepositoryMock.Verify(repo=>repo.GetById(It.IsAny<int>()), Times.Once);
            Assert.That(result.Content, Is.EqualTo(skeletName));
        }
    }
}
