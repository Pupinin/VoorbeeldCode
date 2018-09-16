using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aon18.data.Interfaces;

namespace Aon18.api.Controllers
{
    public class LoginController : Controller
    {
        private readonly IStudentDbRepository _studentDbRepository;


        public LoginController(IStudentDbRepository studentDbRepository)
        {
            _studentDbRepository = studentDbRepository;
        }


        // GET: Login
        public ActionResult StudentLogin()
        {

            return View();
        }

        public ActionResult SupervisorLogin()
        {
            return View();
        }
    }
}