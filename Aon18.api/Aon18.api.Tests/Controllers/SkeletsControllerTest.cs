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
   public class SkeletsControllerTest
    {
        private Mock<ISkeletDbRepository> _repoMock;
        private SkeletsController _controller;

        [SetUp]
        public void SetUp()
        {
            _repoMock = new Mock<ISkeletDbRepository>();
            _controller = new SkeletsController(_repoMock.Object);
        }

        [Test]
        public void Post_AddSkeletsShallAddSkeletToDb()
        {
            //Arrange
            var skeletToAdd = new SkeletBuilder().Build();

            _repoMock.Setup(repo => repo.AddSkelet(It.IsAny<Skelet>())).Returns(() => skeletToAdd);

            //Act
            var result = _controller.Post_addSkelet(skeletToAdd) as OkNegotiatedContentResult<Skelet>;

            //Assert
            _repoMock.Verify(repo => repo.AddSkelet(skeletToAdd), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(result.Content, skeletToAdd);
            Assert.That(result.Content.Id, Is.GreaterThan(0));
        }

        [Test]
        public void Delete_RemovesSkeletFromRepository()
        {
            //Arrange
            var skeletToDelete = new SkeletBuilder().Build();

            _repoMock.Setup(repo => repo.GetById(skeletToDelete.Id)).Returns(skeletToDelete);

            //Act
            var result = _controller.Delete_SkeletById(skeletToDelete.Id) as OkResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            _repoMock.Verify(repo => repo.GetById(skeletToDelete.Id), Times.Once);
            _repoMock.Verify(repo => repo.DeleteById(skeletToDelete.Id), Times.Once);
        }

        [Test]
        public void Delete_NonExistingSkeletReturnsNotFound()
        {
            //Arrange
            _repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(() => null);
            var randomId = new Random().Next(1, int.MaxValue);
            //Act
            var result = _controller.Delete_SkeletById(randomId) as NotFoundResult;
            //Assert
            Assert.NotNull(result);
            _repoMock.Verify(repo => repo.GetById(randomId), Times.Once);
            _repoMock.Verify(repo => repo.DeleteById(It.IsAny<int>()), Times.Never);
        }
    }
}
