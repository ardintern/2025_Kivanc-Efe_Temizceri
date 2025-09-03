using System;
using System.IO;

namespace ECommerceApi.Helpers
{
    public static class Logger
    {

        private static string logPath = @"C:\Users\ke_te\OneDrive\Masaüstü\ApiLogs\Logs.txt";

        public static void Log(string message)
        {

            try
            {

                Directory.CreateDirectory(Path.GetDirectoryName(logPath));

                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]{message}\n";

                File.AppendAllText(logPath, logEntry);


            }


            catch (Exception ex) 
            
            {



                File.AppendAllText(@"C:\Logs\apilogs_error.txt", ex.ToString() + "\n");
            
            
            
            
            }




        }



    }       



    
}
