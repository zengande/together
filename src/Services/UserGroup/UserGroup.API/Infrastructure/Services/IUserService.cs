﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Models;

namespace Together.UserGroup.API.Infrastructure.Services
{
    public interface IUserService
        : IBaseService<User>
    {
        Task<User> JoinGroup(int userId, int groupId);
    }
}
