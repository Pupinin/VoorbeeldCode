using Aon18.data.Context;
using Aon18.data.DomainClasses;
using Aon18.data.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.IO.Compression;

namespace Aon18.api.Services
{
    public class Zipper
    {
        public static void ZipExams(IEnumerable<Student> students)
        {

            var basePath = @"D:\school\PXL\2de jaar\Research project\2018_AON18\Aon18.api\Aon18.api\";
            System.IO.DirectoryInfo di = new DirectoryInfo(basePath + @"Export\");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            //save exams
            var context = new ExamDbContext();
            var repositiry = new ExamDbRepository(context);
            int index = 0;
            //File file = new File();
            foreach (var student in students)
            {
                //get exam for student
                Exam exam = student.Examen;
                string fileName = student.FirstName + "_" + student.Name + "_" + student.StudentNumber + "_" + index.ToString() + ".zip";
                byte[] data = exam.Bytes;
                var filePath = basePath + @"Export\" + fileName;
                File.WriteAllBytes(filePath, data);

                index++;
            }

            //zip file export
            //ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);
        }

        public static byte[] ExportedExams()
        {
            var basePath = @"D:\school\PXL\2de jaar\Research project\2018_AON18\Aon18.api\Aon18.api\";

            string startPath = basePath + @"\Export";
            string zipPath = basePath +@"\ExportZip\result.zip";
            System.IO.DirectoryInfo di = new DirectoryInfo(basePath + @"ExportZip\");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            ZipFile.CreateFromDirectory(startPath, zipPath);

            return File.ReadAllBytes(zipPath);
        }

    }
}