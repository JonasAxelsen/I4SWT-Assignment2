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
        void StationMessage(string message, TextWriter writer = null);
        void ChargingMessage(string message, TextWriter writer = null);
    }


    public class Display : IDisplay
    {
        public Display()
        {
        }

        public void StationMessage(string message, TextWriter writer = null)
        {
            Message(message, writer);
        }

        public void ChargingMessage(string message, TextWriter writer = null)
        {
            Message(message, writer);
        }

        private void Message(string message, TextWriter writer = null)
        {
            if (writer == null)
            {
                writer = Console.Out;
            }

            using (writer)
            {
                writer.Write(message);
            }
        }
    }
}