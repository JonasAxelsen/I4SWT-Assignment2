using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public interface IDisplay
    {
        void StationMessage(string message);
        void ChargingMessage(string message);
    }

    public class Display : IDisplay
    {
        public Display()
        {
        }

        public void StationMessage(string message)
        {
            System.Console.WriteLine(message);
        }

        public void ChargingMessage(string message)
        {
            System.Console.Write(message);
        }
    }
}