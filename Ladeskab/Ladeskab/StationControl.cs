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
    // TODO: This class is completely broken. Fix it!
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
        private IChargeControl _charger;
        private IDoor _door;
        private IRfidReader _rfidReader;
        private IDisplay _display;
        private ILogger _log;

        private int _oldId;

        public StationControl(
            IRfidReader rfidReader,
            IDoor door,
            IChargeControl charger,
            IDisplay display,
            ILogger log)
        {
            _rfidReader = rfidReader;
            _door = door;
            _charger = charger;
            _display = display;
            _log = log;

            _rfidReader.RfidReadEvent += RfidDetected;
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
                        _display.StationMessage("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.\n");

                        _state = LadeskabState.Locked;
                    }
                    else if (!_charger.IsConnected())
                    {
                        _display.StationMessage("Din telefon er ikke ordentlig tilsluttet. Prøv igen.\n");
                    }

                    break;

                case LadeskabState.DoorOpen:

                    break;

                case LadeskabState.Locked:
                    if (e.Id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _log.LogDoorUnlocked(_oldId);
                        _display.StationMessage("Tag din telefon ud af skabet og luk døren\n");

                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.StationMessage("Forkert RFID tag\n");
                    }

                    break;
            }
        }

        private void DoorOpened(object sender, DoorOpenEventArgs e)
        {
            if (_state == LadeskabState.Available)
            {
                _display.StationMessage("Tilslut telefon!\n");

                _state = LadeskabState.DoorOpen;
            }
        }

        private void DoorClosed(object sender, DoorCloseEventArgs e)
        {
            if (_state == LadeskabState.DoorOpen)
            {
                _display.StationMessage("Indlæs RFID\n");

                _state = LadeskabState.Available;
            }
        }
    }
}