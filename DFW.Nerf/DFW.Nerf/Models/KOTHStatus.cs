using System;
using System.Collections.Generic;
using System.Text;

namespace DFW.Nerf.Models
{
    public class Team
    {
        public string teamName { get; set; }
        public bool isActive { get; set; }
        public object timerStartedAt { get; set; }
        public int elapsedTimeInSeconds { get; set; }

        public string IsActive
        {
            get
            {
                return isActive ? "ACTIVE" :"";
            }
        }

        public string GetActiveColor
        {
            get
            {
                return isActive ? "Red" : "Color.Default";
            }
        }

        public string getUrlForImage
        {
            get
            {
                return getImageUrlForTeam(teamName);
            }
        }

        private string getImageUrlForTeam(string _teamName)
        {
            switch (_teamName)
            {
                case "Red":
                    return "https://ae01.alicdn.com/kf/HTB1Pj3HIFXXXXb6XVXXq6xXFXXXK/Licensed-Nerf-N-Strike-ELITE-Stryfe-Motorized-Blaster-Toy-Gun-Blasters-Refill-Clip-Darts-nerf-bullets.jpg";
                case "Blue":
                    return "https://images-na.ssl-images-amazon.com/images/I/71D%2Brf9kXuL._SX425_.jpg";
                default:
                    return "https://i.pinimg.com/originals/e4/c7/e4/e4c7e4c6625f7aeff3865d04f7881740.png";
            }

        }

        private string serverUrl;
        public string GetServerUrl
        {
            get
            {
                return serverUrl ?? "";
            }
            set
            {
                if (serverUrl != value)
                {
                    serverUrl = value;
                }
            }
        }
        
    }

    public class RootObject
    {
        public List<Team> Teams { get; set; }
    }

}
