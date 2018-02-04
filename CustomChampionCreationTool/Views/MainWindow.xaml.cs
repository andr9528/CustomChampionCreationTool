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
using System.Windows.Shapes;

namespace CustomChampionCreationTool.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Repo.Initialize();
        }

        private void NewChamp_Click(object sender, RoutedEventArgs e)
        {
            NewChamp newchamp = new NewChamp();

            newchamp.ShowDialog();
        }

        private void ShowChamps_Click(object sender, RoutedEventArgs e)
        {
            ShowChamps showchamps = new ShowChamps();

            showchamps.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowCreators_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Created by André Steenhoff Madsen, aka WolfDK", "Creators", MessageBoxButton.OK);
        }
    }
}
