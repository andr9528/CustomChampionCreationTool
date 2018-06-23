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
    /// Interaction logic for NewAbility.xaml
    /// </summary>
    public partial class NewAbility : Window
    {
        Repo.AbilitySlot Slot;
        List<string> resourceNamesList = new List<string>();
        List<Resource> resourceList;

        public NewAbility()
        {
            InitializeComponent();

            UpdateAvailableResources();
            ResourceType.ItemsSource = resourceNamesList;
        }

        internal void Initialize(Repo.AbilitySlot slot, int typeIndex)
        {
            Slot = slot;
            AbilitySlot.Text = Slot.ToString();
            ResourceType.SelectedIndex = typeIndex;
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
        private void ResourceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close without saving?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HaveActive_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void HaveActive_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void HaveEmpoweredOrAlternative_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void HaveEmpoweredOrAlternative_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void HavePassive_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void HavePassive_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
