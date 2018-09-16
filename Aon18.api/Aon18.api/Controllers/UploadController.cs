using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.WebPages;
using Aon18.data.DomainClasses;
using Aon18.data.Interfaces;
using Aon18.data.Services;

namespace Aon18.api.Controllers
{
    public class UploadController : ApiController
    {
        private readonly IExamDbRepository _examRepo;
        private readonly IStudentDbRepository _studentRepo;

        public UploadController(IExamDbRepository examDbRepository, IStudentDbRepository studentDbRepository)
        {
            _examRepo = examDbRepository;
            _studentRepo = studentDbRepository;
        }


        // POST: api/Upload
        [HttpPost]
        public IHttpActionResult Post()
        {
            var httpRequest = HttpContext.Current.Request;
           

            string fileName = null;
            //var  = httpRequest.Params["naam"];
            //var collection = httpRequest.Params["studentNumber"];

            try
            {
                fileName = httpRequest.Files[0].FileName;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Could not be uploaded:" + e.Message);
            }
            

            if (!fileName.IsEmpty())
            {
                HttpPostedFile postedFile = httpRequest.Files[0];
                int postedFileLength = postedFile.ContentLength;
                byte[] input = new byte[postedFileLength];

                Stream mystream = postedFile.InputStream;
                mystream.Read(input, 0, postedFileLength);


                Exam exam = new Exam();
                exam.Bytes = input;
                string hash = MD5Hasher.CalculateHash(input);
                exam.Md5 = hash;

                exam = _examRepo.AddExam(exam);

                Student student = new Student();
                student.Name = httpRequest.Params["naam"];
                student.FirstName = "demo";
                student.ExamenHash = hash;
                student.StudentNumber = httpRequest.Params["studentNumber"];
                student.Datetime = DateTime.Now.ToString();
                student.FileName = "webExamen.zip";
                student.ExamenId = exam.Id;


                _studentRepo.AddStudent(student);
                //return Ok($"File {fileName} Uploaded");
                return Ok(hash);
            }

            return BadRequest("Could not be uploaded");
        }
    }
}
