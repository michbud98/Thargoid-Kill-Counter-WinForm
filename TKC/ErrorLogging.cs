using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKC
{
    /// <summary>
    /// Class which handles saving data about errors in .txt files
    /// </summary>
    class ErrorLogging
    {
        /// <summary>
        /// Method which saves error in log
        /// </summary>
        /// <param name="e"> Exception</param>
        public static void LogError(Exception e)
        {
            string directoryPath;
            //finds path to Users folder ("C:\Users\<user>)"
            directoryPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                directoryPath = Directory.GetParent(directoryPath).ToString();
            }
            directoryPath += @"\Documents\TkcErrorLogs";
            string date = DateTime.Today.ToString("dd/MM/yyyy");
            Directory.CreateDirectory(directoryPath);
            string filePath = Path.Combine(directoryPath, "Log"+date+".txt");

            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine("-----------------------------------------------------------------------------");
            writer.WriteLine("Date : " + DateTime.Now.ToString());
            writer.WriteLine();

            while (e != null)
            {
                writer.WriteLine(e.GetType().FullName);
                writer.WriteLine("Message : " + e.Message);
                writer.WriteLine("StackTrace : " + e.StackTrace);

                e = e.InnerException;
            }
            writer.Close();
        }

        /// <summary>
        /// Overloaded method which saves error in log with additional input from method causing Error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"> Exception</param>
        /// <param name="input1"> Generic for one input</param>
        public static void LogError<T>(Exception e, T input1)
        {
            string directoryPath;
            //finds path to Users folder ("C:\Users\<user>)"
            directoryPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                directoryPath = Directory.GetParent(directoryPath).ToString();
            }
            directoryPath += @"\Documents\TkcErrorLogs";
            string date = DateTime.Today.ToString("dd/MM/yyyy");
            Directory.CreateDirectory(directoryPath);
            string filePath = Path.Combine(directoryPath, "Log" + date + ".txt");

            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine("-----------------------------------------------------------------------------");
            writer.WriteLine("Date : " + DateTime.Now.ToString());
            writer.WriteLine();

            while (e != null)
            {
                writer.WriteLine(e.GetType().FullName);
                writer.WriteLine("Message : " + e.Message);
                writer.WriteLine("StackTrace : " + e.StackTrace);
                writer.WriteLine("Input1 : " + input1.ToString());

                e = e.InnerException;
            }
            writer.Close();
        }
        /// <summary>
        /// Method which saves unknown thargoid types found in logs into txt file
        /// </summary>
        /// <param name="JSONString">JSON string input</param>
        public static void LogUnknownThargoidType(String JSONString)
        {
            string directoryPath;
            //finds path to Users folder ("C:\Users\<user>)"
            directoryPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                directoryPath = Directory.GetParent(directoryPath).ToString();
            }
            directoryPath += @"\Documents\TkcErrorLogs";
            string date = DateTime.Today.ToString("dd/MM/yyyy");
            Directory.CreateDirectory(directoryPath);
            string filePath = Path.Combine(directoryPath, "UnknownThargoid" + date + ".txt");

            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine("-----------------------------------------------------------------------------");
            writer.WriteLine("Date : " + DateTime.Now.ToString());
            writer.WriteLine("JSONString : " + JSONString);
            writer.WriteLine();

            
            writer.Close();
        }
    }
}

