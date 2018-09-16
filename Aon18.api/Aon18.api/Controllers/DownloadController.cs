using Aon18.data.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aon18.api.Services;
using Aon18.data.DomainClasses;

namespace Aon18.api.Controllers
{
    public class DownloadController : ApiController
    {

        private readonly ISkeletDbRepository _skeletRepo;
        private readonly IExamDbRepository _examRepo;

        public DownloadController(ISkeletDbRepository skeletDbRepository, IExamDbRepository examDbRepository)
        {
            _skeletRepo = skeletDbRepository;
            _examRepo = examDbRepository;
        }


        // GET: api/Download
        public IHttpActionResult Get()
        {
            Skelet skelet = _skeletRepo.GetById(1);
            Exam exam = _examRepo.GetById(skelet.ExamId);

            var dataBytes = exam.Bytes;      

            return new SkeletResponse(dataBytes, Request, skelet.FileName);
        }

        // GET: api/Download
        public IHttpActionResult Get(int id)
        {
            Skelet skelet = _skeletRepo.GetById(1);

            return Ok(skelet.FileName);
        }

    }
}
