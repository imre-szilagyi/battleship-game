using Battleship.Logic.Models;
using System;

namespace Battleship.Logic.Interfaces
{
    public interface IPlayer
    {
        IBoard Board { get; }

        /// <summary>
        /// Call this method when a cell is bombed
        /// </summary>
        /// <returns>The Result of the Bombing: Miss, Hit, Sink, AllShipsSunk</returns>
        /// <exception cref="System.ArgumentException">Thrown when these coordinates where already bombed.</exception>
        /// <exception cref="System.IndexOutOfRangeException">Thrown when the row or column is out of bounds</exception>
        BombingResult Bomb(char column, int row);
    }
}
