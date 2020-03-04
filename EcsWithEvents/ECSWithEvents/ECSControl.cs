using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSWithEvents
{

    public class ECSControl
    {
        private readonly IHeater _heater;

        private int _threshold;

        public int CurrentTemperature;

        public ECSControl(int threshold, ITempSensor tempSensor, IHeater heater)
        {
            tempSensor.TempChangedEvent += HandleTempChangedEvent;
            _threshold = threshold;
            _heater = heater;
        }

        private void HandleTempChangedEvent(object sender, TempChangedEventArgs e)
        {
            CurrentTemperature = e.Temp;
            Regulate();
        }

        private void Regulate()
        {
            if (CurrentTemperature < _threshold)
            {
                _heater.TurnOn();
            }
            else
            {
                _heater.TurnOff();
            }
        }
    }
}
