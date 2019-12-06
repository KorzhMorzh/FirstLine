using System.IO;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coursework.Controllers;
using Moq;
using DocumentFormat.OpenXml.Packaging;


namespace CourseworkTests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void TestIndex()
        {
            HomeController homeController = new HomeController();
            var result = homeController.Index();
            Assert.AreEqual("Index", ((ViewResult) result).ViewName);
        }

        [TestMethod]
        public void UploadReturnedDocxFileWithOriginalName()
        {
            HomeController homeController = new HomeController();
            var fileMock = new Mock<HttpPostedFileBase>();
            using (var fs = new FileStream("../../Files/HomeControllerTests/TestUpload.docx", FileMode.Open))
            {
                fileMock.Setup(f => f.InputStream).Returns(fs);
                fileMock.Setup(f => f.FileName).Returns("TestUpload.docx");

                var contextMock = new Mock<HttpContextBase>();
                var controllerContextMock = new Mock<ControllerContext>();
                controllerContextMock.SetupGet(con => con.HttpContext)
                    .Returns(contextMock.Object);
                homeController.ControllerContext = controllerContextMock.Object;
                var result = homeController.Upload(fileMock.Object, "скорпион", "false", "");
                var fileType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                Assert.AreEqual(fileType, (result.ContentType));
                Assert.AreEqual("TestUpload.docx", result.FileDownloadName);
            }
        }

        [TestMethod]
        public void UploadReturnedDocxFileWithNewName()
        {
            HomeController homeController = new HomeController();
            var fileMock = new Mock<HttpPostedFileBase>();
            using (var fs = new FileStream("../../Files/HomeControllerTests/TestUpload.docx", FileMode.Open))
            {
                fileMock.Setup(f => f.InputStream).Returns(fs);
                fileMock.Setup(f => f.FileName).Returns("TestUpload.docx");

                var contextMock = new Mock<HttpContextBase>();
                var controllerContextMock = new Mock<ControllerContext>();
                controllerContextMock.SetupGet(con => con.HttpContext)
                    .Returns(contextMock.Object);
                homeController.ControllerContext = controllerContextMock.Object;
                var result = homeController.Upload(fileMock.Object, "скорпион", "false", "NewFile");
                var fileType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                Assert.AreEqual(fileType, (result.ContentType));
                Assert.AreEqual("NewFile.docx", result.FileDownloadName);
            }
        }

        [TestMethod]
        public void UploadReturnedErrorWhenFileIsNotDocx()
        {
            HomeController homeController = new HomeController();
            var fileMock = new Mock<HttpPostedFileBase>();
            using (var fs = new FileStream("../../Files/HomeControllerTests/TestUpload.txt", FileMode.Open))
            {
                fileMock.Setup(f => f.InputStream).Returns(fs);
                fileMock.Setup(f => f.FileName).Returns("TestUpload");

                var contextMock = new Mock<HttpContextBase>();
                var controllerContextMock = new Mock<ControllerContext>();
                controllerContextMock.SetupGet(con => con.HttpContext)
                    .Returns(contextMock.Object);
                homeController.ControllerContext = controllerContextMock.Object;
                Assert.ThrowsException<OpenXmlPackageException>(() =>
                    homeController.Upload(fileMock.Object, "скорпион", "false", ""));
            }
        }

        [TestMethod]
        public void UploadReturnedErrorWhenFileDoesNotExist()
        {
            HomeController homeController = new HomeController();
            Assert.ThrowsException<HttpException>(() =>
                homeController.Upload(null, "скорпион", "false", ""));
        }

        [TestMethod]
        public void DownloadReturnedNewDocxFile()
        {
            HomeController homeController = new HomeController();
            var result = homeController.Download("Some text");
            var fileType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            Assert.AreEqual(fileType, (result.ContentType));
        }

        [TestMethod]
        public void CalculateReturnIndex()
        {
            HomeController homeController = new HomeController();
            var result = homeController.Calculate("поздравляю", "скорпион", "false");
            Assert.AreEqual("Index", ((ViewResult)result).ViewName);
        }

        [TestMethod]
        public void CalculateSetsViewBag()
        {
            HomeController homeController = new HomeController();
            var result = homeController.Calculate("поздравляю", "скорпион", "true");
            Assert.AreEqual("бщцфаирщри", ((ViewResult)result).ViewData["Result"]);
            Assert.AreEqual("скорпион", ((ViewResult)result).ViewData["Key"]);
        }
    }
}