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
using MoreLinq;

namespace CustomChampionCreationTool.Views
{
    /// <summary>
    /// Interaction logic for NewChamp.xaml
    /// </summary>
    public partial class NewChampion : Window
    {
        Champion champ = new Champion();

        public NewChampion()
        {
            InitializeComponent();
            RepoPC.UpdateAvailableResources();

            PassiveAbilityButton.Content = "New Ability";
            QAbilityButton.Content = "New Ability";
            WAbilityButton.Content = "New Ability";
            EAbilityButton.Content = "New Ability";
            RAbilityButton.Content = "New Ability";

            ResourceType.ItemsSource = RepoPC.ResourceNamesList;
            ResourceType.SelectedIndex = 0;
        }
        #region General Methods
        private Ability NewAbility(LibRepo.AbilitySlot slot, int typeIndex)
        {
            Ability output = null;
            RepoPC.UpdateAvailableAbilities();
            int before = RepoPC.AbilitiesList.Count;

            NewAbility newAbility = new NewAbility();
            newAbility.Initialize(slot, typeIndex);
            newAbility.ShowDialog();

            RepoPC.UpdateAvailableAbilities();
            int after = RepoPC.AbilitiesList.Count;

            if (after == before + 1)
            {
                output = RepoPC.AbilitiesList.Last();
            }
            return output;
        }

        private Ability ShowAbility(Ability input)
        {
            Ability output = null;
            RepoPC.UpdateAvailableAbilities();
            int before = RepoPC.AbilitiesList.Count;

            ShowAbility showAbility = new ShowAbility();
            showAbility.Initialize(input);
            showAbility.ShowDialog();

            RepoPC.UpdateAvailableAbilities();
            int after = RepoPC.AbilitiesList.Count;

            try
            {
                if (after != before - 1)
                {
                    output = RepoPC.AbilitiesList.Find(x => x.ID == input.ID);
                }
            }
            catch (ArgumentNullException)
            {
            }

            return output;
        }
        #endregion

        #region Click Handlers
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if (RepoPC.ChampionList.Count == 0)
            {
                id = 0;
            }
            else
            {
                id = RepoPC.ChampionList.MaxBy(x => x.ID).ID + 1;
            }

            champ.ID = id;
            champ.Resource = RepoPC.ResourceList[ResourceType.SelectedIndex];
            champ.Name = Name.Text;
            champ.HealthStart = Health.Text;
            champ.HealthGrowth = HealthGrowth.Text;
            champ.HealthRegenStart = HealthRegeneration.Text;
            champ.HealthRegenGrowth = HealthRegenerationGrowth.Text;
            champ.ResourceStart = Resource.Text;
            champ.ResourceGrowth = ResourceGrowth.Text;
            champ.ResourceRegenStart = ResourceRegeneration.Text;
            champ.ResourceRegenGrowth = ResourceRegenerationGrowth.Text;
            champ.AttackDamageStart = AttackDamage.Text;
            champ.AttackDamageGrowth = AttackDamageGrowth.Text;
            champ.AbilityPowerStart = AbilityPower.Text;
            champ.AbilityPowerGrowth = AbilityPowerGrowth.Text;
            champ.AttackSpeedStart = AttackSpeed.Text;
            champ.AttackSpeedGrowth = AttackSpeedGrowth.Text;
            champ.RangeStart = Range.Text;
            champ.RangeGrowth = RangeGrowth.Text;
            champ.CriticalStrikeChanceStart = CriticalStrikeChance.Text;
            champ.CriticalStrikeChanceGrowth = CriticalStrikeChanceGrowth.Text;
            champ.ArmorStart = Armor.Text;
            champ.ArmorGrowth = ArmorGrowth.Text;
            champ.MagicResistStart = MagicResist.Text;
            champ.MagicResistGrowth = MagicResistGrowth.Text;
            champ.MoveSpeedStart = Movespeed.Text;
            champ.MoveSpeedGrowth = MovespeedGrowth.Text;

            ReturnMessage result = RepoPC.NewChampion(champ);

            if (result.WasSuccesful == true)
            {
                Close();
            }
            else
            {
                MessageBox.Show(result.Message + " - " + result.Exception, "Warning", MessageBoxButton.OK);
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
        private void NewResource_Click(object sender, RoutedEventArgs e)
        {
            RepoPC.UpdateAvailableResources();

            int before = RepoPC.ResourceList.Count;

            NewResource newResource = new NewResource();
            newResource.ShowDialog();

            RepoPC.UpdateAvailableResources();

            int after = RepoPC.ResourceList.Count;

            if (after == before + 1)
            {
                RepoPC.UpdateAvailableResources();
                ResourceType.ItemsSource = new string[] { "You Can't See Me" };
                ResourceType.ItemsSource = RepoPC.ResourceNamesList;
                ResourceType.SelectedIndex = RepoPC.ResourceList.Count - 1;
            }
            else
            {
                MessageBox.Show("Resource Creation cancelled by User", "Message", MessageBoxButton.OK);
            }
        }
        private void ShowResource_Click(object sender, RoutedEventArgs e)
        {
            ShowResource show = new ShowResource();
            show.Initialize(RepoPC.ResourceList[ResourceType.SelectedIndex]);

            show.ShowDialog();
            RepoPC.UpdateAvailableResources();
        }
        private void DeleteResource_Click(object sender, RoutedEventArgs e)
        {
            int indexBefore = ResourceType.SelectedIndex;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the selected Resource?", "Warning", MessageBoxButton.YesNo);
            ReturnMessage returnMessage = null;

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    returnMessage = RepoPC.DeleteResource(RepoPC.ResourceList[ResourceType.SelectedIndex]);
                    ResourceType.ItemsSource = new string[] { "You Can't See Me" };

                    RepoPC.UpdateAvailableResources();
                    ResourceType.ItemsSource = RepoPC.ResourceNamesList;
                    ResourceType.SelectedIndex = indexBefore - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong - " + ex.Message, "Error", MessageBoxButton.OK);
            }
        }
        #region Ability Buttons
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

        #endregion

        #endregion
    }
}
