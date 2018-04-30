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
        public UserService(IUserRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
