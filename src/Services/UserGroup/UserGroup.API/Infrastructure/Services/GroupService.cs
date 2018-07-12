using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Models;
using Together.UserGroup.API.Infrastructure.Repositories;

namespace Together.UserGroup.API.Infrastructure.Services
{
    public class GroupService
        : BaseService<Group>, IGroupService
    {
        private readonly IGroupRepository _repository;
        public GroupService(IGroupRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<Group> CreateGroup(Group group)
        {
            if(_repository.Existed(g=>g.GroupName.Equals(group.GroupName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return null;
            }
            await _repository.AddAsync(group);
            await _repository.SaveChangesAsync();
            return group;
        }
    }
}
