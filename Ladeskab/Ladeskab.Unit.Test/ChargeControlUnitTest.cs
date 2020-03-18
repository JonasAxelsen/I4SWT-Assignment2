using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.UsbSimulator;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    class ChargeControlUnitTest
    {
        private ChargeControl _uut;
        private IUsbCharger _fakeUsbCharger;
        private IDisplay _fakedisplay;

        [SetUp]
        public void Setup()
        {
            _fakeUsbCharger = Substitute.For<IUsbCharger>();
            _fakedisplay = Substitute.For<IDisplay>();

            _uut = new ChargeControl(_fakeUsbCharger, _fakedisplay);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsConnected_Returns_RightValue(bool connection)
        {
            _fakeUsbCharger.Connected.Returns(connection);

            Assert.That(_uut.IsConnected(), Is.EqualTo(connection));
        }

        [Test]
        public void StartCharge_NotConnected_NoCallsReceived()
        {
            _fakeUsbCharger.Connected.Returns(false);

            _uut.StartCharge();

            _fakeUsbCharger.DidNotReceive().StartCharge();
        }

        [Test]
        public void StartCharge_Connected_OneCallReceived()
        {
            _fakeUsbCharger.Connected.Returns(true);

            _uut.StartCharge();

            _fakeUsbCharger.Received(1).StartCharge();
        }

        [Test]
        public void StopCharge_NotConnected_NoCallsReceived()
        {
            _fakeUsbCharger.Connected.Returns(false);

            _uut.StopCharge();

            _fakeUsbCharger.DidNotReceive().StopCharge();
        }

        [Test]
        public void StopCharge_Connected_OneCallReceived()
        {
            _fakeUsbCharger.Connected.Returns(true);

            _uut.StopCharge();

            _fakeUsbCharger.Received(1).StopCharge();
        }

        [TestCase(double.MaxValue, "Ladningen er stoppet")]
        [TestCase(500.1, "Ladningen er stoppet")]
        [TestCase(500.0, "Ladestrømmen er")]
        [TestCase(5.1, "Ladestrømmen er")]
        [TestCase(5.0, "Telefonen er ladet fuldt op")]
        [TestCase(4.9, "Telefonen er ladet fuldt op")]
        [TestCase(0.1, "Telefonen er ladet fuldt op")]
        public void RaiseEvent_ReadCurrentValue_displayCalledRight(double current, string msg)
        {
            _fakeUsbCharger.Connected.Returns(true);

            _fakeUsbCharger.CurrentValueEvent += 
                Raise.EventWith(new CurrentEventArgs()
                {
                    Current = current
                });

            _fakedisplay.Received(1).ChargingMessage(Arg.Is<string>(s => s.Contains(msg)));
        }

        [TestCase(0.0)]
        [TestCase(-0.1)]
        [TestCase(double.MinValue)]
        public void RaiseEvent_ReadCurrentValue_displayCalledRight(double current)
        {
            _fakeUsbCharger.Connected.Returns(true);

            _fakeUsbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs()
                {
                    Current = current
                });

            _fakedisplay.DidNotReceive().ChargingMessage(Arg.Any<String>());
        }

        [TestCase(double.MaxValue)]
        [TestCase(500.1)]
        [TestCase(500.0)]
        [TestCase(5.1)]
        [TestCase(5.0)]
        [TestCase(4.9)]
        [TestCase(0.1)]
        [TestCase(0.0)]
        [TestCase(-0.1)]
        [TestCase(double.MinValue)]
        public void RaiseEvent_ReadCurrentValue_Disconnected_displayCalledRight(double current)
        {
            _fakeUsbCharger.Connected.Returns(false);

            _fakeUsbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs()
                {
                    Current = current
                });

            _fakedisplay.DidNotReceive().ChargingMessage(Arg.Any<String>());
        }

        [TestCase(double.MaxValue)]
        [TestCase(500.1)]
        [TestCase(5.0)]
        [TestCase(4.9)]
        [TestCase(0.1)]
        public void RaiseEvent_ReadCurrentValue_StopChargeCalledOnce(double current)
        {
            _fakeUsbCharger.Connected.Returns(true);

            _fakeUsbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs()
                {
                    Current = current
                });

            _fakeUsbCharger.Received(1).StopCharge();
        }

        [TestCase(500.0)]
        [TestCase(5.1)]
        [TestCase(0.0)]
        [TestCase(-0.1)]
        [TestCase(double.MinValue)]
        public void RaiseEvent_ReadCurrentValue_StopChargeNotCalled(double current)
        {
            _fakeUsbCharger.Connected.Returns(true);

            _fakeUsbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs()
                {
                    Current = current
                });

            _fakeUsbCharger.DidNotReceive().StopCharge();
        }

        [TestCase(double.MaxValue)]
        [TestCase(500.1)]
        [TestCase(500.0)]
        [TestCase(5.1)]
        [TestCase(5.0)]
        [TestCase(4.9)]
        [TestCase(0.1)]
        [TestCase(0.0)]
        [TestCase(-0.1)]
        [TestCase(double.MinValue)]
        public void RaiseEvent_ReadCurrentValue_Disconnected_StopChargeNotCalled(double current)
        {
            _fakeUsbCharger.Connected.Returns(false);

            _fakeUsbCharger.CurrentValueEvent +=
                Raise.EventWith(new CurrentEventArgs()
                {
                    Current = current
                });

            _fakeUsbCharger.DidNotReceive().StopCharge();
        }

    }
}