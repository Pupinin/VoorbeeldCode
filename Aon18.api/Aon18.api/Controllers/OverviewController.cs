using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aon18.data.Interfaces;

namespace Aon18.api.Controllers
{
    public class OverviewController : Controller
    {

        // GET: Overview
        public ActionResult Overview()
        {
            return View();
        }

        public ActionResult StudentOverview()
        {
            
            return View();
        }
    }
}