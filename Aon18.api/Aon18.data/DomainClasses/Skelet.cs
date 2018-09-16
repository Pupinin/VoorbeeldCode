using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aon18.data.DomainClasses
{
    public class Skelet
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        //navigation
        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        public int ExamId { get; set; }
    }
}
