using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ECSWithEvents.Test.Unit
{
    [TestFixture()]
    class TestTempSensor
    {
        private TempSensor _uut;
        private TempChangedEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;

            _uut = new TempSensor();
            _uut.SetTemp(20);

            // Set up an event listener to check the event occurrence and event data
            _uut.TempChangedEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };
        }

        [Test]
        public void SetTemp_TempSetToNewValue_EventFired()
        {
            _uut.SetTemp(25);
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void SetTemp_TempSetToNewValue_CorrectNewTempReceived()
        {
            _uut.SetTemp(25);
            Assert.That(_receivedEventArgs.Temp, Is.EqualTo(25));
        }

        [Test]
        public void SetTemp_SameTemperature_EventNotFired()
        {
            _uut.SetTemp(20);
            Assert.That(_receivedEventArgs, Is.Null);
        }
    }
}
