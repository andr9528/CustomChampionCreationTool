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
    /// Interaction logic for ShowAbility.xaml
    /// </summary>
    public partial class ShowAbility : Window
    {
        Ability ability = null;

        public ShowAbility()
        {
            InitializeComponent();

            RepoPC.UpdateAvailableResources();
            ResourceType.ItemsSource = RepoPC.ResourceNamesList;

            GoodExit.Content = "OK";
            BadExit.Visibility = Visibility.Hidden;
            TerribleExit.Visibility = Visibility.Hidden;

            HaveActive.IsChecked = true;
            HaveEmpoweredOrAlternative.IsChecked = true;
            HavePassive.IsChecked = true;
        }        
        #region Click Handlers
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
                    ability.Name = AbilityName.Text;
                    ability.ResourceUse = RepoPC.ResourceList[ResourceType.SelectedIndex];
                    ability.HaveActive = (bool)HaveActive.IsChecked;
                    ability.IsToogleAble = (bool)IsToogleAble.IsChecked;
                    ability.HaveEmpoweredOrAlternative = (bool)HaveEmpoweredOrAlternative.IsChecked;
                    ability.HavePassive = (bool)HavePassive.IsChecked;
                    ability.DescriptionAct = DescriptionAct.Text;
                    ability.DamageAct = DamageAct.Text;
                    ability.CooldownAct = CooldownAct.Text;
                    ability.RangeAct = RangeAct.Text;
                    ability.ResourceCostAct = ResourceCostAct.Text;
                    ability.DescriptionEmpAlt = DescriptionEmpAlt.Text;
                    ability.DamageEmpAlt = DamageEmpAlt.Text;
                    ability.CooldownEmpAlt = CooldownEmpAlt.Text;
                    ability.RangeEmpAlt = RangeEmpAlt.Text;
                    ability.ResourceCostEmpAlt = ResourceCostEmpAlt.Text;
                    ability.DescriptionPas = DescriptionPas.Text;
                    ability.RangePas = RangePas.Text;
                    ability.DamagePas = DamagePas.Text;
                    ability.CooldownPas = CooldownPas.Text;

                    RepoPC.UpdateAbility(ability);

                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong - " + ex.Message, "Error", MessageBoxButton.OK);
                }
            }
        }
        private void BadExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close without saveing?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
        private void TerribleExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this ability?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                RepoPC.DeleteAbility(ability);
                Close();
            }
        }
        #endregion

        #region General Methods
        private void Check()
        {
            if ((bool)EditMode.IsChecked)
            {
                GoodExit.Content = "Save";
                BadExit.Visibility = Visibility.Visible;
                TerribleExit.Visibility = Visibility.Visible;

                AbilityName.IsReadOnly = false;
                ResourceType.IsReadOnly = false;

                HaveActive.IsEnabled = true;
                HaveEmpoweredOrAlternative.IsEnabled = true;
                HavePassive.IsEnabled = true;
                IsToogleAble.IsEnabled = true;

                if (HaveActive.IsChecked == true)
                {
                    HaveActive.IsChecked = false;
                    HaveActive.IsChecked = true;
                }
                if (HaveEmpoweredOrAlternative.IsChecked == true)
                {
                    HaveEmpoweredOrAlternative.IsChecked = false;
                    HaveEmpoweredOrAlternative.IsChecked = true;
                }
                if (HavePassive.IsChecked == true)
                {
                    HavePassive.IsChecked = false;
                    HavePassive.IsChecked = true;
                }
            }
            else
            {
                GoodExit.Content = "OK";
                BadExit.Visibility = Visibility.Hidden;
                TerribleExit.Visibility = Visibility.Hidden;

                AbilityName.IsReadOnly = true;
                ResourceType.IsReadOnly = true;
                HaveActive.IsEnabled = false;
                HaveEmpoweredOrAlternative.IsEnabled = false;
                HavePassive.IsEnabled = false;

                CooldownPas.IsReadOnly = true;
                DescriptionPas.IsReadOnly = true;
                DamagePas.IsReadOnly = true;
                RangePas.IsReadOnly = true;
                CooldownEmpAlt.IsReadOnly = true;
                DescriptionEmpAlt.IsReadOnly = true;
                ResourceCostEmpAlt.IsReadOnly = true;
                DamageEmpAlt.IsReadOnly = true;
                RangeEmpAlt.IsReadOnly = true;
                CooldownAct.IsReadOnly = true;
                DescriptionAct.IsReadOnly = true;
                ResourceCostAct.IsReadOnly = true;
                DamageAct.IsReadOnly = true;
                RangeAct.IsReadOnly = true;
                IsToogleAble.IsEnabled = false;
            }
        }
        internal void Initialize(Ability _ability)
        {
            ability = _ability;

            AbilitySlot.Text = ability.Slot.ToString();
            AbilityName.Text = ability.Name;
            ResourceType.SelectedIndex = RepoPC.ResourceNamesList.IndexOf(ability.ResourceUse.Name);
            HaveActive.IsChecked = ability.HaveActive;
            HaveEmpoweredOrAlternative.IsChecked = ability.HaveEmpoweredOrAlternative;
            HavePassive.IsChecked = ability.HavePassive;

            CooldownPas.Text = ability.CooldownPas;
            DescriptionPas.Text = ability.DescriptionPas;
            DamagePas.Text = ability.DamagePas;
            RangePas.Text = ability.RangePas;
            CooldownEmpAlt.Text = ability.CooldownEmpAlt;
            DescriptionEmpAlt.Text = ability.DescriptionEmpAlt;
            ResourceCostEmpAlt.Text = ability.ResourceCostEmpAlt;
            DamageEmpAlt.Text = ability.DamageEmpAlt;
            RangeEmpAlt.Text = ability.RangeEmpAlt;
            CooldownAct.Text = ability.CooldownAct;
            DescriptionAct.Text = ability.DescriptionAct;
            ResourceCostAct.Text = ability.ResourceCostAct;
            DamageAct.Text = ability.DamageAct;
            RangeAct.Text = ability.RangeAct;
            IsToogleAble.IsChecked = ability.IsToogleAble;

            Check();
        }
        private void CheckBoxHandler(object sender)
        {
            CheckBox box = (CheckBox)sender;

            switch (box.Name)
            {
                case "HaveActive":
                    switch (box.IsChecked)
                    {
                        case true:
                            CooldownAct.IsReadOnly = false;
                            DescriptionAct.IsReadOnly = false;
                            ResourceCostAct.IsReadOnly = false;
                            DamageAct.IsReadOnly = false;
                            RangeAct.IsReadOnly = false;
                            IsToogleAble.IsEnabled = true;
                            break;
                        case false:
                            CooldownAct.IsReadOnly = true;
                            DescriptionAct.IsReadOnly = true;
                            ResourceCostAct.IsReadOnly = true;
                            DamageAct.IsReadOnly = true;
                            RangeAct.IsReadOnly = true;
                            IsToogleAble.IsEnabled = false;
                            break;
                        default:
                            break;
                    }
                    break;
                case "HaveEmpoweredOrAlternative":
                    switch (box.IsChecked)
                    {
                        case true:
                            CooldownEmpAlt.IsReadOnly = false;
                            DescriptionEmpAlt.IsReadOnly = false;
                            ResourceCostEmpAlt.IsReadOnly = false;
                            DamageEmpAlt.IsReadOnly = false;
                            RangeEmpAlt.IsReadOnly = false;
                            break;
                        case false:
                            CooldownEmpAlt.IsReadOnly = true;
                            DescriptionEmpAlt.IsReadOnly = true;
                            ResourceCostEmpAlt.IsReadOnly = true;
                            DamageEmpAlt.IsReadOnly = true;
                            RangeEmpAlt.IsReadOnly = true;
                            break;
                        default:
                            break;
                    }
                    break;
                case "HavePassive":
                    switch (box.IsChecked)
                    {
                        case true:
                            CooldownPas.IsReadOnly = false;
                            DescriptionPas.IsReadOnly = false;
                            DamagePas.IsReadOnly = false;
                            RangePas.IsReadOnly = false;
                            break;
                        case false:
                            CooldownPas.IsReadOnly = true;
                            DescriptionPas.IsReadOnly = true;
                            DamagePas.IsReadOnly = true;
                            RangePas.IsReadOnly = true;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Checkbox Handlers
        private void HaveActive_Checked(object sender, RoutedEventArgs e)
        {
            CheckBoxHandler(sender);
        }
        private void HaveActive_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBoxHandler(sender);
        }
        private void HaveEmpoweredOrAlternative_Checked(object sender, RoutedEventArgs e)
        {
            CheckBoxHandler(sender);
        }
        private void HaveEmpoweredOrAlternative_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBoxHandler(sender);
        }
        private void HavePassive_Checked(object sender, RoutedEventArgs e)
        {
            CheckBoxHandler(sender);
        }
        private void HavePassive_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBoxHandler(sender);
        }
        private void EditMode_Checked(object sender, RoutedEventArgs e)
        {
            Check();
        }
        private void EditMode_Unchecked(object sender, RoutedEventArgs e)
        {
            Check();
        }
        #endregion

        
    }
}
