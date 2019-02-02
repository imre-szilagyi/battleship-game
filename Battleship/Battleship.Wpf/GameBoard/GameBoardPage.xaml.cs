using System.Windows.Controls;

namespace Battleship.Wpf.GameBoard
{
    /// <summary>
    /// Interaction logic for GameBoardPage.xaml
    /// </summary>
    public partial class GameBoardPage : Page
    {
        public GameBoardPage()
        {
            DataContext = new GameBoardViewModel();
            InitializeComponent();
        }
    }
}
