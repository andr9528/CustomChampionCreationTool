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
using CustomChampionCreationTool.Objects;


namespace CustomChampionCreationTool.Views
{
    /// <summary>
    /// Interaction logic for NewChamp.xaml
    /// </summary>
    public partial class NewChamp : System.Windows.Window
    {
        public NewChamp()
        {
            InitializeComponent();

            ResourceType.ItemsSource = Repo.GetResources();

            Test.ItemsSource = Repo.Test1();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close without saveing?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void ResourceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
        }

        private void NewResource_Click(object sender, RoutedEventArgs e)
        {


            ResourceType.ItemsSource = Repo.GetResources();
        }

    }
}
