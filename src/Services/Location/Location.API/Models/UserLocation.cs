using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Location.API.Models
{
    public class UserLocation
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public int LocationId { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
