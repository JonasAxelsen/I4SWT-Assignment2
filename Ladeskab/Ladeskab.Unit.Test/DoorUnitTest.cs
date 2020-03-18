using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{

    [TestFixture]
    public class DoorUnitTestOpenDoor
    {
        private Door _uut;
        private DoorCloseEventArgs _recivedCloseEventArgs;
        private DoorOpenEventArgs _recivedOpenEventArgs;

        [SetUp]
        public void Setup()
        {
            _recivedCloseEventArgs = null;
            _recivedOpenEventArgs = null;

            _uut = new Door();

            _uut.DoorCloseEvent += (o, args) => { _recivedCloseEventArgs = args; };
            _uut.DoorOpenEvent += (o, args) => { _recivedOpenEventArgs = args; };
        }

        [Test]
        public void OpenDoor_CalledFromLockedObject_OpenEventNotFired()
        {
            _uut.LockDoor();
            _uut.OpenDoor();

            Assert.That(_recivedOpenEventArgs, Is.Null);
        }

        [Test]
        public void OpenDoor_CalledFromLockedObject_CloseEventNotFired()
        {
            _uut.LockDoor();
            _uut.OpenDoor();

            Assert.That(_recivedCloseEventArgs, Is.Null);
        }

        [Test]
        public void OpenDoor_CalledFromNewObject_OpenEventFired()
        {
            _uut.OpenDoor();

            Assert.That(_recivedOpenEventArgs, Is.Not.Null);
        }

        [Test]
        public void OpenDoor_CalledFromNewObject_CloseEventNotFired()
        {
            _uut.OpenDoor();

            Assert.That(_recivedCloseEventArgs, Is.Null);
        }
    }

    public class DoorUnitTestCloseDoor
    {
        private Door _uut;
        private DoorCloseEventArgs _recivedCloseEventArgs;
        private DoorOpenEventArgs _recivedOpenEventArgs;

        [SetUp]
        public void Setup()
        {

            _uut = new Door();
            _uut.OpenDoor();

            _recivedCloseEventArgs = null;
            _recivedOpenEventArgs = null;

            _uut.DoorCloseEvent += (o, args) => { _recivedCloseEventArgs = args; };
            _uut.DoorOpenEvent += (o, args) => { _recivedOpenEventArgs = args; };
        }

        [Test]
        public void CloseDoor_CalledFromNewObject_OpenEventNotFired()
        {
            _uut.CloseDoor();


            Assert.That(_recivedOpenEventArgs, Is.Null);
        }

        [Test]
        public void CloseDoor_CalledFromNewObject_CloseEventFired()
        {
            _uut.CloseDoor();

            Assert.That(_recivedCloseEventArgs, Is.Not.Null);
        }

        [Test]
        public void CloseDoor_CalledLockedonOpenDoor_ThrowException()
        {
            Assert.That(()=>_uut.LockDoor(),
                Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void CloseDoor_CalledLockedonOpenDoor_ThrowExceptionMessage()
        {
            var ex = Assert.Catch(() => _uut.LockDoor());


            Assert.That(ex.Message,Is.EqualTo("Cannot lock an open door."));
        }
    }
}
