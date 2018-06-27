
using CCCTLibrary;
using MoreLinq;
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
    /// Interaction logic for NewResource.xaml
    /// </summary>
    public partial class NewResource : Window
    {
        List<Resource> resourceList;
        public NewResource()
        {
            InitializeComponent();
            resourceList = Repo.GetResources().Item1;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id;
                if (resourceList.Count == 0)
                {
                    id = 0;
                }
                else
                {
                    id = resourceList.MaxBy(x => x.ID).ID + 1;
                }

                Resource dummy = new Resource()
                {
                    Name = Name.Text,
                    MaxValue = int.Parse(MaxValue.Text),
                    MinValue = int.Parse(MinValue.Text),
                    MaxedAtStart = (bool)StartMaxed.IsChecked,
                    ID = id
                };
                Repo.NewResource(dummy);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong - " + ex.Message, "Error", MessageBoxButton.OK);
            }
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close without saving?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
