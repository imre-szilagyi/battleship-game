using System;
using System.Linq;
using System.Windows.Input;
using Battleship.Logic;
using Battleship.Wpf.GameBoard.Models;

namespace Battleship.Wpf.GameBoard.Commands
{
    internal class RefreshCommand : ICommand
    {
        private GameBoardViewModel _viewModel;

        public event EventHandler CanExecuteChanged;

        public RefreshCommand(GameBoardViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var cells = Setup.GetBoard();
            var groupedCells = cells.Select(c => new BoardTile(c)).GroupBy(c => c.Row);

            _viewModel.Tiles = groupedCells;
        }
    }
}
