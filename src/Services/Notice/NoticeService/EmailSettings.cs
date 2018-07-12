using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Notice
{
    public class EmailSettings
    {
        public string From { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int StmpPort { get; set; }
        public string StmpHost { get; set; }
        public string Name { get; set; }
        public string ApiKey { get; set; }
    }
}
