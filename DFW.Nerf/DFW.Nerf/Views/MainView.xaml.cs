using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DFW.Nerf.ViewModels;
using DFW.Nerf.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;

namespace DFW.Nerf.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : ContentPage
	{
        private MainViewModel _viewModel;

		public MainView ()
		{
            BindingContext = _viewModel = new MainViewModel(new APIService());
			InitializeComponent ();
            //StartTimer();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            RefreshScreen();
        }



        public void RefreshScreen()
        {
            _viewModel.RefreshCommand.Execute(null);
        }

        private void StartTimer()
        {
            Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            RefreshScreen();
        }

    }
}