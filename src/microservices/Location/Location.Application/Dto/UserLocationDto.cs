using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Location.Application.Dto
{
    public class UserLocationDto
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int CityCode { get; set; }
    }
}
