using Battleship.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Tests.UnitTests
{
    [TestClass]
    public class ShipRandomPlacerTests
    {
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void PlaceShips_ShipSizeBiggerThanNumberOfRows_ArgumentExpcetion()
        {
            //Arrange
            var ships = new List<Ship>() { new Ship("Test", 8) };
            var board = new Board(10, 7);
            var placer = new ShipRandomPlacer();

            //Act
            placer.PlaceShips(ships, board.Cells);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void PlaceShips_ShipSizeBiggerThanNumberOfColumns_ArgumentExpcetion()
        {
            //Arrange
            var ships = new List<Ship>() { new Ship("Test", 8) };
            var board = new Board(7, 10);
            var placer = new ShipRandomPlacer();

            //Act
            placer.PlaceShips(ships, board.Cells);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void PlaceShips_NumberOfShipsBiggerThanNumberOfColumns_ArgumentExpcetion()
        {
            //Arrange
            var ships = new List<Ship>() { new Ship("Test", 1), new Ship("Test", 1) };
            var board = new Board(1, 1);
            var placer = new ShipRandomPlacer();

            //Act
            placer.PlaceShips(ships, board.Cells);
        }

        [TestMethod]
        public void PlaceShips_ShipSize1BoardSize1x1_ShipOccupiedCellsContainsSingleCell()
        {
            //Arrange
            var ship = new Ship("Test", 1);
            var ships = new List<Ship>() { ship };
            var board = new Board(1, 1);
            var cell = board.Cells.First();
            var placer = new ShipRandomPlacer();

            //Act
            placer.PlaceShips(ships, board.Cells);

            //Assert
            Assert.AreEqual(1, board.Cells.Count());
            Assert.AreEqual(1, ship.Size);
            Assert.AreEqual(cell, ship.OccupiedCells.First());
        }

        [TestMethod]
        public void PlaceShips_ShipSize1NumberOfShips2BoardSize2x2_ShipOccupiedCellsAreNotEmpty()
        {
            //Arrange
            var ships = new List<Ship>() { new Ship("Test", 1), new Ship("Test", 1) };
            var board = new Board(2, 2);
            var cell = board.Cells.First();
            var placer = new ShipRandomPlacer();

            //Act
            placer.PlaceShips(ships, board.Cells);

            //Assert
            Assert.IsTrue(ships.All(s => s.OccupiedCells.Any()));
        }
        
    }
}
