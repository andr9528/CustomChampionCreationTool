using CCCTLibrary;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace CCCTMobile
{
	public partial class App : Application
	{
        public NavigationPage NavigationPage { get; private set; }
		public App ()
		{
			InitializeComponent();

            MenuPage menu = new MenuPage();
            NavigationPage = new NavigationPage(new HomePage());
            RootPage root = new RootPage
            {
                Master = menu,
                Detail = NavigationPage
            };
            MainPage = root;
		}

		protected override async void OnStart ()
		{
            List<FacadeService.ReturnMessage> result = await RepoMobile.Initialize();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
