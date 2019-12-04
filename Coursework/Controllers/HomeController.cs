using System;
using System.Web;
using System.Web.Mvc;
using Coursework.Custom_Classes;

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
            ViewBag.Result = new Vigener(text, key, isEncrypted).NewText;
            ViewBag.Text = text;
            ViewBag.Key = key;
            ViewBag.IsEncrypted = isEncrypted;
            return View("Index");
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(System.IO.FileFormatException), View = "~/Error/CustomError")]
        public FileResult Upload(HttpPostedFileBase upload, string key, string isEncrypted, string FileName)
        {
            if (upload != null)
            {
                var docFile = new FileHandler(upload);
                var byteFile = docFile.Cipher(key, isEncrypted);
                string fileType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                string fileName = FileName == "" ? docFile.OriginalFileName : FileName + ".docx";
                return File(byteFile, fileType, fileName);
            }

            return null;
        }
    }
}