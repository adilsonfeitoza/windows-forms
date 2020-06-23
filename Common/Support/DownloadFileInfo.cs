using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Support
{
    public class DownloadFileInfo
    {
        public DownloadFileTypesEnum FileType { get; set; }

        public string Extension { get; set; }

        public string ContentType { get; set; }

        public DownloadFileInfo(string fileType)
        {
            if (fileType == null)
                throw new ArgumentNullException("fileType");

            if (string.Equals(fileType, "xls", StringComparison.CurrentCultureIgnoreCase))
            {
                this.FileType = DownloadFileTypesEnum.Xls;
                this.ContentType = "application/x-xlsx";
                this.Extension = "xls";
            }
            else if (string.Equals(fileType, "csv", StringComparison.CurrentCultureIgnoreCase))
            {
                this.FileType = DownloadFileTypesEnum.Csv;
                this.ContentType = "text/csv";
                this.Extension = "csv";
            }
            else if (string.Equals(fileType, "zip", StringComparison.CurrentCultureIgnoreCase))
            {
                this.FileType = DownloadFileTypesEnum.Zip;
                this.ContentType = "application/zip";
                this.Extension = "zip";
            }
            else
            {
                throw new System.InvalidOperationException(string.Format("Tipo desconhecido. FileType={0}", fileType));
            }
        }

        public string GetFileName(string fileName)
        {
            return $"{ fileName.Replace(" ", "_") }_{ DateTime.Now.ToString("ddMMyyyy_HHmmss") }.{ Extension }";
        }
    }

    public enum DownloadFileTypesEnum
    {
        Xls,
        Pdf,
        Csv,
        Doc,
        Zip
    }
}
