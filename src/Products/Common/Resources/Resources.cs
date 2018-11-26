using GroupDocs.Annotation.MVC.Products.Common.Entity.Web;
using System;
using System.IO;
using System.Web;

namespace GroupDocs.Annotation.MVC.Products.Common.Resources
{
    /// <summary>
    /// Resources
    /// </summary>
    public class Resources
    {
        /// <summary>
        /// Get free file name for uploaded file if such file already exists
        /// </summary>
        /// <param name="directory">Directory where to search files</param>
        /// <param name="fileName">Uploaded file name</param>
        /// <returns></returns>
        public string GetFreeFileName(string directory, string fileName)
        {
            string resultFileName = "";
            try
            {
                // get all files from the directory
                string[] listOfFiles = Directory.GetFiles(directory);
                for (int i = 0; i < listOfFiles.Length; i++)
                {
                    // check if file with current name already exists
                    int number = i + 1;
                    string newFileName = Path.GetFileNameWithoutExtension(fileName) + "-Copy(" + number + ")." + Path.GetExtension(fileName);
                    resultFileName = Path.Combine(directory, newFileName);
                    if (File.Exists(resultFileName))
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return resultFileName;
        }

        /// <summary>
        /// Generate System.Exception
        /// </summary>
        /// <param name="ex">System.Exception</param>
        /// <returns>System.ExceptionEntity</returns>
        public ExceptionEntity GenerateException(System.Exception ex)
        {
            // Initiate System.Exception entity
            ExceptionEntity exceptionEntity = new ExceptionEntity();
            // set System.Exception data
            exceptionEntity.message = ex.Message;
            exceptionEntity.Exception = ex;
            return exceptionEntity;
        }

        /// <summary>
        /// Generate System.Exception for password error
        /// </summary>
        /// <param name="ex">System.Exception</param>
        /// <param name="password">string</param>
        /// <returns>System.ExceptionEntity</returns>
        public ExceptionEntity GenerateException(System.Exception ex, String password)
        {
            // Initiate System.Exception
            ExceptionEntity exceptionEntity = new ExceptionEntity();
            // Check if System.Exception message contains password and password is empty
            if (ex.Message.Contains("password") && String.IsNullOrEmpty(password))
            {
                exceptionEntity.message = "Password Required";
            }
            // Check if System.Exception contains password and password is set
            else if (ex.Message.Contains("password") && !String.IsNullOrEmpty(password))
            {
                exceptionEntity.message = "Incorrect password";
            }
            else
            {
                exceptionEntity.message = ex.Message;
                exceptionEntity.Exception = ex;
            }
            return exceptionEntity;
        }
    }
}