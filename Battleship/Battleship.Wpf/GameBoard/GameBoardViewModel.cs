using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Battleship.Logic;
using Battleship.Wpf.GameBoard.Commands;
using Battleship.Wpf.GameBoard.Models;

namespace Battleship.Wpf.GameBoard
{
    internal class GameBoardViewModel : INotifyPropertyChanged
    {
        private bool _showHiddenShips;
        public Board Board { get; private set; }

        public GameBoardViewModel()
        {
            RefreshCommand = new RelayCommand(OnRefreshCommand);
            BombCommand = new RelayCommand(OnBombTileCommand);
            OnRefreshCommand(null);
        }

        public ICommand BombCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        public string Coordinates { get; set; }
        public string StatusMessage { get; private set; }
        public IEnumerable<char> HeaderCharacters { get; private set; }
        public IEnumerable<int> HeaderNumbers { get; private set; }
        public IEnumerable<BoardTile> Tiles { get; private set; }
        public IEnumerable<IGrouping<int, BoardTile>> GroupedTiles { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public bool ShowHiddenShips
        {
            get { return _showHiddenShips; }
            set
            {
                _showHiddenShips = value;
                ToggleShipVisibility();
            }
        }


        internal void OnRefreshCommand(object param)
        {
            RefreshTiles();
            RefreshGroupedTiles();
            RefreshHeaders();           
            RaisePropertyChanged(null);
            UpdateStatusMessage("Refreshed!");
        }

        private void ToggleShipVisibility()
        {
            foreach (var tile in Tiles)
            {
                tile.ShowShip = _showHiddenShips;
                tile.RefreshTile();
            }
        }

        private void RefreshGroupedTiles()
        {
            GroupedTiles = Tiles.GroupBy(t => t.Row).OrderBy(t => t.Key);
        }

        private void RefreshTiles()
        {
            if (Board != null)
                CleanupExistingBoard();

            Board = Setup.GetBoard();
            Board.ShipHit += OnShipHit;
            Board.ShipMiss += OnShipMiss;
            Board.ShipSunk += OnShipSunk;
            Board.GameOver += OnGameOver;
            Tiles = Board.Cells.Select(c => new BoardTile(c, OnTileClickedCallback) { ShowShip = _showHiddenShips }).ToList();
        }

        private void CleanupExistingBoard()
        {
            Board.ShipHit -= OnShipHit;
            Board.ShipMiss -= OnShipMiss;
            Board.ShipSunk -= OnShipSunk;
            Board.GameOver -= OnGameOver;
        }

        private void OnGameOver(object sender, bool e)
        {
            UpdateStatusMessage("Game Over - Victory!");
        }

        private void OnShipSunk(object sender, Ship e)
        {
            UpdateStatusMessage("Ship sunk!");
        }

        private void OnShipMiss(object sender, EventArgs e)
        {
            UpdateStatusMessage("Missed!");
        }

        private void OnShipHit(object sender, Ship e)
        {
            UpdateStatusMessage("Ship hit!");
        }

        private void OnTileClickedCallback(BoardTile tile)
        {
            if (tile.Cell.IsBombed)
            {
                UpdateStatusMessage("Coordinates already bombed!");
                return;
            }

            if (Board.Bomb(tile.Row, tile.Column) == false)
            {
                UpdateStatusMessage("Incorrect coordinates!");
                return;
            }

            tile.RefreshTile();
        }


        private void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void RefreshHeaders()
        {
            HeaderCharacters = Tiles.Select(t => t.Column).Distinct();
            HeaderNumbers = Tiles.Select(t => t.Row).Distinct();
        }

        private void OnBombTileCommand(object obj)
        {
            if (string.IsNullOrEmpty(Coordinates))
            {
                UpdateStatusMessage("First enter coordinates to bomb!");
                return;
            }

            var coords = Coordinates.ToUpper();
            char column;
            int row;
            if (TryParseCoordinates(coords, out column, out row) == false)
            {
                UpdateStatusMessage("Incorrect coordinates format, please specify column then row.");
                return;
            }

            var tile = Tiles.FirstOrDefault(t => t.Row == row && t.Column == column);
            if (tile == null)
            {
                UpdateStatusMessage("Incorrect coordinates!");
                return;
            }

            OnTileClickedCallback(tile);
        }

        private bool TryParseCoordinates(string coordinates, out char column, out int row)
        {
            column = ' ';
            row = -1;

            var match = Regex.Match(coordinates, "^([A-Z]{1})([1-9]{1}[0-9]?)$");
            if (match.Success && match.Groups.Count == 3)
            {
                column = match.Groups[1].Value[0];
                row = int.Parse(match.Groups[2].Value);
            }

            return match.Success;
        }

        public void UpdateStatusMessage(string message)
        {
            StatusMessage = message;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusMessage)));
        }
    }
}
