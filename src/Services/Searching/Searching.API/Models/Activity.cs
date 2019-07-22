using Microsoft.CodeAnalysis;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Searching.API.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime CreateTime { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public GeoLocation LocationPoint => new GeoLocation(Latitude, Longitude);
    }
}
