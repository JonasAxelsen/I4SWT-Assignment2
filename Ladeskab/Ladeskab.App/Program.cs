using System;
using Ladeskab;
using Ladeskab.UsbSimulator;

namespace Ladeskab.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Door door = new Door();
            Display display = new Display();
            RfidReader rfidReader = new RfidReader();
            UsbChargerSimulator usbSim = new UsbChargerSimulator();
            ChargeControl charger = new ChargeControl(usbSim);
            StationControl station = new StationControl(rfidReader, door, charger, display);

            bool finish = false;
            do
            {
                string input;
                //System.Console.WriteLine("Indtast E, O, C, R: ");
                display.displayMessage("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OpenDoor();
                        break;

                    case 'C':
                        door.CloseDoor();
                        break;

                    case 'R':
                        //System.Console.WriteLine("Indtast RFID id: ");
                        display.displayMessage("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.ReadRfid(id);
                        break;

                    default:
                        break;
                }
            } while (!finish);
        }
    }
}