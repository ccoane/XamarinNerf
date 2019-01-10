using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DFW.Nerf.Models;

namespace DFW.Nerf.Services
{
    public class APIService
    {
        public async Task<List<Team>> GetTeamsStatusAsync()
        {
            var httpClient = new HttpClient();

            //var response = await httpClient.GetAsync($"https://randomuser.me/api/?results={count}&seed=northdallas");
            var response = await httpClient.GetAsync($"https://nerf-data-api-dfw.herokuapp.com/koth/status");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var results = JsonConvert.DeserializeObject<RootObject>(json);
                
                return results.Teams;
            }

            throw new Exception("API call failed!");
        }
    }
}
