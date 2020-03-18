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
            ChargeControl charger = new ChargeControl(usbSim, display);
            Logger log = new Logger();
            StationControl station = new StationControl(rfidReader, door, charger, display, log);

            bool finish = false;
            do
            {
                char input;
                //System.Console.WriteLine("Indtast E, O, C, R: ");
                display.StationMessage("Indtast (E)xit, (O)pen door, (C)lose door, (R)FID: ");
                input = Console.ReadKey(true).KeyChar;

                switch (Char.ToUpper(input))
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
                        display.StationMessage("Indtast RFID id: ");
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