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
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        ///     Thrown when the row or column is out of bounds
        ///     Or when these coordinates where already bombed.
        /// </exception>
        BombingResult Bomb(int row, char column);
    }
}
