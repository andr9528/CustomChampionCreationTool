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

        public NewAbility()
        {
            InitializeComponent();
        }

        internal void Initialize(Repo.AbilitySlot slot)
        {
            Slot = slot;
        }
    }
}
