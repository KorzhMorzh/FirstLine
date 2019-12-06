using System.IO;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coursework.Custom_Classes;
using DocumentFormat.OpenXml.Packaging;
using Moq;

namespace CourseworkTests
{
    [TestClass]
    public class FileHandlerTests
    {
        [TestMethod]
        public void FileHandlerReturnedDocxWithCorrectText()
        {
            var fileMock = new Mock<HttpPostedFileBase>();
            using (var fsEncrypt =
                new FileStream("../../Files/FileHandlerTests/OriginalEncryptedText.docx", FileMode.Open))
            {
                fileMock.Setup(f => f.InputStream).Returns(fsEncrypt);
                fileMock.Setup(f => f.FileName).Returns("OriginalEncryptedText.docx");
                var result = new FileHandler(fileMock.Object).Cipher("скорпион", "true");
                using (var memEncrypted = new MemoryStream(result))
                {
                    using (var memDecrypted = new MemoryStream())
                    {
                        using (var fsDecrypt = new FileStream(
                            "../../Files/FileHandlerTests/DecryptedTextForComparing.docx",
                            FileMode.Open))
                        {
                            fsDecrypt.CopyTo(memDecrypted);
                        }

                        memDecrypted.Position = 0;
                        using (var wordDocEncrypted = WordprocessingDocument.Open(memEncrypted, false))
                        {
                            using (var wordDocDecrypted = WordprocessingDocument.Open(memDecrypted, false))
                            {
                                Assert.AreEqual(wordDocDecrypted.MainDocumentPart.Document.InnerText,
                                    wordDocEncrypted.MainDocumentPart.Document.InnerText);
                            }
                        }
                    }
                }
            }
        }
    }
}