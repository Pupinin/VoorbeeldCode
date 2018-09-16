using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aon18.data.DomainClasses;
using Aon18.data.Interfaces;

namespace Aon18.api.Controllers
{
    public class StudentController : ApiController
    {
        private readonly IStudentDbRepository _studentDbRepository;

        public StudentController(IStudentDbRepository repository)
        {
            _studentDbRepository = repository;
        }

        // Post: Student
        [System.Web.Http.HttpPost]
        public IHttpActionResult Post_AddStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var studentToPost = _studentDbRepository.AddStudent(student);

            return Ok(studentToPost);
        }

        //Delete: Student
        public IHttpActionResult Delete_StudentById(int id)
        {
            if (_studentDbRepository.GetById(id) == null)
            {
                return NotFound();
            }

            _studentDbRepository.DeleteById(id);

            return Ok();
        }

        [Authorize]
        public IHttpActionResult GetAll()
        {
            return Ok(_studentDbRepository.GetAllStudents());
        }

        public IHttpActionResult GetById(int id)
        {
            var student = _studentDbRepository.GetById(id);
            if (student == null) return NotFound();
            return Ok(student);
        }
    }
}
