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
        
        public ChargeControl(UsbChargerSimulator usbCharger)
        {
            connected = false;
            _usbCharger = usbCharger;
            _usbCharger.CurrentValueEvent += ReadCurrentValue;
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
                //do some charging
            }
        }
        public void StopCharge()
        {
            if(IsConnected())
            {
                //stop the charge
                _usbCharger.StopCharge();
            }
        }

        public void ReadCurrentValue(object sender, CurrentEventArgs e)
        {
            Console.WriteLine(e.Current);
        }
    }
}
