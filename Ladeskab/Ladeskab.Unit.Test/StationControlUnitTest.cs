using NSubstitute;
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
            _fakeChargeControl = Substitute.For<IChargeControl>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeLogger = Substitute.For<ILogger>();
            
            _uut = new StationControl(_fakeRfidReader, _fakeDoor, _fakeChargeControl,_fakeDisplay,_fakeLogger);
        }


        #region DoorEvents


        #endregion


        #region RfidEvents


        #endregion
    }
}