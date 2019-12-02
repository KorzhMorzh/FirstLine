using System.IO;
using System.Web;
using DocumentFormat.OpenXml.Packaging;

namespace Coursework.Custom_Classes
{
    public class FileHandler
    {
        public HttpPostedFileBase UploadedDocument { get; }
        public string OriginalFileName { get; }

        public FileHandler(HttpPostedFileBase upload)
        {
            UploadedDocument = upload;
            OriginalFileName = upload.FileName;
        }

        public string ParseDocument(WordprocessingDocument wordDoc)
        {
            string docText;
            using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();
            }
            return docText;
        }

        public byte[] Cipher(string key, string isEncrypted)
        {
            byte[] byteArray; 
            using (MemoryStream stream = new MemoryStream())
            {
                CopyInputStreamToStream(stream);
                stream.Position = 0;
                using (var wordDoc = WordprocessingDocument.Open(stream, true))
                {
                    var newDocText = new Vigener(ParseDocument(wordDoc), key, isEncrypted).NewText;

                    using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(newDocText);
                    }

                }
                stream.Position = 0;
                byteArray = stream.ToArray();
            }
            return byteArray;
        }

        public void CopyInputStreamToStream(Stream destination)
        {
            byte[] buffer = new byte[32768];
            int bytesRead;
            do
            {
                bytesRead = UploadedDocument.InputStream.Read(buffer, 0, buffer.Length);
                destination.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);
        }

    }
}