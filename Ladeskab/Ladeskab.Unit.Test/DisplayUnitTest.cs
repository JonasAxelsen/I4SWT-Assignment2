using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    class DisplayUnitTest
    {
        private IDisplay _uut;

        [SetUp]
        public void Setup()
        {
            _uut = Substitute.For<IDisplay>();
        }

        [Test]
        public void Parameter_value_StationMessage_Did_Recieve()
        {
            _uut.StationMessage("Test");
            _uut.Received().StationMessage("Test");
        }

        [Test]
        public void Parameter_value_StationMessage_Did_not_Recieve()
        {
            _uut.StationMessage("DidNotTest");
            _uut.DidNotReceive().ChargingMessage("Test");
        }

        [Test]
        public void Parameter_value_ChargingMessage_Did_Recieve()
        {
            _uut.ChargingMessage("Test");
            _uut.Received().ChargingMessage("Test");
        }

        [Test]
        public void Parameter_value_ChargingMessage_Did_not_Recieve()
        {
            _uut.ChargingMessage("DidNotTest");
            _uut.DidNotReceive().ChargingMessage("Test");
        }

        [Test]
        public void Test_Chargeing_Did_Not_Call_Station()
        {
            _uut.ChargingMessage("Test");
            _uut.DidNotReceive().StationMessage("Test");
        }

        [Test]
        public void Test_StationMessage_Did_Not_Call_Charging()
        {
            _uut.StationMessage("Test");
            _uut.DidNotReceive().ChargingMessage("Test");
        }
    }
}
