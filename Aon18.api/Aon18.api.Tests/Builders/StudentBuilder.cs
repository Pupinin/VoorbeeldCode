using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aon18.data.DomainClasses;

namespace Aon18.api.Tests.Builders
{
    public class StudentBuilder
    {
        private readonly Student _student;

        public StudentBuilder()
        {
            _student = new Student
            {
                Name = Guid.NewGuid().ToString(),
                FirstName = Guid.NewGuid().ToString()
            };

        }

        public StudentBuilder WithId()
        {
            _student.Id = new Random().Next(1, int.MaxValue);
            return this;
        }

        public StudentBuilder WithEmptyName()
        {
            _student.Name = null;
            return this;
        }

        public Student Build()
        {
            return _student;
        }
    }
}
