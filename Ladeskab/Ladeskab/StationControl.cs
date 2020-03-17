using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.UsbSimulator;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IUsbCharger _charger;
        private int _oldId;
        private Door _door;
        private RfidReader _rfidReader;

        private string _logFile = "../../logfile.txt"; // Navnet på systemets log-fil

        //TODO: Her mangler constructor
        public StationControl(RfidReader rfidReader, Door door, IUsbCharger charger)
        { 
            _rfidReader = rfidReader;
            _door = door;
            _charger = charger;

            rfidReader.RfidReadEvent += RfidDetected;
            _door.DoorOpenEvent += DoorOpened;
            _door.DoorCloseEvent += DoorClosed;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(object rfidReader, RfidReadEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = e.Id;
                        LogDoorLocked(_oldId);
                        using (var writer = File.AppendText(_logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", e.Id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (e.Id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        LogDoorUnlocked(_oldId);
                        using (var writer = File.AppendText(_logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", e.Id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        private void DoorOpened(object sender, DoorOpenEventArgs e)
        {
            Console.WriteLine("Tilslut telefon!");
        }

        private void DoorClosed(object sender, DoorCloseEventArgs e)
        {
            Console.WriteLine("Indlæs RFID");
        }

        private void LogDoorUnlocked(int ID)
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

        private void LogDoorLocked(int ID)
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
