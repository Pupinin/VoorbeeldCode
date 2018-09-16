using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aon18.data.DomainClasses
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string ExamenHash { get; set; }
        [Required]
        public string StudentNumber { get; set; }

        public string Datetime { get; set; }
        public string FileName { get; set; }
        public int ExamenId { get; set; }
        //navigation
        [ForeignKey("ExamenId")]
        public virtual Exam Examen { get; set; }
    }
}
