using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeBetoverdeDoolhof.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Rules(object sender, RoutedEventArgs e)
        {
            RulesModalWindow rulesModal = new RulesModalWindow();
            rulesModal.ShowDialog();
        }

        private void Button_Click_Treasures(object sender, RoutedEventArgs e)
        {
            TreasuresModalWindow treasuresModal = new TreasuresModalWindow();
            treasuresModal.ShowDialog();
        }
        private void Button_Click_Players(object sender, RoutedEventArgs e)
        {
            PlayersModalWindow playersModal = new PlayersModalWindow();
            playersModal.ShowDialog();
        }
    }
}
