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
    public class ExamDbRepository : IExamDbRepository
    {
        private readonly ExamDbContext _context;

        public ExamDbRepository(ExamDbContext context)
        {
            _context = context;
        }

        public Exam AddExam(Exam exam)
        {
            _context.Exams.Add(exam);
            _context.SaveChanges();
            return exam;
        }

        public void DeleteById(int id)
        {
            var exam = _context.Exams.Find(id);
            _context.Exams.Remove(exam);
            _context.SaveChanges();
        }

        public IEnumerable<Exam> GetAllExams()
        {
            return _context.Exams.ToList();
        }

        public Exam GetById(int id)
        {
            try
            {

                return _context.Exams.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
