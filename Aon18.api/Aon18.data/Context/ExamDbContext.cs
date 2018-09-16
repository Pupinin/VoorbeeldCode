using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aon18.data.DomainClasses;

namespace Aon18.data.Context
{
   public class ExamDbContext: DbContext
    {
        public ExamDbContext():base("ExamDbContext")
        {

        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Skelet> Skelets { get; set; }


        public static void SetInitializer()
        {
          //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ExamDbContext, UpdateDatabaseOperation.Migration.Configuration>());
        }
    }
}
