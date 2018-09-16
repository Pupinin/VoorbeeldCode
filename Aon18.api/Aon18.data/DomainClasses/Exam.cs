using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aon18.data.DomainClasses
{
    public   class Exam
    {
        [Key]
        public int Id { get; set; }
        public Byte[] Bytes { get; set; }
        public string Md5 { get; set; }
    }
}
