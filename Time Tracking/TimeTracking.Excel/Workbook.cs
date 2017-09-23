using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Excel
{
    /// <summary>
    /// (c) 2014 Vienna, Dietmar Schoder
    /// 
    /// Code Project Open License (CPOL) 1.02
    /// 
    /// Deals with an Excel workbook in an xlsx-file and provides all worksheets in it
    /// </summary>
    public class Workbook
    {
        public static sst SharedStrings;

        /// <summary>
        /// All worksheets in the Excel workbook deserialized
        /// </summary>
        /// <param name="excelFileName">Full path and filename of the Excel xlsx-file</param>
        /// <returns></returns>
        public static IEnumerable<worksheet> Worksheets(string excelFileName)
        {
            worksheet ws;

            using (ZipArchive zipArchive = ZipFile.Open(excelFileName, ZipArchiveMode.Read))
            {
                SharedStrings = DeserializedZipEntry<sst>(GetZipArchiveEntry(zipArchive, @"xl/sharedStrings.xml"));
                foreach (var worksheetEntry in (WorkSheetFileNames(zipArchive)).OrderBy(x => x.FullName))
                {
                    ws = DeserializedZipEntry<worksheet>(worksheetEntry);
                    ws.NumberOfColumns = worksheet.MaxColumnIndex + 1;
                    ws.ExpandRows();
                    yield return ws;
                }
            }
        }

        /// <summary>
        /// Method converting an Excel cell value to a date
        /// </summary>
        /// <param name="excelCellValue"></param>
        /// <returns></returns>
        public static DateTime DateFromExcelFormat(string excelCellValue)
        {
            return DateTime.FromOADate(Convert.ToDouble(excelCellValue));
        }
        
        private static ZipArchiveEntry GetZipArchiveEntry(ZipArchive zipArchive, string zipEntryName)
        {
            return zipArchive.Entries.First<ZipArchiveEntry>(n => n.FullName.Equals(zipEntryName));
        }
        private static IEnumerable<ZipArchiveEntry> WorkSheetFileNames(ZipArchive zipArchive)
        {
            foreach (var zipEntry in zipArchive.Entries)
                if (zipEntry.FullName.StartsWith("xl/worksheets/sheet"))
                    yield return zipEntry;
        }
        private static T DeserializedZipEntry<T>(ZipArchiveEntry zipArchiveEntry)
        {
            using (Stream stream = zipArchiveEntry.Open())
                return (T)new XmlSerializer(typeof(T)).Deserialize(XmlReader.Create(stream));
        }
    }
}
