using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSWithEvents
{
    public class TempSensor : ITempSensor
    {
        public event EventHandler<TempChangedEventArgs> TempChangedEvent;

        private int _oldTemp;
        public void SetTemp(int newTemp)
        {
            if (newTemp != _oldTemp)
            {
                OnTempChanged(new TempChangedEventArgs { Temp = newTemp });
                _oldTemp = newTemp;
            }
        }

        protected virtual void OnTempChanged(TempChangedEventArgs e)
        {
            TempChangedEvent?.Invoke(this, e);
        }
    }
}
