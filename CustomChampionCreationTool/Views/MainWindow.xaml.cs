using CCCTLibrary;
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

            List<ReturnMessage> messages = RepoPC.Initialize();

            ResourceSelction.ItemsSource = RepoPC.ResourceNamesList;
            AbilitySelction.ItemsSource = RepoPC.AbilityNamesList;
            ChampionSelction.ItemsSource = RepoPC.ChampionNamesList;

            ResourceSelction.SelectedIndex = 0;
            AbilitySelction.SelectedIndex = 0;
            ChampionSelction.SelectedIndex = 0;

            UpdateView();

            try
            {
                foreach (ReturnMessage mes in messages)
                {
                    if (mes.WasSuccesful == false)
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception)
            {
                string warning = "Initialization of Database Handler failed \n" +
                                        "Restart program to try again \n" +
                                        "If this message have been shown multiple times, then contact program creator";

                MessageBox.Show(warning, "Warning", MessageBoxButton.OK);

                NewChampion.IsEnabled = false;
                NewResource.IsEnabled = false;
                ChampionSelction.IsEnabled = false;
                AbilitySelction.IsEnabled = false;
                ResourceSelction.IsEnabled = false;
                ShowChampion.IsEnabled = false;
                ShowAbility.IsEnabled = false;
                ShowResource.IsEnabled = false;
                DeleteChampion.IsEnabled = false;
                DeleteResource.IsEnabled = false;
            }
        }

        #region General Methods
        private void UpdateView()
        {
            if (RepoPC.ChampionList.Count == 0)
            {
                ShowChampion.IsEnabled = false;
                DeleteChampion.IsEnabled = false;
            }
            else
            {
                ShowChampion.IsEnabled = true;
                DeleteChampion.IsEnabled = true;
            }
            if (RepoPC.AbilitiesList.Count == 0)
            {
                ShowAbility.IsEnabled = false;
            }
            else
            {
                ShowAbility.IsEnabled = true;
            }
            if (RepoPC.ResourceList.Count == 0)
            {
                ShowResource.IsEnabled = false;
                DeleteResource.IsEnabled = false;
            }
            else
            {
                ShowResource.IsEnabled = true;
                DeleteResource.IsEnabled = true;
            }
        }
        #endregion

        #region Click Handlers
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowCreators_Click(object sender, RoutedEventArgs e)
        {
            string message = "Created by André Steenhoff Madsen, aka WolfDK \n" +
                            "Email: andre@steenhoff.dk or mcwolfdk@gmail.com";
                                
            MessageBox.Show(message, "Creators", MessageBoxButton.OK);
        }
        #region Champion
        private void ShowChampion_Click(object sender, RoutedEventArgs e)
        {
            ShowChampion show = new ShowChampion();

            show.ShowDialog();
        }

        private void DeleteChampion_Click(object sender, RoutedEventArgs e)
        {
            int indexBefore = ChampionSelction.SelectedIndex;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the selected Champion?", "Warning", MessageBoxButton.YesNo);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    RepoPC.DeleteChampion(RepoPC.ChampionList[indexBefore]);
                    ChampionSelction.ItemsSource = new string[] { "You Can't See Me" };

                    RepoPC.UpdateAvailableChampions();
                    ChampionSelction.ItemsSource = RepoPC.ChampionNamesList;
                    ChampionSelction.SelectedIndex = indexBefore - 1;

                    UpdateView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong - " + ex.Message, "Error", MessageBoxButton.OK);
            }
        }
        
        private void NewChampion_Click(object sender, RoutedEventArgs e)
        {
            RepoPC.UpdateAvailableChampions();

            int before = RepoPC.ChampionList.Count;

            NewChampion newchamp = new NewChampion();
            newchamp.ShowDialog();

            RepoPC.UpdateAvailableChampions();

            int after = RepoPC.ChampionList.Count;

            if (after == before + 1)
            {
                ChampionSelction.ItemsSource = new string[] { "You Can't See Me" };
                ChampionSelction.ItemsSource = RepoPC.ResourceNamesList;
                ChampionSelction.SelectedIndex = RepoPC.ResourceList.Count - 1;
            }
            else
            {
                MessageBox.Show("Champion Creation cancelled by User", "Message", MessageBoxButton.OK);
            }
            UpdateView();
        }
        #endregion

        #region Ability
        private void ShowAbility_Click(object sender, RoutedEventArgs e)
        {
            int before = RepoPC.AbilitiesList.Count;
            int indexBefore = AbilitySelction.SelectedIndex;

            ShowAbility showAbility = new ShowAbility();
            showAbility.Initialize(RepoPC.AbilitiesList[indexBefore]);
            showAbility.ShowDialog();

            RepoPC.UpdateAvailableAbilities();
            int after = RepoPC.AbilitiesList.Count;

            if (after == before - 1)
            {
                AbilitySelction.ItemsSource = new string[] { "You Can't See Me" };
                AbilitySelction.ItemsSource = RepoPC.AbilityNamesList;
                AbilitySelction.SelectedIndex = indexBefore - 1;
            }
            UpdateView();
        }
        #endregion

        #region Resource
        private void ShowResource_Click(object sender, RoutedEventArgs e)
        {
            ShowResource show = new ShowResource();
            show.Initialize(RepoPC.ResourceList[ResourceSelction.SelectedIndex]);

            show.ShowDialog();
            RepoPC.UpdateAvailableResources();
        }

        private void DeleteResource_Click(object sender, RoutedEventArgs e)
        {
            int indexBefore = ResourceSelction.SelectedIndex;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the selected Resource?", "Warning", MessageBoxButton.YesNo);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    RepoPC.DeleteResource(RepoPC.ResourceList[indexBefore]);
                    ResourceSelction.ItemsSource = new string[] { "You Can't See Me" };

                    RepoPC.UpdateAvailableResources();
                    ResourceSelction.ItemsSource = RepoPC.ResourceNamesList;
                    ResourceSelction.SelectedIndex = indexBefore - 1;

                    UpdateView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong - " + ex.Message, "Error", MessageBoxButton.OK);
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
                ResourceSelction.ItemsSource = new string[] { "You Can't See Me" };
                ResourceSelction.ItemsSource = RepoPC.ResourceNamesList;
                ResourceSelction.SelectedIndex = RepoPC.ResourceList.Count - 1;

                UpdateView();
            }
            else
            {
                MessageBox.Show("Resource Creation cancelled by User", "Message", MessageBoxButton.OK);
            }
        }
        #endregion

        #endregion
    }
}
