using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using System;
using Android.Views;
using Android.Runtime;
using Android.Support.V7.AppCompat;
using Android.Support.V4.View;
using Android.Content;

namespace CCCTApp
{
    [Activity(Label = "CCCTApp", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainActivity);

            SetupGeneral();

            Toast.MakeText(Application, Resource.String.test, ToastLength.Long).Show();
        }


        #region General Setup
        DrawerLayout MenuView;
        NavigationView MenuList;
        IMenu NaviMenu;
        FrameLayout Detail;
        readonly int AbilityMenuItemOffset = 2000;
        readonly int ResourceMenuItemOffset = 10000;

        #region Methods
        private void SetupGeneral()
        {
            List<FacadeService.ReturnMessage> messages = Repo.Initialize();

            ImageButton burger = (ImageButton)FindViewById(Resource.Id.burger);
            burger.Click += Burger_Click;

            MenuView = (DrawerLayout)FindViewById(Resource.Id.drawer_layout);

            MenuList = (NavigationView)FindViewById(Resource.Id.nav_view);
            MenuList.NavigationItemSelected += NavigationView_NavigationItemSelected;
            OnCreateOptionsMenu(MenuList.Menu);

            Detail = (FrameLayout)FindViewById(Resource.Id.detail);
        }

        public override bool OnCreateOptionsMenu(IMenu _menu)
        {
            MenuInflater.Inflate(Resource.Menu.navmenu, _menu);

            IMenu postSetup = SetupChampionMenu(_menu);

            NaviMenu = postSetup;

            return base.OnCreateOptionsMenu(NaviMenu);
        }
        #endregion
        #region Events
        private void Burger_Click(object sender, EventArgs e)
        {
            MenuView.OpenDrawer(GravityCompat.Start);
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            e.MenuItem.SetChecked(true);
            View view = null;

            Detail.RemoveAllViewsInLayout();

            if (e.MenuItem.ItemId == NaviMenu.GetItem(0).ItemId) //Home
            {
                view = LayoutInflater.Inflate(Resource.Layout.Home, null);
                Detail.AddView(view);
            }
            else if (e.MenuItem.ItemId >= 0 && e.MenuItem.ItemId < AbilityMenuItemOffset)
            {
                view = LayoutInflater.Inflate(Resource.Layout.Champion, null);
                Detail.AddView(view);
                SetChampionValues(e.MenuItem);
            }

            MenuView.CloseDrawers();
        }


        #endregion
        #endregion
        TextView ChampName;
        Button ChammpResource;
        Button ChampPassiveAbility;
        Button ChampQAbility;
        Button ChampWAbility;
        Button ChampEAbility;
        Button ChampRAbility;
        TextView HealthStart;
        TextView HealthGrowth;
        TextView HealthRegenStart;
        TextView HealthRegenGrowth;
        TextView ResourceStart;
        TextView ResourceGrowth;
        TextView ResourceRegenStart;
        TextView ResourceRegenGrowth;
        TextView AttackDamageStart;
        TextView AttackDamageGrowth;
        TextView AbilityPowerStart;
        TextView AbilityPowerGrowth;
        TextView AttackSpeedStart;
        TextView AttackSpeedGrowth;
        TextView RangeStart;
        TextView RangeGrowth;
        TextView CriticalStrikeChanceStart;
        TextView CriticalStrikeChanceGrowth;
        TextView ArmorStart;
        TextView ArmorGrowth;
        TextView MagicResistStart;
        TextView MagicResistGrowth;
        TextView MoveSpeedStart;
        TextView MoveSpeedGrowth;

