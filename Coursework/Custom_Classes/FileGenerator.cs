using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Coursework.Custom_Classes
{
    public class FileGenerator
    {
        public byte[] ByteArray { get; }

        public FileGenerator(string text)
        {
            ByteArray = CreateDocx(text);
        }

        private byte[] CreateDocx(string text)
        {
            byte[] byteArray;
            using (MemoryStream stream = new MemoryStream())
            {
                using (WordprocessingDocument wordDoc =
                    WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document, true))
                {
                    MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                    mainPart.Document = new Document(
                        new Body(
                            new Paragraph(
                                new Run(
                                    new Text(text)))));
                }
                
                byteArray = stream.ToArray();
            }

            return byteArray;
        }
    }
}