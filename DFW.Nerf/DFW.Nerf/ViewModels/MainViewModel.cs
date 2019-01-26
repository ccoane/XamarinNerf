using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;

using DFW.Nerf.Services;
using DFW.Nerf.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using System.Timers;
using System.Collections.ObjectModel;
using System.Threading;

namespace DFW.Nerf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public class KOTHobj
        {
            private ObservableRangeCollection<Team> teams;

            public ObservableRangeCollection<Team> Teams
            {
                get { return teams ?? (teams = new ObservableRangeCollection<Team>()); }
                set { teams = value; }
            }

            private string textForm = null; 
            public string TextForm
            {
                get { return textForm ?? "DEFAULT"; }
                set { textForm = value; }
            }
        }
        
        private APIService _apiService;
        public MainViewModel(APIService apiService)
        {
            _apiService = apiService;
        }

        private KOTHobj kothobj;
        public KOTHobj Kothobj
        {
            get { return kothobj ?? (kothobj = new KOTHobj()); }
            set { kothobj = value; }
        }

        private System.Timers.Timer aTimer { get; set; }

        private ICommand _refreshCommand;
        public ICommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new Command(async () => await ExecuteRefreshCommandAsync()));

        private async Task ExecuteRefreshCommandAsync()
        {
            try
            {
                IsBusy = true;
                Kothobj.Teams.Clear();
                Kothobj.Teams.AddRange(await _apiService.GetTeamsStatusAsync());
                StartTimer();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void StartTimer()
        {
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(GetStatus);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
        }

        private async void GetStatus(object source, ElapsedEventArgs e)
        {
            try
            {
                Kothobj.Teams.ReplaceRange(await _apiService.GetTeamsStatusAsync());
                CheckForWinnerWinnerChickenDinner(Kothobj);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetTeamsStatusAsync: {ex}");
            }
        }

        private void CheckForWinnerWinnerChickenDinner(KOTHobj arg)
        {
            foreach (var team in arg.Teams)
            {
                if (team.elapsedTimeInSeconds > 10)
                {
                    aTimer.Stop();
                    
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("Winner Winner Chicken Dinner", $"{team.teamName} Team Wins!", "OK");
                    });

                }
            }
        }
    }
}
