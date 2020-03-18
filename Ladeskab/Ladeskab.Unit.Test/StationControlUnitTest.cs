using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    [TestFixture] 
    public class StationControlUnitTest
    {
        private StationControl _uut;
        private IRfidReader _fakeRfidReader;
        private IDoor _fakeDoor;
        private IChargeControl _fakeChargeControl;
        private IDisplay _fakeDisplay;
        private ILogger _fakeLogger;


        [SetUp]
        public void Setup()
        {
            _fakeRfidReader = Substitute.For<IRfidReader>();
            _fakeDoor = Substitute.For<IDoor>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeChargeControl = Substitute.For<IChargeControl>();
            _fakeLogger = Substitute.For<ILogger>();
            
            _uut = new StationControl(_fakeRfidReader, _fakeDoor, _fakeChargeControl,_fakeDisplay,_fakeLogger);
        }

        #region RfidDetected


        #region LadeskabIsAvailable

        #region DeviceIsConnected


        // RfidDetected_LadeskabIsAvailable_DeviceIsConnected
        // RfidDetected_LadeskabIsAvailable_DeviceIsConnected_LockDoor_Called
        // RfidDetected_LadeskabIsAvailable_DeviceIsConnected_StartCharge_Called
        // RfidDetected_LadeskabIsAvailable_DeviceIsConnected_DisplayStationMessage_Called
        // RfidDetected_LadeskabIsAvailable_DeviceIsConnected_LogDoorLocked_Called

        public int RfidDetected_LadeskabIsAvailable_DeviceIsConnected()
        {
            // Arrange
            int testId = 3;

            // Act
            _fakeChargeControl.IsConnected().ReturnsForAnyArgs(true);
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));
            
            return testId;
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsConnected_LockDoor_Called()
        {
            RfidDetected_LadeskabIsAvailable_DeviceIsConnected();

            // Assert
            _fakeDoor.Received(1).LockDoor();
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsConnected_StartCharge_Called()
        {
            RfidDetected_LadeskabIsAvailable_DeviceIsConnected();

            // Assert
            _fakeChargeControl.Received(1).StartCharge();
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsConnected_DisplayStationMessage_Called()
        {
            RfidDetected_LadeskabIsAvailable_DeviceIsConnected();

            // Assert
            _fakeDisplay.Received(1).StationMessage(Arg.Is<string>(s => s.Contains("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.")));
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsConnected_LogDoorLocked_Called()
        {
            int testId = RfidDetected_LadeskabIsAvailable_DeviceIsConnected();

            // Assert
            _fakeLogger.Received(1).LogDoorLocked(testId);
        }

        #endregion

        #region DeviceIsNotConnected

        // RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected
        // RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_LockDoor_NotCalled
        // RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_StartCharge_NotCalled
        // RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_DisplayStationMessage_Called
        // RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_LogDoorLocked_NotCalled

        public int RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected()
        {
            // Arrange
            int testId = 3;

            // Act
            _fakeChargeControl.IsConnected().ReturnsForAnyArgs(false);
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));

            return testId;
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_LockDoor_NotCalled()
        {
            RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected();

            // Assert
            _fakeDoor.DidNotReceive().LockDoor();
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_StartCharge_NotCalled()
        {
            RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected();

            // Assert
            _fakeChargeControl.DidNotReceive().StartCharge();
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_DisplayStationMessage_Called()
        {
            RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected();

            // Assert
            _fakeDisplay.Received(1).StationMessage(Arg.Is<string>(s => s.Contains("Din telefon er ikke ordentlig tilsluttet. Prøv igen.")));
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_LogDoorLocked_NotCalled()
        {
            int testId = RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected();

            // Assert
            _fakeLogger.DidNotReceive().LogDoorLocked(testId);
        }

        #endregion

        #endregion


        #region DoorOpen

        // RfidDetected_DoorOpen
        // RfidDetected_DoorOpen_LockDoor_NotCalled
        // RfidDetected_DoorOpen_StartCharge_NotCalled
        // RfidDetected_DoorOpen_DisplayStationMessage_NotCalled
        // RfidDetected_DoorOpen_LogDoorLocked_NotCalled

        public void RfidDetected_DoorOpen()
        {
            // Arrange

            // Act
            _fakeDoor.OpenDoor();
        }

        [Test]
        public void RfidDetected_DoorOpen_LockDoor_NotCalled()
        {
            RfidDetected_DoorOpen();

            // Assert
            _fakeDoor.DidNotReceive().LockDoor();
        }

        [Test]
        public void RfidDetected_DoorOpen_StartCharge_NotCalled()
        {
            RfidDetected_DoorOpen();

            // Assert
            _fakeChargeControl.DidNotReceive().StartCharge();
        }

        [Test]
        public void RfidDetected_DoorOpen_DisplayStationMessage_NotCalled()
        {
            RfidDetected_DoorOpen();

            // Assert
            _fakeDisplay.DidNotReceive().StationMessage("d");
        }

        [Test]
        public void RfidDetected_DoorOpen_LogDoorLocked_NotCalled()
        {
            RfidDetected_DoorOpen();

            // Assert
            _fakeLogger.DidNotReceiveWithAnyArgs().LogDoorLocked(1);
        }

        #endregion




        #endregion


        #region DoorEvents


        #endregion


        #region RfidEvents


        #endregion
    }
}