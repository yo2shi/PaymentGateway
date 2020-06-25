using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace PaymentWebAPI.Logger
{
    public class DefaultLogger
    {

        private static string fileName;
        private static string fileLocation;

        public DefaultLogger()
        {
            //fileName = 

            //string userName = System.Configuration.ConfigurationManager.secureAppSettings["userName"];
            //string userPassword = System.Configuration.ConfigurationManager.secureAppSettings["userPassword"];

            System.Collections.Specialized.NameValueCollection section = (System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("appSettings");
            fileName = section["fileName"];
            fileLocation = section["filePath"];

        }

        public static void CheckFileExists()
        {
            string targetFile = "";
            targetFile = fileLocation;
            if (targetFile.EndsWith("\\") == false)
            {
                targetFile += "\\";
            }
            targetFile += fileName;

            if (System.IO.File.Exists(targetFile) == false)
            {
                File.Create(targetFile);
            }

        }

        //public void WriteToFile(string fileName, string fileLocation, string[] lines)
        public void WriteLinesToLog(string[] lines)
        {
            CheckFileExists();

            string targetFile = "";
            targetFile = fileLocation;
            if (targetFile.EndsWith("\\") == false)
            {
                targetFile += "\\";
            }
            targetFile += fileName;

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@targetFile, true))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }


        public void AppendToLog(string line)
        {
            CheckFileExists();

            string targetFile = "";
            targetFile = fileLocation;
            if (targetFile.EndsWith("\\") == false)
            {
                targetFile += "\\";
            }
            targetFile += fileName;

            using (System.IO.StreamWriter file =
             new System.IO.StreamWriter(@targetFile, true))
            {
                file.WriteLine(line);
            }

       
        }
    }
}