﻿using System;

namespace Ladeskab.UsbSimulator
{
    class UsbChargerSimulator : IUsbCharger
    {
        public event EventHandler<CurrentEventArgs> CurrentValueEvent;
        public double CurrentValue { get; }
        public bool Connected { get; }
        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }

        void SimulateConnected(bool isConnected)
        {
            throw new NotImplementedException();
        }

        void SimulateOverload(bool isOverloaded)
        {
            throw new NotImplementedException();
        }
    }
}