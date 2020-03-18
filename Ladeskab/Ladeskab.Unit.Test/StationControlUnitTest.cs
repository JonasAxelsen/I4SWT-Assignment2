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

        // RfidDetected_LadeskabIsAvailable_DeviceIsConnected
        // RfidDetected_LadeskabIsAvailable_DeviceIsConnected_LockDoor_Called
        // RfidDetected_LadeskabIsAvailable_DeviceIsConnected_StartCharge_Called
        // RfidDetected_LadeskabIsAvailable_DeviceIsConnected_DisplayStationMessage_Called
        // RfidDetected_LadeskabIsAvailable_DeviceIsConnected_LogDoorLocked_Called

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsConnected_LockDoor_Called()
        {
            // Arrange
            int testId = 3;

            // Act
            _fakeChargeControl.IsConnected().ReturnsForAnyArgs(true);
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));


            // Assert
            _fakeDoor.Received(1).LockDoor();
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsConnected_StartCharge_Called()
        {
            // Arrange
            int testId = 3;

            // Act
            _fakeChargeControl.IsConnected().ReturnsForAnyArgs(true);
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));


            // Assert
            _fakeChargeControl.Received(1).StartCharge();
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsConnected_DisplayStationMessage_Called()
        {
            // Arrange
            int testId = 3;

            // Act
            _fakeChargeControl.IsConnected().ReturnsForAnyArgs(true);
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));


            // Assert
            _fakeDisplay.Received(1).StationMessage(Arg.Is<string>(s => s.Contains("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.")));
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsConnected_LogDoorLocked_Called()
        {
            // Arrange
            int testId = 3;

            // Act
            _fakeChargeControl.IsConnected().ReturnsForAnyArgs(true);
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));


            // Assert
            _fakeLogger.Received(1).LogDoorLocked(testId);
        }

        // RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected
        // RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_LockDoor_NotCalled
        // RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_StartCharge_NotCalled
        // RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_DisplayStationMessage_Called
        // RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_LogDoorLocked_NotCalled

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_LockDoor_NotCalled()
        {
            // Arrange
            int testId = 3;

            // Act
            _fakeChargeControl.IsConnected().ReturnsForAnyArgs(false);
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));


            // Assert
            _fakeDoor.Received(1).LockDoor();
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_StartCharge_NotCalled()
        {
            // Arrange
            int testId = 3;

            // Act
            _fakeChargeControl.IsConnected().ReturnsForAnyArgs(false);
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));


            // Assert
            _fakeChargeControl.Received(1).StartCharge();
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_DisplayStationMessage_Called()
        {
            // Arrange
            int testId = 3;

            // Act
            _fakeChargeControl.IsConnected().ReturnsForAnyArgs(false);
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));


            // Assert
            _fakeDisplay.Received(1).StationMessage(Arg.Is<string>(s => s.Contains("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.")));
        }

        [Test]
        public void RfidDetected_LadeskabIsAvailable_DeviceIsNotConnected_LogDoorLocked_NotCalled()
        {
            // Arrange
            int testId = 3;

            // Act
            _fakeChargeControl.IsConnected().ReturnsForAnyArgs(false);
            _fakeRfidReader.RfidReadEvent += Raise.EventWith(_fakeRfidReader, new RfidReadEventArgs(testId));


            // Assert
            _fakeLogger.Received(1).LogDoorLocked(testId);
        }


        #region DoorEvents


        #endregion


        #region RfidEvents


        #endregion
    }
}