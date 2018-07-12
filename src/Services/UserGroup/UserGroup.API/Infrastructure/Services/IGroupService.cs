using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Models;

namespace Together.UserGroup.API.Infrastructure.Services
{
    public interface IGroupService
        : IBaseService<Group>
    {
        Task<Group> CreateGroup(Group group);
    }
}
