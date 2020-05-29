using AgroCommerce.Utilities.CustomExceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace AgroCommerce.Utilities
{
    public class FileService
    {
        public static string SaveImage(IFormFile file, string folderName)
        {
            try
            {
                string ext = Path.GetExtension(file.FileName);
                if (!CheckIfFileIsAnImage(ext.ToLower()))
                    throw new FileNotAnImageException();

                if (!CheckFileSize(file))
                    throw new FileTooLargeException();

                string uniqueFileName = "";
                string uploadsFolder = "wwwroot/Images/" + folderName;
                Directory.CreateDirectory(uploadsFolder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                file.CopyTo(new FileStream(filePath, FileMode.Create));

                return filePath;
            }
            catch (Exception ex)
            {
                throw new FileSaveErrorException(ex.ToString());
            }
        }

        public static void DeleteFile(string path)
        {
            //string path = Request.MapPath(filePath);
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    throw new FileDeleteErrorException(ex.ToString());
                }
            }

        }

        private static bool CheckIfFileIsAnImage(string extenstion)
        {
            if (extenstion == ".jpeg" || extenstion == ".gif" || extenstion == ".png" || extenstion == ".jpg" || extenstion == ".ico")
                return true;

            return false;
        }

        private static bool CheckFileSize(IFormFile file)
        {
            if (file.Length > (4 * 1000 * 1024))
                return false;

            return true;

        }
    }
}

