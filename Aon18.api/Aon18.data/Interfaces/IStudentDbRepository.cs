using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aon18.data.DomainClasses;

namespace Aon18.data.Interfaces
{
    public interface IStudentDbRepository
    {
        IEnumerable<Student> GetAllStudents();
        Student AddStudent(Student student);
        void DeleteById(int id);
        Student GetById(int id);
    }
}
