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

namespace DFW.Nerf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Object lockObj = new object();
        private APIService _apiService;
        public MainViewModel(APIService apiService)
        {
            _apiService = apiService;
        }

        //public ObservableRangeCollection<Team> Teams { get; set; } =
        //    new ObservableRangeCollection<Team>();

        private ObservableRangeCollection<Team> teams;

        public ObservableRangeCollection<Team> Teams
        {
            get { return teams ?? (teams = new ObservableRangeCollection<Team>()); }
            set { teams = value; }
        }

        private Timer aTimer { get; set; }

        private ICommand _refreshCommand;
        public ICommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new Command(async () => await ExecuteRefreshCommandAsync()));

        private async Task ExecuteRefreshCommandAsync()
        {
            try
            {
                IsBusy = true;
                Teams.Clear();
                Teams.AddRange(await _apiService.GetTeamsStatusAsync());
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
            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
        }

        private async void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                Teams.ReplaceRange(new List<Team>(await _apiService.GetTeamsStatusAsync()));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetTeamsStatusAsync: {ex}");
            }
        }
    }
}
