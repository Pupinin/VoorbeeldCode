using Aon18.data.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aon18.api.Tests.Builders
{
   public class SkeletBuilder
    {
        private readonly Skelet _skelet;

        public SkeletBuilder()
        {
            _skelet = new Skelet
            {
                Id = new Random().Next(1, int.MaxValue),
                FileName = Guid.NewGuid().ToString(),
                Exam = new Exam(),
                ExamId = new Random().Next(1, int.MaxValue)
            };
        }

        public Skelet Build()
        {
            return _skelet;
        }
    }
}
