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
    /// Interaction logic for NewAbility.xaml
    /// </summary>
    public partial class NewAbility : Window
    {
        LibRepo.AbilitySlot Slot;

        public NewAbility()
        {
            InitializeComponent();

            Repo.UpdateAvailableResources();
            ResourceType.ItemsSource = Repo.ResourceNamesList;

            HaveActive.IsChecked = true;
            HaveEmpoweredOrAlternative.IsChecked = true;
            HavePassive.IsChecked = true;
        }        
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
        #endregion

        #region Click Handlers
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
            try
            {
                int id;
                if (Repo.AbilitiesList.Count == 0)
                {
                    id = 0;
                }
                else
                {
                    id = Repo.AbilitiesList.MaxBy(x => x.ID).ID + 1;
                }

                Ability dummy = new Ability()
                {
                    Name = AbilityName.Text,
                    ID = id,
                    Slot = Slot,
                    ResourceUse = Repo.ResourceList[ResourceType.SelectedIndex],
                    HaveActive = (bool)HaveActive.IsChecked,
                    IsToogleAble = (bool)IsToogleAble.IsChecked,
                    HaveEmpoweredOrAlternative = (bool)HaveEmpoweredOrAlternative.IsChecked,
                    HavePassive = (bool)HavePassive.IsChecked,
                    DescriptionAct = DescriptionAct.Text,
                    DamageAct = DamageAct.Text,
                    CooldownAct = CooldownAct.Text,
                    RangeAct = RangeAct.Text,
                    ResourceCostAct = ResourceCostAct.Text,
                    DescriptionEmpAlt = DescriptionEmpAlt.Text,
                    DamageEmpAlt = DamageEmpAlt.Text,
                    CooldownEmpAlt = CooldownEmpAlt.Text,
                    RangeEmpAlt = RangeEmpAlt.Text,
                    ResourceCostEmpAlt = ResourceCostEmpAlt.Text,
                    DescriptionPas = DescriptionPas.Text,
                    RangePas = RangePas.Text,
                    DamagePas = DamagePas.Text,
                    CooldownPas = CooldownPas.Text
                };
                ReturnMessage result = Repo.NewAbility(dummy);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong - " + ex.Message, "Error", MessageBoxButton.OK);
            }
        }
        #endregion

        #region General Methods
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
        internal void Initialize(LibRepo.AbilitySlot slot, int typeIndex)
        {
            Slot = slot;
            AbilitySlot.Text = Slot.ToString();
            ResourceType.SelectedIndex = typeIndex;

            switch (Slot)
            {
                case LibRepo.AbilitySlot.Passive:
                    HaveActive.IsChecked = false;
                    HaveEmpoweredOrAlternative.IsChecked = false;
                    HavePassive.IsChecked = true;
                    break;
                case LibRepo.AbilitySlot.Q:
                    HaveActive.IsChecked = true;
                    HaveEmpoweredOrAlternative.IsChecked = false;
                    HavePassive.IsChecked = false;
                    break;
                case LibRepo.AbilitySlot.W:
                    HaveActive.IsChecked = true;
                    HaveEmpoweredOrAlternative.IsChecked = false;
                    HavePassive.IsChecked = false;
                    break;
                case LibRepo.AbilitySlot.E:
                    HaveActive.IsChecked = true;
                    HaveEmpoweredOrAlternative.IsChecked = false;
                    HavePassive.IsChecked = false;
                    break;
                case LibRepo.AbilitySlot.R:
                    HaveActive.IsChecked = true;
                    HaveEmpoweredOrAlternative.IsChecked = false;
                    HavePassive.IsChecked = false;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
