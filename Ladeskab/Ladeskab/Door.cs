using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Door
    {
        private bool _open;
        private bool _locked;

        public Door()
        {
            _open = false;
            _locked = false;
        }

        public void LockDoor()
        {
            if (!_open)
            {
                _locked = true;
            }
            else
            {
                throw new InvalidOperationException("Cannot lock an open door.");
            }
        }

        public void UnlockDoor()
        {
            _locked = false;
        }

        public void OpenDoor()
        {
            if (!_open && !_locked)
            {
                _open = true;
                OnDoorOpen(new DoorOpenEventArgs());
            }
        }

        public void CloseDoor()
        {
            if (_open)
            {
                _open = false;
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
    }

    public class DoorCloseEventArgs : EventArgs
    {
    }
}