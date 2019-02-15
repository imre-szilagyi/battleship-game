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
        public BombingResult Bomb(char column, int row)
        {
            return _board.Bomb(column, row);
        }
    }
}
