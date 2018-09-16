using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Aon18.api.Controllers;
using NUnit.Framework;

namespace Aon18.api.Tests.Controllers
{
    [TestFixture]
   public class HomeControllerTest
    {
        private HomeController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new HomeController();
        }


        [Test]
        public void IndexMethodReturnsAViewWithContent()
        {
            // Arrange


            // Act
            ViewResult result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DownloadReturnsViewWithContent()
        {
            // Arrange


            // Act
            ViewResult result = _controller.Download() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void UploadReturnsViewWithContent()
        {
            // Arrange


            // Act
            ViewResult result = _controller.Upload() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