        #region Champion Setup
        private void SetupChampion()
        {
            ChampName = (TextView)FindViewById(Resource.Id.champName);
            ChammpResource = (Button)FindViewById(Resource.Id.champResourceBnt);
            ChampPassiveAbility = (Button)FindViewById(Resource.Id.champPassiveBtn);
            ChampQAbility = (Button)FindViewById(Resource.Id.champQBtn);
            ChampWAbility = (Button)FindViewById(Resource.Id.champWBtn);
            ChampEAbility = (Button)FindViewById(Resource.Id.champEBtn);
            ChampRAbility = (Button)FindViewById(Resource.Id.champRBtn);
            HealthStart = (TextView)FindViewById(Resource.Id.champHealthStart);
            HealthGrowth = (TextView)FindViewById(Resource.Id.champHealthGrowth);
            HealthRegenStart = (TextView)FindViewById(Resource.Id.champHealthRegenStart);
            HealthRegenGrowth = (TextView)FindViewById(Resource.Id.champHealthRegenGrowth);
            ResourceStart = (TextView)FindViewById(Resource.Id.champResourceStart);
            ResourceGrowth = (TextView)FindViewById(Resource.Id.champResourceGrowth);
            ResourceRegenStart = (TextView)FindViewById(Resource.Id.champResourceRegenStart);
            ResourceRegenGrowth = (TextView)FindViewById(Resource.Id.champResourceRegenGrowth);
            AttackDamageStart = (TextView)FindViewById(Resource.Id.champAttackDamageStart);
            AttackDamageGrowth = (TextView)FindViewById(Resource.Id.champAttackDamageGrowth);
            AbilityPowerStart = (TextView)FindViewById(Resource.Id.champAbilityPowerStart);
            AbilityPowerGrowth = (TextView)FindViewById(Resource.Id.champAbilityPowerGrowth);
            AttackSpeedStart = (TextView)FindViewById(Resource.Id.champAttackSpeedStart);
            AttackSpeedGrowth = (TextView)FindViewById(Resource.Id.champAttackSpeedGrowth);
            RangeStart = (TextView)FindViewById(Resource.Id.champRangeStart);
            RangeGrowth = (TextView)FindViewById(Resource.Id.champRangeGrowth);
            CriticalStrikeChanceStart = (TextView)FindViewById(Resource.Id.champCSCStart);
            CriticalStrikeChanceGrowth = (TextView)FindViewById(Resource.Id.champCSCGrowth);
            ArmorStart = (TextView)FindViewById(Resource.Id.champArmorStart);
            ArmorGrowth = (TextView)FindViewById(Resource.Id.champArmorGrowth);
            MagicResistStart = (TextView)FindViewById(Resource.Id.champMagicResistStart);
            MagicResistGrowth = (TextView)FindViewById(Resource.Id.champMagicResistGrowth);
            MoveSpeedStart = (TextView)FindViewById(Resource.Id.champMoveSpeedStart);
            MoveSpeedGrowth = (TextView)FindViewById(Resource.Id.champMoveSpeedGrowth);
        }
        private void SetChampionValues(IMenuItem menuItem)
        {
            FacadeService.Champion champion = Repo.ChampionList.Find(x => x.Name == menuItem.TitleFormatted.ToString());

            SetupChampion();

            ChampName.Text = champion.Name;
            ChammpResource.Text = champion.Resource.Name;
            ChampPassiveAbility.Text = champion.PassiveAbility.Name;
            ChampQAbility.Text = champion.QAbility.Name;
            ChampWAbility.Text = champion.WAbility.Name;
            ChampEAbility.Text = champion.EAbility.Name;
            ChampRAbility.Text = champion.RAbility.Name;
            HealthStart.Text = champion.HealthStart;
            HealthGrowth.Text = champion.HealthGrowth;
            HealthRegenStart.Text = champion.HealthRegenStart;
            HealthRegenGrowth.Text = champion.HealthRegenGrowth;
            ResourceStart.Text = champion.ResourceStart;
            ResourceGrowth.Text = champion.ResourceGrowth;
            ResourceRegenStart.Text = champion.ResourceRegenStart;
            ResourceRegenGrowth.Text = champion.ResourceRegenGrowth;
            AttackDamageStart.Text = champion.AttackDamageStart;
            AttackDamageGrowth.Text = champion.AttackDamageGrowth;
            AbilityPowerStart.Text = champion.AbilityPowerStart;
            AbilityPowerGrowth.Text = champion.AbilityPowerGrowth;
            AttackSpeedStart.Text = champion.AttackSpeedStart;
            AttackSpeedGrowth.Text = champion.AttackSpeedGrowth;
            RangeStart.Text = champion.RangeStart;
            RangeGrowth.Text = champion.RangeGrowth;
            CriticalStrikeChanceStart.Text = champion.CriticalStrikeChanceStart;
            CriticalStrikeChanceGrowth.Text = champion.CriticalStrikeChanceGrowth;
            ArmorStart.Text = champion.ArmorStart;
            ArmorGrowth.Text = champion.ArmorGrowth;
            MagicResistStart.Text = champion.MagicResistStart;
            MagicResistGrowth.Text = champion.MagicResistGrowth;
            MoveSpeedStart.Text = champion.MoveSpeedStart;
            MoveSpeedGrowth.Text = champion.MoveSpeedGrowth;
        }

        private IMenu SetupChampionMenu(IMenu _menu)
        {
            var champMenu = _menu.AddSubMenu("Champions");
            foreach (FacadeService.Champion item in Repo.ChampionList)
            {
                champMenu.Add(0, item.ID, Menu.None, item.Name);
            }

            return _menu;
        }
        #endregion
    }
}

