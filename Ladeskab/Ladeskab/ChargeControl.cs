using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.UsbSimulator;

namespace Ladeskab
{
    public class ChargeControl
    {
        public bool connected { private get; set; }

        private UsbChargerSimulator _usbCharger;
        private Display _display;
        
        public ChargeControl(UsbChargerSimulator usbCharger, Display display)
        {
            connected = false;
            _usbCharger = usbCharger;
            _usbCharger.CurrentValueEvent += ReadCurrentValue;
            _display = display;
        }
        public bool IsConnected()
        {
            // Kan ikke kalde _usbCharger.SimulateConnected fordi det ikke er en del af IUsbCharger interface
            // UsbChargerSimulator forventer mere (precondition) end IUsbCharger. Bryder med Liskov substitution principle.
            _usbCharger.SimulateConnected(true);
            return _usbCharger.Connected;
        }
        public void StartCharge()
        {
            if(IsConnected())
            {
                _usbCharger.StartCharge();
            }
        }

        public void StopCharge()
        {
            if(IsConnected())
            {
                _usbCharger.StopCharge();
            }
        }

        public void ReadCurrentValue(object sender, CurrentEventArgs e)
        {
            if (e.Current > 5 && e.Current <= 500)
            {
                //Ladning forløber normalt
                _display.displayMessage("Ladestrømmen er " + e.Current + " mA");
                return;
            }

            if (e.Current > 0 && e.Current <= 5)
            {
                //Ladning er fuldført
                StopCharge();
                _display.displayMessage("Ladningen er stoppet");
                return;
            }

            if (e.Current > 500)
            {
                //Noget er galt evt. kortslutning så ladning skal stoppes omgående!
                StopCharge();
                _display.displayMessage("Ladningen er stoppet. Ladestrøm for høj!");
            }
        }
    }
}
