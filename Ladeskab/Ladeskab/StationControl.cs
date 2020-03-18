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
        private ChargeControl _charger;
        private Door _door;
        private RfidReader _rfidReader;
        private Display _display;
        private Logger _log;

        private int _oldId;

        public StationControl(RfidReader rfidReader,
            Door door,
            ChargeControl charger,
            Display display,
            Logger log)
        {
            _rfidReader = rfidReader;
            _door = door;
            _charger = charger;
            _display = display;
            _log = log;

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
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = e.Id;
                        _log.LogDoorLocked(_oldId);

                        //Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _display.DisplayMessage(
                            "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        //Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                        _display.DisplayMessage("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
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
                        _log.LogDoorUnlocked(_oldId);

                        _display.DisplayMessage("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.DisplayMessage("Forkert RFID tag");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        private void DoorOpened(object sender, DoorOpenEventArgs e)
        {
            _display.DisplayMessage("Tilslut telefon!");
        }

        private void DoorClosed(object sender, DoorCloseEventArgs e)
        {
            _display.DisplayMessage("Indlæs RFID");
        }
    }
}