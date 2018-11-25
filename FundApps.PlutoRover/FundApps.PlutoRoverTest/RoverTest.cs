using FundApps.PlutoRover;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundApps.PlutoRoverTest
{
    [TestClass]
    public class RoverTest
    {
        private Planisfere Planisfere
        {
            get { return new Planisfere(100, 100); }
        }

        [TestMethod]
        public void TestRotateLeft()
        {
            Rover rover = new Rover(Planisfere, new Position { Coordinate = new Coordinate { X = 0, Y = 0 }, Orientation = Orientation.North });
            Orientation expected = Orientation.West;

            var newOrientation = rover.Rotate(-1);

            Assert.AreEqual(expected, newOrientation);
        }

        [TestMethod]
        public void TestRotateRight()
        {
            Rover rover = new Rover(Planisfere, new Position { Coordinate = new Coordinate { X = 0, Y = 0 }, Orientation = Orientation.North });
            Orientation expected = Orientation.East;

            var newOrientation = rover.Rotate(1);

            Assert.AreEqual(expected, newOrientation);
        }

        [TestMethod]
        public void TestStepEdgeMax()
        {
            Rover rover = new Rover(Planisfere, new Position { Coordinate = new Coordinate { X = 0, Y = 99 }, Orientation = Orientation.North });
            int expectedY = 0;
            int expectedX = 0;

            var newOrientation = rover.Step();

            Assert.AreEqual(expectedY, newOrientation.Y);
            Assert.AreEqual(expectedX, newOrientation.X);
        }

        [TestMethod]
        public void TestStepEdgeMin()
        {
            Rover rover = new Rover(Planisfere, new Position { Coordinate = new Coordinate { X = 0, Y = 0 }, Orientation = Orientation.South });
            int expectedY = 99;
            int expectedX = 0;

            var newOrientation = rover.Step();

            Assert.AreEqual(expectedY, newOrientation.Y);
            Assert.AreEqual(expectedX, newOrientation.X);
        }
    }
}
