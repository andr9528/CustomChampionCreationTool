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
    public partial class NewChamp : Window
    {
        List<string> resourceNamesList = new List<string>();
        List<Resource> resourceList;

        public NewChamp()
        {
            InitializeComponent();
            Title = "New Champion";

            UpdateAvailableResources();

            ResourceType.ItemsSource = resourceNamesList;
            ResourceType.SelectedIndex = 0;

            //Test.ItemsSource = Repo.Test2();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close without saveing?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void ResourceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ResourceType.SelectedIndex)
            {
                default:
                    break;
            }

        }

        private void NewResource_Click(object sender, RoutedEventArgs e)
        {


            UpdateAvailableResources();
        }
        private void UpdateAvailableResources()
        {
            resourceList = Repo.GetResources();
            resourceNamesList.Clear();

            foreach (Resource item in resourceList)
            {
                resourceNamesList.Add(item.ToStringR());
            }
        }

    }
}
