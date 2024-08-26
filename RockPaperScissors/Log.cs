using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    internal class Log
    {
        /// <summary>
        /// Outputs the info logs in the console and log file (bot.log)
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void LogInformation(string message)
        {
            using (ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole()))
            {
                ILogger logger = factory.CreateLogger<Program>();

                logger.LogInformation(DateTime.Now + " | " + message);
            }

            using (var fs = new FileStream(@"./data/bot.log", FileMode.Append, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(DateTime.Now.ToString() + " | INFO | " + message);
                }
            }
        }

        /// <summary>
        /// Outputs warning logs in the console and log file (bot.log)
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void LogWarning(string message)
        {
            using (ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole()))
            {
                ILogger logger = factory.CreateLogger<Program>();

                logger.LogWarning(DateTime.Now + " | " + message);
            }

            using (var fs = new FileStream(@"./data/bot.log", FileMode.Append, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(DateTime.Now.ToString() + " | WARNING | " + message);
                }
            }
        }

        /// <summary>
        /// Outputs error logs in the console and log file (bot.log)
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void LogError(string message)
        {
            using (ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole()))
            {
                ILogger logger = factory.CreateLogger<Program>();

                logger.LogError(DateTime.Now + " | " + message);
            }

            using (var fs = new FileStream(@"./data/bot.log", FileMode.Append, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(DateTime.Now.ToString() + " | ERROR | " + message);
                }
            }
        }

        ///<summary>
        ///Checks if the log file (bot.log) exists and if not, it creats it.
        ///</summary>
        public static void CheckLogFile()
        {
            if (!File.Exists(@"./data/bot.log"))
            {
                Directory.CreateDirectory(@"./data");
                File.Create(@"./data/bot.log");
            }
        }
    }
}
