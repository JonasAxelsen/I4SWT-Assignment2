using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class RfidReader
    {
        public RfidReader()
        {

        }

        public void ReadRfid(int id)
        {
            OnRfidRead(new RfidReadEventArgs(id));
        }

        // Events
        public EventHandler<RfidReadEventArgs> RfidReadEvent;
        protected virtual void OnRfidRead(RfidReadEventArgs e)
        {
            RfidReadEvent?.Invoke(this, e);
        }
    }

    public class RfidReadEventArgs : EventArgs
    {
        public int Id { get; private set; }
        public RfidReadEventArgs(int id)
        {
            Id = id;
        }

        // TODO: Add attributes
    }
}
