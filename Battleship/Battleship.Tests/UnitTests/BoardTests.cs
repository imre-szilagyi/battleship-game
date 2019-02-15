using System;
using System.Collections.Generic;
using System.Linq;
using Battleship.Logic;
using Battleship.Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Battleship.Tests.UnitTests
{
    [TestClass]
    public class BoardTests
    {

        [TestMethod]
        public void Constructor_InstantiatingBoardWith1Rows8Columns_BoardCotains8Cells()
        {
            //Act
            var board = new Board(8, 1);

            //Assert
            Assert.IsNotNull(board.Cells);
            Assert.IsTrue(board.Cells.Count() == 8);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Constructor_InstatiatingBoard27Columns_ThrowsExceptionBecauseThereAreOnly26Chars()
        {
            //Act
            var board = new Board(27, 1);
        }

        [TestMethod]
        public void Constructor_InstantiatingBoard26Columns_BoardCotainsCellsWithColumnHeadersATroughZ()
        {
            //Act
            var board = new Board(26, 1);

            //Assert
            for (char header = 'A'; header <= 'Z'; header++)
            {
                int numberOfCellsWithColumnHeader = board.Cells.Count(c => c.ColumnHeader == header);
                Assert.AreEqual(1, numberOfCellsWithColumnHeader);
            }
        }

        [TestMethod]
        public void Constructor_InstantiatingBoardColumns26Rows20_BoardContainsCellWithTheColumnsAndHeigth()
        {
            //Arrange
            int boardColumns = 26;
            int boardRows = 20;

            //Act
            var board = new Board(boardColumns, boardRows);

            //Assert
            var cell = board.Cells.FirstOrDefault(c => c.Row == boardRows && c.Column == boardColumns);
            Assert.IsNotNull(cell);
        }

        [TestMethod]
        public void Bomb_Board1x1BombA1_ReturnsBombResultMiss()
        {
            //Arrange
            int boardColumns = 1;
            int boardRows = 1;
            var board = new Board(boardColumns, boardRows);

            //Act
            var result = board.Bomb('A', boardRows);

            //Assert
            Assert.AreEqual(BombingResult.Miss, result);
        }

        [TestMethod]
        public void Bomb_Board5x5WithAShipSize2PlacedAtB3B2BombB3_ReturnsBombResultHit()
        {
            //Arrange
            int boardColumns = 5;
            int boardRows = 5;
            int bombedRow = 3;
            char bombedCol = 'B';
            var board = new Board(boardColumns, boardRows);
            var ship = new Ship("Test", 2);
            ship.OccupiedCells = board.Cells.Where(c => c.ColumnHeader == 'B' && (c.Row == 3 || c.Row == 2));
            board.Ships = new List<Ship> { ship };
            var bombedCell = board.Cells.First(c => c.Row == bombedRow && c.ColumnHeader == bombedCol);

            //Act
            var result = board.Bomb('B', 3);

            //Assert
            Assert.IsTrue(bombedCell.IsBombed);
            Assert.AreEqual(BombingResult.Hit, result);
        }

        [TestMethod]
        public void Bomb_Board5x5WithAShipSize1PlacedAtB3BombB3_ReturnsBombResultAllShipsSink()
        {
            //Arrange
            int boardColumns = 5;
            int boardRows = 5;
            var board = new Board(boardColumns, boardRows);
            var ship = new Ship("Test", 1);
            ship.OccupiedCells = board.Cells.Where(c => c.ColumnHeader == 'B' && c.Row == 3);
            board.Ships = new List<Ship> { ship };

            //Act
            var result = board.Bomb('B', 3);

            //Assert
            Assert.AreEqual(BombingResult.AllShipsSunk, result);
        }

        [TestMethod]
        public void Bomb_Board5x5With2ShipsSize1PlacedAtB3AndB1BombB3_ReturnsBombResultAllShipsSink()
        {
            //Arrange
            int boardColumns = 5;
            int boardRows = 5;
            var board = new Board(boardColumns, boardRows);
            var bombedShip = new Ship("Test 2", 1) { OccupiedCells = board.Cells.Where(c => c.ColumnHeader == 'B' && c.Row == 3) };
            board.Ships = new List<Ship>
            {
                new Ship("Test 1", 1) { OccupiedCells = board.Cells.Where(c => c.ColumnHeader == 'B' && c.Row == 1) },
                bombedShip
            };

            //Act
            var result = board.Bomb('B', 3);

            //Assert
            Assert.IsTrue(bombedShip.IsSunk);
            Assert.AreEqual(BombingResult.Sink, result);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Bomb_Board1x1BombA2_ThrowsIndexOutOfRangeExceptionBecauseCoordsAreOutsideOfBounds()
        {
            //Arrange
            var board = new Board(1, 1);

            //Act
            board.Bomb('A', 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Bomb_Board1x1BombA1x2_ThrowsArgumentExceptionBecauseCellWasAlreadyBombed()
        {
            //Arrange
            var board = new Board(1, 1);

            //Act
            board.Bomb('A', 1);
            board.Bomb('A', 1);
        }
    }
}
