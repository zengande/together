using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Searching.API.ViewModels
{
    public class ActivitySearchViewModel
    {
        public string Keyword { get; set; }
        public string Distance { get; set; } = "3km";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 10;

        public GeoLocation Location => new GeoLocation(Latitude, Longitude);
    }
}
