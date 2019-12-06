using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coursework.Custom_Classes;
using DocumentFormat.OpenXml.Packaging;

namespace CourseworkTests
{
    [TestClass]
    public class FileGeneratorTests
    {
        [TestMethod]
        public void ReturnDocxWithSentText()
        {
            var text = "Поздравляю!";
            var result = new FileGenerator(text).ByteArray;
            using (var mem = new MemoryStream(result))
            {
                using (var docWord = WordprocessingDocument.Open(mem, false))
                {
                    Assert.AreEqual(text, docWord.MainDocumentPart.Document.InnerText);
                }
            }
        }
    }
}
