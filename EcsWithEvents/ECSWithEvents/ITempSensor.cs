using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSWithEvents
{
    public class TempChangedEventArgs : EventArgs
    {
        public int Temp { get; set; }
    }

    public interface ITempSensor
    {
        event EventHandler<TempChangedEventArgs> TempChangedEvent;
    }
}
