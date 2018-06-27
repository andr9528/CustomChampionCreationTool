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
using CCCTLibrary;


namespace CustomChampionCreationTool.Views
{
    /// <summary>
    /// Interaction logic for NewChamp.xaml
    /// </summary>
    public partial class NewChamp : Window
    {
        List<string> resourceNamesList = new List<string>();
        List<Resource> resourceList;
        List<Ability> abilitiesList;
        
        Champion champ = new Champion();

        public NewChamp()
        {
            InitializeComponent();
            Title = "New Champion";

            UpdateAvailableResources();

            PassiveAbilityButton.Content = "New Ability";
            QAbilityButton.Content = "New Ability";
            WAbilityButton.Content = "New Ability";
            EAbilityButton.Content = "New Ability";
            RAbilityButton.Content = "New Ability";

            ResourceType.SelectedIndex = 0;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
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

        private void ResourceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NewResource_Click(object sender, RoutedEventArgs e)
        {
            UpdateAvailableResources();

            int before = resourceList.Count;

            NewResource newResource = new NewResource();
            newResource.ShowDialog();

            UpdateAvailableResources();

            int after = resourceList.Count;

            if (after == before + 1)
            {
                UpdateAvailableResources();
                ResourceType.ItemsSource = new string[] { "You Can't See Me" };
                ResourceType.ItemsSource = resourceNamesList;
                ResourceType.SelectedIndex = resourceList.Count - 1; 
            }
            else
            {
                MessageBox.Show("Resource Creation cancelled by User", "Message", MessageBoxButton.OK);
            }
        }
        private void UpdateAvailableResources()
        {
            int indexBefore = ResourceType.SelectedIndex;
            resourceList = Repo.GetResources().Item1;
            resourceNamesList.Clear();
            ResourceType.ItemsSource = new string[] { "You Can't See Me" };

            foreach (Resource item in resourceList)
            {
                resourceNamesList.Add(item.ToStringR());
            }

            ResourceType.ItemsSource = resourceNamesList;
            ResourceType.SelectedIndex = indexBefore;
        }
        private void UpdateAvailableAbilities()
        {
            abilitiesList = Repo.GetAbilities().Item1;
        }

        private void ShowResource_Click(object sender, RoutedEventArgs e)
        {
            ShowResource show = new ShowResource();
            show.Initialize(resourceList[ResourceType.SelectedIndex]);

            show.ShowDialog();
            UpdateAvailableResources();
        }

        private void DeleteResource_Click(object sender, RoutedEventArgs e)
        {
            int indexBefore = ResourceType.SelectedIndex;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the selected Resource?", "Warning", MessageBoxButton.YesNo);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    Repo.DeleteResource(resourceList[ResourceType.SelectedIndex]);

                    UpdateAvailableResources();
                    ResourceType.SelectedIndex = indexBefore - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong - " + ex.Message, "Error" , MessageBoxButton.OK);
            }
            
        }

        private void PassiveAbilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (champ.PassiveAbility == null)
            {
                Ability ability = NewAbility(LibRepo.AbilitySlot.Passive, ResourceType.SelectedIndex);

                if (ability == null)
                {
                    MessageBox.Show("Ability Creation cancelled by User", "Message", MessageBoxButton.OK);
                }
                else
                {
                    champ.PassiveAbility = ability;
                    PassiveAbilityText.Text = champ.PassiveAbility.Name;
                    PassiveAbilityButton.Content = "Show Ability";
                }

                
            }
            else
            {
                Ability showAbility = ShowAbility(champ.PassiveAbility);

                champ.PassiveAbility = showAbility;

                if (champ.PassiveAbility == null)
                {
                    PassiveAbilityButton.Content = "New Ability";
                    PassiveAbilityText.Text = "";
                }
            }
        }

