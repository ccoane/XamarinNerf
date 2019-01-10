using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DFW.Nerf.Views;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace DFW.Nerf
{
	public partial class App : Application
	{
		public App ()
		{
            // Initialize Live Reload.
            #if DEBUG
            LiveReload.Init();
            #endif

            InitializeComponent();
            MainPage = new MainView();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
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
