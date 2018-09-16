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
    public class OverviewControllerTest
    {
        private OverviewController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new OverviewController();
        }

        [Test]
        public void OverviewReturnsAViewWithContent()
        {
            //Arrange
            //Act
            ViewResult result = _controller.Overview() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        //[Test]
        //public void PasswordsOverviewReturnsViewWithContent()
        //{
        //    //Act
        //    ViewResult result = _controller.PasswordsOverview() as ViewResult;
        //    //Assert
        //    Assert.IsNotNull(result);
        //}

        [Test]
        public void StudentOverviewReturnsViewWithContent()
        {
            //Act
            ViewResult result = _controller.StudentOverview() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
