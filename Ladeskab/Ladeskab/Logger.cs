using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public interface ILogger
    {
        void LogDoorUnlocked(int id, StreamWriter output = null);
        void LogDoorLocked(int id);
    }


    public class Logger : ILogger
    {
        private readonly string _logFile; // Navnet på systemets log-fil

        public Logger(string logFile = "../../logfile.txt")
        {
            _logFile = logFile;
        }

        public void LogDoorUnlocked(int id, StreamWriter output = null)
        {
            if (!File.Exists(_logFile))
            {
                using (output ?? File.CreateText(_logFile))
                {
                    output.WriteLine($"Door Unlocked at {DateTime.Now:T} by {id}");
                }
            }
            else
            {
                using (output ?? File.AppendText(_logFile))
                {
                    output.WriteLine($"Door Unlocked at {DateTime.Now:T} by {id}");
                }
            }
        }

        public void LogDoorLocked(int id)
        {
            if (!File.Exists(_logFile))
            {
                using (StreamWriter output = File.CreateText(_logFile))
                {
                    output.WriteLine($"Door locked at {DateTime.Now:T} by {id}");
                }
            }
            else
            {
                using (StreamWriter output = File.AppendText(_logFile))
                {
                    output.WriteLine($"Door locked at {DateTime.Now:T} by {id}");
                }
            }
        }
    }
}