using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Models;
using Together.UserGroup.API.Infrastructure.Repositories;

namespace Together.UserGroup.API.Infrastructure.Services
{
    public class UserService
        : BaseService<User>, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IGroupRepository _groupRepository;
        public UserService(IUserRepository repository,
            IGroupRepository groupRepository)
            : base(repository)
        {
            _repository = repository;
            _groupRepository = groupRepository;
        }

        public async Task<User> JoinGroup(int userId, int groupId)
        {
            var user = await _repository.GetAsync(userId);
            if (user != null)
            {
                var group = await _groupRepository.GetAsync(groupId);
                if (group != null)
                {
                    user.Group = group;
                    _repository.Update(user);
                    var result = await _repository.SaveChangesAsync();
                    return result ? user : null;
                }
            }
            return null;
        }
    }
}
