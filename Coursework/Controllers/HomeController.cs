using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coursework.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send(string text, string key, string isEnCoded)
        {
            ViewBag.Result = new Models.Vigener(text, key, isEnCoded).NewText;
            ViewBag.Text = text;
            ViewBag.Key = key;
            ViewBag.IsEncoded = isEnCoded;
            return View("Index");
        }
    }
}