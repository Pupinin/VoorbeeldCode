using System.IO;
using Aon18.data.DomainClasses;
using Aon18.data.Services;

namespace Aon18.data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Aon18.data.Context.ExamDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Aon18.data.Context.ExamDbContext context)
        {
            //Exams seed

            //adding first Exam (skelet)

            string locationSkelet = @"D:\school\PXL\2de jaar\Research project\2018_AON18\Aon18.api\Aon18.api\skelet\examen.zip";  //AppDomain.CurrentDomain.BaseDirectory + @"\skelet\examen.zip";

            Byte[] dataBytes = File.ReadAllBytes(locationSkelet);
            var exams1 = new Exam
            {
                Bytes = dataBytes,
                Md5 = MD5Hasher.CalculateHash(dataBytes)
            };

            //adding skelet
            var skelet1 = new Skelet
            {
                FileName = "Web_Advanced_Examen1.zip",
                ExamId = exams1.Id
            };
            context.Exams.AddOrUpdate(r => r.Bytes, exams1);
            context.Skelets.AddOrUpdate(r => r.FileName, skelet1);

            //adding first Exam (student)
            var student1 = new Student
            {
                Name = "Yilmaz",
                FirstName = "Sinasi",
                ExamenHash = MD5Hasher.CalculateHash(dataBytes),
                StudentNumber = "38",
                FileName = "Sergey_Web_Advanced.zip",
                Datetime = DateTime.Now.ToString(),
                ExamenId = 1
        };
            context.Students.AddOrUpdate(r => r.FirstName, student1);


        }
    }
}
