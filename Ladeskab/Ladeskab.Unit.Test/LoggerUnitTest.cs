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
    class LoggerUnitTest_Unlocked
    {
        private Logger _uut;
        private string _logFile;

        [SetUp]
        public void Setup()
        {
            _logFile = "../../logfile.txt";
            _uut = new Logger(_logFile);


            if (File.Exists(_logFile))
            {
                File.Delete(_logFile);
            }
        }

        [Test]
        public void LogDoorUnlocked_FirstRun_WritesToFile()
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

        [Test]
        public void LogDoorUnlocked_HasRunBefore_WritesToFile()
        {
            // Arrange
            int id = 3;
            File.Create(_logFile);

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

    [TestFixture]
    class LoggerUnitTest_Locked
    {
        private Logger _uut;
        private string _logFile;

        [SetUp]
        public void Setup()
        {
            _logFile = "../../logfile2.txt";
            _uut = new Logger(_logFile);


            if (File.Exists(_logFile))
            {
                File.Delete(_logFile);
            }
        }

        [Test]
        public void LogDoorLocked_FirstRun_WritesToFile()
        {
            // Arrange
            int id = 3;

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                // Act
                _uut.LogDoorLocked(id, writer);

                // Assert
                string actual = Encoding.UTF8.GetString(stream.ToArray());
                Assert.AreEqual($"Door locked at {DateTime.Now:T} by {id}\r\n", actual);
            }
        }

        [Test]
        public void LogDoorLocked_HasRunBefore_WritesToFile()
        {
            // Arrange
            int id = 3;
            File.Create(_logFile);

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))

            {
                // Act
                _uut.LogDoorLocked(id, writer);

                // Assert
                string actual = Encoding.UTF8.GetString(stream.ToArray());
                Assert.AreEqual($"Door locked at {DateTime.Now:T} by {id}\r\n", actual);
            }
        }
    }
}
