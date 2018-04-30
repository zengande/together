using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Data;
using Together.UserGroup.API.Infrastructure.Models;

namespace Together.UserGroup.API.Infrastructure.Repositories
{
    public class UserRepository
        : BaseRepository<User>, IUserRepository
    {
        private readonly UserGroupDbContext _context;
        public UserRepository(UserGroupDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override async Task<User> GetAsync(int userId)
        {
            var user = await _context.Users
                .FindAsync(userId);
            if (user != null)
            {
                await _context.Entry(user)
                    .Reference(u => u.Group)
                    .LoadAsync();
            }
            return user;
        }
    }
}
