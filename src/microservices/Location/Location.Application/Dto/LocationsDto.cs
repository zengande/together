using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Location.Application.Dto
{
    public class LocationsDto
    {
        public int Code { get; set; }
        public int? ParentCode { get; set; }
        public string Name { get; set; }
        public string MergeName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Level { get; set; }
    }
}
