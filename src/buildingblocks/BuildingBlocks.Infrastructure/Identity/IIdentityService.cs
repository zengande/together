using System;
using System.Collections.Generic;
using System.Text;

namespace Together.BuildingBlocks.Infrastructure.Identity
{
    public interface IIdentityService
    {
        string GetUserIdentity();
        string GetUserName();
    }
}
