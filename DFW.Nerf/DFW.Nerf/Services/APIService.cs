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
        public async Task<List<Team>> GetTeamsStatusAsync(string URL = "")
        {
            var httpClient = new HttpClient();
            string url = (String.IsNullOrWhiteSpace(URL)) ? $"https://nerf-data-api-dfw.herokuapp.com/koth/status" : URL;
            //var response = await httpClient.GetAsync($"https://randomuser.me/api/?results={count}&seed=northdallas");
            var response = await httpClient.GetAsync(url);

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
