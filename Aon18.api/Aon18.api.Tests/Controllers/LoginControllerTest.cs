using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Aon18.api.Controllers;
using Aon18.data.Interfaces;
using Moq;
using NUnit.Framework;

namespace Aon18.api.Tests.Controllers
{
    [TestFixture]
    public class LoginControllerTest
    {
        private LoginController _controller;
        private Mock<IStudentDbRepository> _studentRepoMock;

        [SetUp]
        public void SetUp()
        {
            _studentRepoMock = new Mock<IStudentDbRepository>();
            _controller = new LoginController(_studentRepoMock.Object);
        }

        [Test]
        public void StudentLoginReturnsViewWithContent()
        {
            //Act
            ViewResult result = _controller.StudentLogin() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void SupervisorLoginReturnsViewWithContent()
        {
            //Act
            ViewResult result = _controller.SupervisorLogin() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
