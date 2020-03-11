using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class ChargeControl
    {
        public bool connected { private get; set; }
        ChargeControl()
        {
            connected = false;
        }
        public bool IsConnected()
        {
            return connected;
        }
        public void StartCharge()
        {
            if(IsConnected())
            {
                //do some charging
            }

        }
        public void StopCharge()
        {
            if(IsConnected())
            {
                //stop the charge
            }
        }
    }
}
