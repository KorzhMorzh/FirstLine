using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coursework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send(string text, string key, string isEncrypted)
        {
            ViewBag.Result = new Models.Vigener(text, key, isEncrypted).NewText;
            ViewBag.Text = text;
            ViewBag.Key = key;
            ViewBag.IsEncrypted = isEncrypted;
            return View("Index");
        }
    }
}