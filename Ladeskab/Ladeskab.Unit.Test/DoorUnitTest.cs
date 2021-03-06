﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    public class DoorUnitTest
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

        #region OpenDoor
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
        public void OpenDoor_CalledFromUnlockedObject_OpenEventFired()
        {
            _uut.LockDoor();
            _uut.UnlockDoor();
            _uut.OpenDoor();

            Assert.That(_recivedOpenEventArgs, Is.Not.Null);
        }

        [Test]
        public void OpenDoor_CalledFromUnlockedObject_CloseEventNotFired()
        {
            _uut.LockDoor();
            _uut.UnlockDoor();
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
        #endregion


        #region CloseDoor
        public void SetupCloseDoor_OpenDoor_ResetEventListener()
        {
            _uut.OpenDoor();
            _recivedCloseEventArgs = null;
            _recivedOpenEventArgs = null;
        }

        [Test]
        public void CloseDoor_CalledFromNewObject_OpenEventNotFired()
        {
            SetupCloseDoor_OpenDoor_ResetEventListener();
            _uut.CloseDoor();


            Assert.That(_recivedOpenEventArgs, Is.Null);
        }

        [Test]
        public void CloseDoor_CalledFromNewObject_CloseEventFired()
        {
            SetupCloseDoor_OpenDoor_ResetEventListener();
            _uut.CloseDoor();

            Assert.That(_recivedCloseEventArgs, Is.Not.Null);
        }

        [Test]
        public void CloseDoor_CalledLockedonOpenDoor_ThrowExceptionType()
        {
            SetupCloseDoor_OpenDoor_ResetEventListener();

            Assert.That(() => _uut.LockDoor(),
                Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void CloseDoor_CalledLockedonOpenDoor_ThrowExceptionMessage()
        {
            SetupCloseDoor_OpenDoor_ResetEventListener();

            var ex = Assert.Catch(() => _uut.LockDoor());

            Assert.That(ex.Message, Is.EqualTo("Cannot lock an open door."));
        }

        #endregion
    }
}
