using Models.Enums;
using System.IO;
using System.IO.Compression;

namespace Business.Helpers
{
    public class ZipHelpers
    {
        public static byte[] ZipFile(byte[] file, string fileName, FileType fileType)
        {
            if (file != null)
            {
                byte[] compressedBytes;
                using (var zipStream = new MemoryStream())
                {
                    using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                    {
                        var fileInZip = zip.CreateEntry(string.Format("{0}.{1}", fileName, fileType));
                        using (var entryStream = fileInZip.Open())
                        using (var fileToCompressStream = new MemoryStream(file))
                            fileToCompressStream.CopyTo(entryStream);
                    }
                    compressedBytes = zipStream.ToArray();
                }
                return compressedBytes;
            }
            else
            {
                return new byte[0];
            }
        }
    }
}