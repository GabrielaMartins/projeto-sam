using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace SamApi.Helpers
{
    public class ImageHelper
    {

        public static void saveAsImage(string base64imageString, string name)
        {
            try
            {
             
                base64imageString = base64imageString.Trim();
                var init = base64imageString.IndexOf('/') + 1;
                var end = base64imageString.IndexOf(';') - init;
                var imageType = base64imageString.Substring(init,end);
                var content = base64imageString.Substring(base64imageString.IndexOf(',') + 1);

                var logicPath = ConfigurationManager.AppSettings["LogicImagePath"];
                var physicalPath = HttpContext.Current.Server.MapPath(logicPath) + Path.DirectorySeparatorChar + name + '.' + imageType;

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

        public static Image GetImageByName(string name)
        {
            try
            {
                var logicPath = ConfigurationManager.AppSettings["LogicImagePath"];
                var path = HttpContext.Current.Server.MapPath(logicPath);
                var imgPath = Directory.GetFiles(path, name + ".*")[0];

                return Image.FromFile(imgPath);
            }
            catch(Exception ex)
            {
                throw ex;
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
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}