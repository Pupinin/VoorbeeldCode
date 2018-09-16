using Aon18.api.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aon18.api.Tests.Services
{
    [TestFixture]
    public class ZipperTest
    {
        [Test]
        public void ExportedExamsReTurnsByteArray()
        {
            Assert.NotNull(Zipper.ExportedExams());
        }
    }
}
