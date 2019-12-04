using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coursework.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult FileLimitError()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult WrongRequest()
        {
            return View();
        }
        
    }
}
