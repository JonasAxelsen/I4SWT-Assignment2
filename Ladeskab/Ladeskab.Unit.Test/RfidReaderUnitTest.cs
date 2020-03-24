using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    class RfidReaderUnitTest
    {
        private RfidReader _uut;
        private RfidReadEventArgs _receivedRfidReadEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedRfidReadEventArgs = null;
            _uut = new RfidReader();

            _uut.RfidReadEvent += (o, args) => { _receivedRfidReadEventArgs = args; };
        }

        [TestCase(int.MaxValue)]
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void ReadRfid_RfidReadEventFired(int id)
        {
            _uut.ReadRfid(id);

            Assert.That(_receivedRfidReadEventArgs, Is.Not.Null);
        }

        [TestCase(int.MaxValue)]
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void ReadRfid_RfidReadEvent_WithRightArgs(int id)
        {   
            _uut.ReadRfid(id);

            Assert.That(_receivedRfidReadEventArgs.Id, Is.EqualTo(id));
        }
    }
}
