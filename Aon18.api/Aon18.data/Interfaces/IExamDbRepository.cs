using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aon18.data.DomainClasses;

namespace Aon18.data.Interfaces
{
   public interface IExamDbRepository
    {
        IEnumerable<Exam> GetAllExams();
        Exam AddExam(Exam exam);
        void DeleteById(int id);
        Exam GetById(int id);
    }
}
