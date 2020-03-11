using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Door
    {
        private bool open;
        private bool locked;

        public Door()
        {
            open = false;
            locked = false;
        }

        public void LockDoor()
        {
            locked = true;
        }

        public void UnlockDoor()
        {
            locked = false;
        }

        public void OpenDoor()
        {
            if (open == false)
            {
                open = true;
                OnDoorOpen(new DoorOpenEventArgs());
            }
        }

        public void CloseDoor()
        {
            if (open == true)
            {
                open = false;
                OnDoorClose(new DoorCloseEventArgs());
            }
        }


        // Events
        public event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
        protected virtual void OnDoorOpen(DoorOpenEventArgs e)
        {
            DoorOpenEvent?.Invoke(this, e);
        }

        public event EventHandler<DoorCloseEventArgs> DoorCloseEvent;
        protected virtual void OnDoorClose(DoorCloseEventArgs e)
        {
            DoorCloseEvent?.Invoke(this, e);
        }
    }


    // Event Args
    public class DoorOpenEventArgs : EventArgs
    {
        // TODO: Add attributes?
    }

    public class DoorCloseEventArgs : EventArgs
    {
        // TODO: Add attributes?
    }
}
