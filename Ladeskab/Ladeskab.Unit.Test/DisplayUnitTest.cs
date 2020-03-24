using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    class DisplayUnitTest
    {
        private Display _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Display();

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


        [Test]
        public void StationMessage_WritesToConsole()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.console.setout?view=netframework-4.8
            // Arrange
            string message = "test";

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                // Act
                _uut.StationMessage(message, writer);

                // Assert
                string actual = Encoding.UTF8.GetString(stream.ToArray());
                Assert.That(actual, Is.EqualTo(message));
            }
        }


    }
}
