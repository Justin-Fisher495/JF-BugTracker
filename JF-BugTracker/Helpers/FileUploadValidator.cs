using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace JF_BugTracker.Helpers
{
    public class FileUploadValidator
    {
        public static bool IsWebFriendlyFile(HttpPostedFileBase file)
        {
            if (file == null) return false;

            var maxSize = WebConfigurationManager.AppSettings["MaxFileSize"];
            var minSize = WebConfigurationManager.AppSettings["MinFileSize"];
            if (file.ContentLength > Convert.ToInt32(maxSize) || file.ContentLength < Convert.ToInt32(minSize))
                return false;

            try
            {
                var allowedExtensions = WebConfigurationManager.AppSettings["AllowedExtentions"];
                var fileExt = Path.GetExtension(file.FileName);
                return allowedExtensions.Contains(fileExt);
            }
            catch
            {
                return false;
            }
        }
    }
}