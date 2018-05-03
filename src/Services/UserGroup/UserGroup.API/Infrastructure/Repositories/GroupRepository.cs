using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Data;
using Together.UserGroup.API.Infrastructure.Models;

namespace Together.UserGroup.API.Infrastructure.Repositories
{
    public class GroupRepository
        : BaseRepository<Group>, IGroupRepository
    {
        private readonly UserGroupDbContext _context;
        public GroupRepository(UserGroupDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async override Task<Group> GetAsync(object Id)
        {
            var group = await _context.Groups
                .FindAsync(Id);
            if (group != null)
            {
                await _context.Entry(group)
                     .Collection(g => g.Members)
                     .LoadAsync();
            }
            return group;
        }
    }
}
