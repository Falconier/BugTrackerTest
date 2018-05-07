using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace BugTrackerTest.Models.Extensions
{
    public static class FileUploadValidator
    {
        public static bool IsWebFriendlyFile(HttpPostedFileBase file)
        {
            if (file == null)
                return false;
            if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024)
                return false;
            string fileExt = VirtualPathUtility.GetExtension(file.FileName).ToLower();
            if(fileExt == ".pdf" || fileExt == ".doc" || fileExt == ".docx" || fileExt == "xls" || fileExt == ".jpg" || fileExt == ".png" || fileExt == ".gif" || fileExt == ".bmp")
            {
                return true;
            }
            else
            {
                return false;
            }
            //try
            //{
            //    using (var upload = Image.FromStream(file.InputStream))
            //    {
            //        return ImageFormat.Jpeg.Equals(upload.RawFormat) || ImageFormat.Png.Equals(upload.RawFormat) || ImageFormat.Gif.Equals(upload.RawFormat) ||;
            //    }
            //}
            //catch
            //{
            //    return false;
            //}
        }
    }
}