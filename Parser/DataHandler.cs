using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Parser
{
    static class DataHandler
    {
        internal static List<IThreatable> BaseInfoThreats = new List<IThreatable>();
        internal static List<Threat> ThreatsList = new List<Threat>();


        internal static void Download(string fileName)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://bdu.fstec.ru/documents/files/thrlist.xlsx",
                    fileName);
            }
        }

        internal static List<Threat> Parse(string fileName)
        {
            var package = new ExcelPackage(new FileInfo(fileName));
            List<Threat> threatsList = new List<Threat>();

            ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
            var start = workSheet.Dimension.Start;
            var end = workSheet.Dimension.End;
            string[] threat = new string[end.Column - 2];
            for (int row = start.Row + 2; row <= end.Row; row++)
            {
                for (int col = start.Column; col <= end.Column - 2; col++)
                {
                    threat[col - 1] = workSheet.Cells[row, col].Text;
                }

                threatsList.Add(new Threat(threat));
            }

            return threatsList;
        }

        internal static void Save()
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                worksheet.Cells["A1:F1"].Merge = true;
                worksheet.Cells["G1:I1"].Merge = true;
                worksheet.Cells["A1"].Value = "Общая информация";
                worksheet.Cells["G1"].Value = "Последствия";
                worksheet.Cells["A2"].Value = "Идентификатор УБИ в формате УБИ.ХХХ";
                worksheet.Cells["B2"].Value = "Идентификатор УБИ";
                worksheet.Cells["C2"].Value = "Наименование УБИ";
                worksheet.Cells["D2"].Value = "Описание";
                worksheet.Cells["E2"].Value = "Источник угрозы (характеристика и потенциал нарушителя)";
                worksheet.Cells["F2"].Value = "Объект воздействия";
                worksheet.Cells["G2"].Value = "Нарушение конфиденциальности";
                worksheet.Cells["H2"].Value = "Нарушение целостности";
                worksheet.Cells["I2"].Value = "Нарушение доступности";
                worksheet.Cells["C2:I300"].AutoFitColumns(10, 30);
                worksheet.Cells["A1:G2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A1:G1"].Style.Font.Bold = true;
                worksheet.Cells["A3:B300"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells["G3:I300"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells["A3"].LoadFromCollection(ThreatsList);

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save Excel sheet";
                saveFileDialog1.Filter = "Excel files|*.xlsx|All files|*.*";
                saveFileDialog1.FileName = "threats.xlsx";

                if (saveFileDialog1.ShowDialog() == true)
                {
                    FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                    excelPackage.SaveAs(fi);
                }
            }
        }

        private static string GetHash(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length); //смысл юзать хеш чтоб не хрнаить в памяти весь файл, у тбея оно тут всё хранится
                byte[] checkSum = md5.ComputeHash(fileData);
                string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
                return result;
            }
        }

        public static bool CheckUpdate()
        {
            Download("newThrlist.xlsx");

            string hashOriginalFile = GetHash("thrlist.xlsx");
            string hashNewFile = GetHash("newThrlist.xlsx");
            if (hashOriginalFile == hashNewFile)
            {
                File.Delete("newThrlist.xlsx");
                return true;
            }

            return false;
        }

        public static List<ThreatsChanges> GetChanges()
        {
            var newThreats = Parse("newThrlist.xlsx");
            List<ThreatsChanges> threatsChanges = new List<ThreatsChanges>();
            for (int i = 0; i < Math.Min(newThreats.Count, ThreatsList.Count); i++)
            {
                if (!ThreatsList[i].Equals(newThreats[i]))
                {
                    threatsChanges.Add(new ThreatsChanges(ThreatsList[i], newThreats[i]));
                }
            }

            if (newThreats.Count > ThreatsList.Count)
            {
                for (int i = ThreatsList.Count; i < newThreats.Count; i++)
                {
                    threatsChanges.Add(new ThreatsChanges(null, newThreats[i]));
                }
            }
            else if (ThreatsList.Count > newThreats.Count)
            {
                for (int i = newThreats.Count; i < ThreatsList.Count; i++)
                {
                    threatsChanges.Add(new ThreatsChanges(ThreatsList[i], null));
                }
            }

            return threatsChanges;
        }

        internal static void Replace()
        {
            File.Delete("thrlist.xlsx");
            File.Move("newThrlist.xlsx", "thrlist.xlsx");
        }

        internal static List<IThreatable> GetShortTreatsInfo()
        {
            var baseInfoThreats = new List<IThreatable>();
            ThreatsList = Parse("thrlist.xlsx");
            foreach (var item in ThreatsList)
            {
                baseInfoThreats.Add(item);
            }

            return baseInfoThreats;
        }
    }
}