        private void QAbilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (champ.QAbility == null)
            {
                Ability ability = NewAbility(LibRepo.AbilitySlot.Q, ResourceType.SelectedIndex);

                if (ability == null)
                {
                    MessageBox.Show("Ability Creation cancelled by User", "Message", MessageBoxButton.OK);
                }
                else
                {
                    champ.QAbility = ability;
                    QAbilityText.Text = champ.QAbility.Name;
                    QAbilityButton.Content = "Show Ability";
                } 
            }
            else
            {
                Ability showAbility = ShowAbility(champ.QAbility);

                champ.QAbility = showAbility;

                if (champ.QAbility == null)
                {
                    QAbilityButton.Content = "New Ability";
                    QAbilityText.Text = "";
                }
            }

            
        }

        private void WAbilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (champ.WAbility == null)
            {
                Ability ability = NewAbility(LibRepo.AbilitySlot.W, ResourceType.SelectedIndex);

                if (ability == null)
                {
                    MessageBox.Show("Ability Creation cancelled by User", "Message", MessageBoxButton.OK);
                }
                else
                {
                    champ.WAbility = ability;
                    WAbilityText.Text = champ.WAbility.Name;
                    WAbilityButton.Content = "Show Ability";
                } 
            }
            else
            {
                Ability showAbility = ShowAbility(champ.WAbility);

                champ.WAbility = showAbility;

                if (champ.WAbility == null)
                {
                    WAbilityButton.Content = "New Ability";
                    WAbilityText.Text = "";
                }
            }
        }

        private void EAbilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (champ.EAbility == null)
            {
                Ability ability = NewAbility(LibRepo.AbilitySlot.E, ResourceType.SelectedIndex);

                if (ability == null)
                {
                    MessageBox.Show("Ability Creation cancelled by User", "Message", MessageBoxButton.OK);
                }
                else
                {
                    champ.EAbility = ability;
                    EAbilityText.Text = champ.EAbility.Name;
                    EAbilityButton.Content = "Show Ability";
                } 
            }
            else
            {
                Ability showAbility = ShowAbility(champ.EAbility);

                champ.EAbility = showAbility;

                if (champ.EAbility == null)
                {
                    EAbilityButton.Content = "New Ability";
                    EAbilityText.Text = "";
                }
            }

            
        }

        private void RAbilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (champ.RAbility == null)
            {
                Ability newAbility = NewAbility(LibRepo.AbilitySlot.R, ResourceType.SelectedIndex);

                if (newAbility == null)
                {
                    MessageBox.Show("Ability Creation cancelled by User", "Message", MessageBoxButton.OK);
                }
                else
                {
                    champ.RAbility = newAbility;
                    RAbilityText.Text = champ.RAbility.Name;
                    RAbilityButton.Content = "Show Ability";
                }
            }
            else
            {
                Ability showAbility = ShowAbility(champ.RAbility);

                champ.RAbility = showAbility;

                if (champ.RAbility == null)
                {
                    RAbilityButton.Content = "New Ability";
                    RAbilityText.Text = "";
                }
            }  
        }

        private Ability NewAbility(LibRepo.AbilitySlot slot, int typeIndex)
        {
            Ability output = null;
            UpdateAvailableAbilities();
            int before = abilitiesList.Count;

            NewAbility newAbility = new NewAbility();
            newAbility.Initialize(slot, typeIndex);
            newAbility.ShowDialog();

            UpdateAvailableAbilities();
            int after = abilitiesList.Count;

            if (after == before + 1)
            {
                output = abilitiesList.Last();
            }
            return output;
        }

        private Ability ShowAbility(Ability input)
        {
            Ability output = null;
            UpdateAvailableAbilities();
            int before = abilitiesList.Count;

            ShowAbility showAbility = new ShowAbility();
            showAbility.Initialize(input);
            showAbility.ShowDialog();

            UpdateAvailableAbilities();
            int after = abilitiesList.Count;

            try
            {
                if (after != before - 1)
                {
                    output = abilitiesList.Find(x => x.ID == input.ID);
                }
            }
            catch (ArgumentNullException)
            {
            }

            return output;
        }
    }
}
