using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aon18.data.Context;
using Aon18.data.DomainClasses;
using Aon18.data.Interfaces;

namespace Aon18.data.Repository
{
    public class StudentDbRepository : IStudentDbRepository
    {
        private readonly ExamDbContext _context;

        public StudentDbRepository(ExamDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public Student AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public void DeleteById(int id)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);

            _context.SaveChanges();
        }

        public Student GetById(int id)
        {
            return _context.Students.Find(id);
        }
    }
}
