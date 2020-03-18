using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Display
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