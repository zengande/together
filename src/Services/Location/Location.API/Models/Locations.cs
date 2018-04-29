using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Location.API.Models
{
    public class Locations
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        
        public string CityCode { get; set; }
        public string Description { get; set; }
        public string CityName { get; set; }
        public string PointX { get; set; }
        public string PointY { get; set; }
    }
}
