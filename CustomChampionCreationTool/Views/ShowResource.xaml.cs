using CustomChampionCreationTool.Objects;
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
    /// Interaction logic for ShowResource.xaml
    /// </summary>
    public partial class ShowResource : Window
    {
        Resource resource = null;
        public ShowResource()
        {
            InitializeComponent();

            GoodExit.Content = "OK";
            BadExit.Visibility = Visibility.Hidden;

            MaxValue.IsReadOnly = true;
            MinValue.IsReadOnly = true;
            Name.IsReadOnly = true;
            StartMaxed.IsEnabled = false;
        }

        private void GoodExit_Click(object sender, RoutedEventArgs e)
        {
            if (EditMode.IsChecked == false)
            {
                Close();
            }
            else
            {
                try
                {
                    resource.Name = Name.Text;
                    resource.MaxValue = int.Parse(MaxValue.Text);
                    resource.MinValue = int.Parse(MinValue.Text);
                    resource.MaxedAtStart = (bool)StartMaxed.IsChecked;

                    Repo.UpdateResource(resource);

                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong - " + ex.Message, "Error", MessageBoxButton.OK);
                }
            }
        }
        public void Initialize(Resource _resource)
        {
            resource = _resource;

            MaxValue.Text = resource.MaxValue.ToString();
            MinValue.Text = resource.MinValue.ToString();
            Name.Text = resource.Name;
            StartMaxed.IsChecked = resource.MaxedAtStart;
        }

        private void BadExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close without saveing?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void EditMode_Checked(object sender, RoutedEventArgs e)
        {
            Check();
        }

        private void Check()
        {
            if ((bool)EditMode.IsChecked)
            {
                GoodExit.Content = "Save";
                BadExit.Visibility = Visibility.Visible;

                MaxValue.IsReadOnly = false;
                MinValue.IsReadOnly = false;
                Name.IsReadOnly = false;
                StartMaxed.IsEnabled = true;
            }
            else
            {
                GoodExit.Content = "OK";
                BadExit.Visibility = Visibility.Hidden;

                MaxValue.IsReadOnly = true;
                MinValue.IsReadOnly = true;
                Name.IsReadOnly = true;
                StartMaxed.IsEnabled = false;
            }
        }

        private void EditMode_Unchecked(object sender, RoutedEventArgs e)
        {
            Check();
        }
    }
}
