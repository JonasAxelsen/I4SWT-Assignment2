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
        public void StationMessage_WritesToConsole()
        {
            // Arrange
            string message = "testy";

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

        [Test]
        public void ChargingMessage_WritesToConsole()
        {
            // Arrange
            string message = "test";

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                // Act
                _uut.ChargingMessage(message, writer);

                // Assert
                string actual = Encoding.UTF8.GetString(stream.ToArray());
                Assert.That(actual, Is.EqualTo(message));
            }
        }

    }
}
