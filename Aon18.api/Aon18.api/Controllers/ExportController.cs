using Aon18.api.Services;
using Aon18.data.Interfaces;
using Aon18.data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aon18.api.Controllers
{
    
    public class ExportController : ApiController
    {
        private readonly IStudentDbRepository _studentDbRepository;

        public ExportController(IStudentDbRepository  studentDbRepository)
        {
            _studentDbRepository = studentDbRepository;
        }

        // GET: api/Export
        public IHttpActionResult Get()
        {
            Zipper.ZipExams(_studentDbRepository.GetAllStudents());
            byte[] array = Zipper.ExportedExams();
            return new SkeletResponse(array, Request, "Export.zip");
        }

    }
}
