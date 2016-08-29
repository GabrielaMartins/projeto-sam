using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace SamServices.Helpers
{
    public class ImageHelper
    {

        public static void SaveAsImage(string img, string name, string where)
        {
            try
            {
             
                img = img.Trim();
                var init = img.IndexOf('/') + 1;
                var end = img.IndexOf(',') - init;
                var imageType = img.Substring(init,end);
                var content = img.Substring(img.IndexOf(',') + 1);

                var physicalPath = $"{where}{Path.DirectorySeparatorChar}{name}.{imageType}";

                using (FileStream fs = new FileStream(physicalPath, FileMode.OpenOrCreate))
                {
                    var contentInBytes = Convert.FromBase64String(content);
                    fs.Write(contentInBytes, 0, contentInBytes.Length);
                }
               
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static FileInfo GetImageInfo(string name)
        {
            try
            {
                var logicPath = ConfigurationManager.AppSettings["LogicImagePath"];
                var path = HttpContext.Current.Server.MapPath(logicPath);
                var imgPath = Directory.GetFiles(path, name + ".*")[0];
                return new FileInfo(imgPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetPhysicalPathForImage(string name)
        {
            try
            {
                return GetImageInfo(name).FullName;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetLogicPathForImage(string name)
        {
            try
            {
                var imgInfo = GetImageInfo(name);

                var logicPath = ConfigurationManager.AppSettings["LogicImagePath"];
                var domain = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                var path = domain + '/' + logicPath.Substring(2) + '/' + imgInfo.Name;

                return path;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static void DeleteImage(string name)
        {
            try
            {
                var imgInfo = GetImageInfo(name);
                File.Delete(imgInfo.FullName);
            }
            catch
            {
                return;
            }
        }
    }
}