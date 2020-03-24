using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    class LoggerUnitTest
    {
        private Logger _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Logger();
        }

        [Test]
        public void LogDoorUnlocked_()
        {
            // Arrange
            int id = 3;
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                // Act
                _uut.LogDoorUnlocked(id, writer);

                // Assert
                string actual = Encoding.UTF8.GetString(stream.ToArray());
                Assert.AreEqual($"Door Unlocked at {DateTime.Now:T} by {id}\r\n", actual);
            }
        }
    }
}
