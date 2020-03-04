using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace ECSWithEvents.Test.Unit
{
    [TestFixture]
    public class TestECSControl
    {
        private IHeater _heater;
        private ITempSensor _tempSource;

        private ECSControl _uut;

        [SetUp]
        public void Setup()
        {
            _heater = Substitute.For<IHeater>();
            _tempSource = Substitute.For<ITempSensor>();

            _uut = new ECSControl(25, _tempSource, _heater);
        }

        [TestCase(25)]
        [TestCase(20)]
        [TestCase(30)]
        public void TemperatureChanged_DifferentArguments_CurrentTemperatureIsCorrect(int newTemp)
        {
            _tempSource.TempChangedEvent += Raise.EventWith(new TempChangedEventArgs { Temp = newTemp });
            Assert.That(_uut.CurrentTemperature, Is.EqualTo(newTemp));
        }

        [TestCase(-5)]
        [TestCase(0)]
        [TestCase(24)]
        public void TemperatureChanged_LowTemperatur_HeaterOnCalled(int newTemp)
        {
            _tempSource.TempChangedEvent += Raise.EventWith(new TempChangedEventArgs { Temp = newTemp });
            _heater.Received(1).TurnOn();
        }

        [TestCase(-5)]
        [TestCase(0)]
        [TestCase(24)]
        public void TemperatureChanged_LowTemperatur_HeaterOffNotCalled(int newTemp)
        {
            _tempSource.TempChangedEvent += Raise.EventWith(new TempChangedEventArgs { Temp = newTemp });
            _heater.DidNotReceive().TurnOff();
        }

        [TestCase(25)]
        [TestCase(26)]
        [TestCase(30)]
        [TestCase(100)]
        public void TemperatureChanged_HighOrEqualTemperatur_HeaterOffCalled(int newTemp)
        {
            _tempSource.TempChangedEvent += Raise.EventWith(new TempChangedEventArgs { Temp = newTemp });
            _heater.Received(1).TurnOff();
        }

        [TestCase(25)]
        [TestCase(26)]
        [TestCase(30)]
        [TestCase(100)]
        public void TemperatureChanged_HighOrEqualTemperatur_HeaterOnNotCalled(int newTemp)
        {
            _tempSource.TempChangedEvent += Raise.EventWith(new TempChangedEventArgs { Temp = newTemp });
            _heater.DidNotReceive().TurnOn();
        }


    }
}
