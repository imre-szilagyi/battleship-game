using Battleship.Logic.Interfaces;
using Battleship.Logic.Models;

namespace Battleship.Logic.Players
{
    internal class SinglePlayerHuman : IPlayer
    {
        private Board _board;

        internal SinglePlayerHuman(Board board)
        {
            _board = board;
        }

        public IBoard Board => _board;
        public BombingResult Bomb(int row, char column)
        {
            return _board.Bomb(row, column);
        }
    }
}
