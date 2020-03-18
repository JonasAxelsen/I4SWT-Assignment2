using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Logger
    {
        private readonly string _logFile; // Navnet på systemets log-fil

        public Logger(string logFile = "../../logfile.txt")
        {
            _logFile = logFile;
        }

        public void LogDoorUnlocked(int ID)
        {
            if (!File.Exists(_logFile))
            {
                using (StreamWriter output = File.CreateText(_logFile))
                {
                    output.WriteLine($"Door Unlocked at {DateTime.Now:T} by {ID}");
                }
            }
            else
            {
                using (StreamWriter output = File.AppendText(_logFile))
                {
                    output.WriteLine($"Door Unlocked at {DateTime.Now:T} by {ID}");
                }
            }
        }

        public void LogDoorLocked(int ID)
        {
            if (!File.Exists(_logFile))
            {
                using (StreamWriter output = File.CreateText(_logFile))
                {
                    output.WriteLine($"Door locked at {DateTime.Now:T} by {ID}");
                }
            }
            else
            {
                using (StreamWriter output = File.AppendText(_logFile))
                {
                    output.WriteLine($"Door locked at {DateTime.Now:T} by {ID}");
                }
            }
        }
    }
}
