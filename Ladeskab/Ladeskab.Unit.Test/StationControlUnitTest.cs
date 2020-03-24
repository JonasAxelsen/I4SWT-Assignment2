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
            _fakeDoor.DoorOpenEvent += Raise.EventWith(_fakeDoor, new DoorOpenEventArgs());
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(3));
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

        #region Locked

        #region CorrectId

        // RfidDetected_Locked_CorrectId
        // RfidDetected_Locked_CorrectId_StopCharge_Called
        // RfidDetected_Locked_CorrectId_UnlockDoor_Called
        // RfidDetected_Locked_CorrectId_LogDoorUnlocked_Called
        // RfidDetected_Locked_CorrectId_StationMessage_Called

        public void RfidDetected_Locked_CorrectId()
        {
            // Arrange
            int testId = 3;

            // Act
            _fakeChargeControl.IsConnected().ReturnsForAnyArgs(true);
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));
            // Nu burde den være i Locked mode.
            // Vi kalder event igen med samme rfid, så burde den låse op og være available
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));
        }

        [Test]
        public void RfidDetected_Locked_CorrectId_StopCharge_Called()
        {
            RfidDetected_Locked_CorrectId();

            // Assert
            _fakeChargeControl.Received(1).StopCharge();
        }

        [Test]
        public void RfidDetected_Locked_CorrectId_UnlockDoor_Called()
        {
            RfidDetected_Locked_CorrectId();

            // Assert
            _fakeDoor.Received(1).UnlockDoor();
        }

        [Test]
        public void RfidDetected_Locked_CorrectId_LogDoorUnlocked_Called()
        {
            RfidDetected_Locked_CorrectId();

            // Assert
            _fakeLogger.ReceivedWithAnyArgs(1).LogDoorUnlocked(1);
        }

        [Test]
        public void RfidDetected_Locked_CorrectId_StationMessage_Called()
        {
            RfidDetected_Locked_CorrectId();

            // Assert
            _fakeDisplay.Received(1).StationMessage("Tag din telefon ud af skabet og luk døren");
        }



        #endregion

        #region WrongId

        // RfidDetected_Locked_WrongId 
        // RfidDetected_Locked_WrongId_StopCharge_NotCalled
        // RfidDetected_Locked_WrongId_UnlockDoor_NotCalled
        // RfidDetected_Locked_WrongId_LogDoorUnlocked_NotCalled
        // RfidDetected_Locked_WrongId_StationMessage_Called

        public void RfidDetected_Locked_WrongId()
        {
            // Arrange
            int testId = 3;

            // Act
            _fakeChargeControl.IsConnected().ReturnsForAnyArgs(true);
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));
            // Nu burde den være i Locked mode.
            // Vi kalder event igen med andet rfid, så burde den ikke låse op
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId-1));
        }

        [Test]
        public void RfidDetected_Locked_WrongId_StopCharge_NotCalled()
        {
            RfidDetected_Locked_WrongId();

            // Assert
            _fakeChargeControl.DidNotReceive().StopCharge();
        }

        [Test]
        public void RfidDetected_Locked_WrongId_UnlockDoor_NotCalled()
        {
            RfidDetected_Locked_WrongId();

            // Assert
            _fakeDoor.DidNotReceive().UnlockDoor();
        }

        [Test]
        public void RfidDetected_Locked_WrongId_LogDoorUnlocked_NotCalled()
        {
            RfidDetected_Locked_WrongId();

            // Assert
            _fakeLogger.DidNotReceiveWithAnyArgs().LogDoorUnlocked(1);
        }

        [Test]
        public void RfidDetected_Locked_WrongId_StationMessage_Called()
        {
            RfidDetected_Locked_WrongId();

            // Assert
            _fakeDisplay.Received(1).StationMessage("Forkert RFID tag");
        }

        #endregion

        #endregion


        #endregion


        #region DoorEvents

        [Test]
        public void DoorOpened_StationMessage_Called()
        {
            // Arrange

            // Act
            _fakeDoor.DoorOpenEvent += Raise.EventWith(_fakeDoor, new DoorOpenEventArgs());

            // Assert
            _fakeDisplay.Received(1).StationMessage("Tilslut telefon!");

        }

        [Test]
        public void DoorClosed_StationMessage_Called()
        {
            // Act
            _fakeDoor.DoorOpenEvent += Raise.EventWith(_fakeDoor, new DoorOpenEventArgs());
            _fakeDoor.DoorCloseEvent += Raise.EventWith(_fakeDoor, new DoorCloseEventArgs());

            // Assert
            _fakeDisplay.Received(1).StationMessage("Indlæs RFID");
        }

        #endregion

    }
}