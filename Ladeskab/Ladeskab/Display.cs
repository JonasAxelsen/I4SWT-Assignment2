using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public interface IDisplay
    {
        void StationMessage(string message, StreamWriter writer = null);
        void ChargingMessage(string message);
    }


    public class Display : IDisplay
    {
        public Display()
        {
        }

        public void StationMessage(string message, StreamWriter writer = null)
        {
            if (writer != null) Console.SetOut(writer);
            Console.WriteLine(message);
        }

        public void ChargingMessage(string message)
        {
            Console.Write(message);
        }
    }
}