using Battleship.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Tests.UnitTests
{
    [TestClass]
    public class ShipRandomPlacerTests
    {
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void PlaceShips_ShipSizeBiggerThanNumberOfRowsAndColumns_ArgumentExpcetion()
        {
            //Arrange
            var ships = new List<Ship>() { new Ship("Test", 8) };
            var board = new Board(7, 7);
            var placer = new ShipRandomPlacer();

            //Act
            placer.PlaceShips(ships, board.Cells);
        }

        [TestMethod]
        public void PlaceShips_ShipSizeBiggerThanNumberOfColumnsButNotBiggerThenNumberOfRows_ShipIsPlacedOnBoard()
        {
            //Arrange
            var ship = new Ship("Test", 8);
            var ships = new List<Ship>() { ship };
            var board = new Board(8, 1);
            var placer = new ShipRandomPlacer();

            //Act
            placer.PlaceShips(ships, board.Cells);

            //Assert
            Assert.IsNotNull(ship.OccupiedCells);
            Assert.IsTrue(ship.OccupiedCells.Any());
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void PlaceShips_NMoreShipsInTheListThanNumberThanNumberOfColumnsAndRows_ArgumentExpcetion()
        {
            //Arrange
            var ships = new List<Ship>() { new Ship("Test", 1), new Ship("Test", 1) };
            var board = new Board(1, 1);
            var placer = new ShipRandomPlacer();

            //Act
            placer.PlaceShips(ships, board.Cells);
        }

        [TestMethod]
        public void PlaceShips_MoreShipsInTheListThanNumberOfColumnsButNotRows_AllShipsArePlaced()
        {
            //Arrange
            var ships = new List<Ship>() { new Ship("Test", 1), new Ship("Test", 1) };
            var board = new Board(2, 1);
            var placer = new ShipRandomPlacer();

            //Act
            placer.PlaceShips(ships, board.Cells);

            //Assert
            Assert.IsTrue(ships.All(s => s.OccupiedCells != null));
            Assert.IsTrue(ships.All(s => s.OccupiedCells.Any()));
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
        public void PlaceShips_ShipSize5NumberOfShips5BoardSize5x5_ShipOccupiedCellsAreNotEmpty()
        {
            //Arrange
            var ships = new List<Ship>() { new Ship("Test", 5), new Ship("Test", 5), new Ship("Test", 5) , new Ship("Test", 5) , new Ship("Test", 5) };
            var board = new Board(5, 5);
            var placer = new ShipRandomPlacer();

            //Act
            placer.PlaceShips(ships, board.Cells);

            //Assert
            Assert.IsTrue(ships.All(s => s.OccupiedCells.Any()));
        }

        [TestMethod]
        public void PlaceShips_DifferentShipSizesNumberOfShips5BoardSize5x5_ShipOccupiedCellsAreNotEmpty()
        {
            //Arrange
            var ships = new List<Ship>() { new Ship("Test", 3), new Ship("Test", 2), new Ship("Test", 2), new Ship("Test", 1), new Ship("Test", 5) };
            var board = new Board(5, 3);
            var placer = new ShipRandomPlacer();

            //Act
            placer.PlaceShips(ships, board.Cells);

            //Assert
            Assert.IsTrue(ships.All(s => s.OccupiedCells.Any()));
        }

    }
}
