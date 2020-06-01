using System;
using System.Collections.Generic;
using System.Text;

namespace Together.BuildingBlocks.Infrastructure.Identity
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public int Gender { get; set; }
    }
}
