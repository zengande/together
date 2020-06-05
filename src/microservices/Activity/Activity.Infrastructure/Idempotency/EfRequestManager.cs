using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Infrastructure.Data;
using Together.BuildingBlocks.Domain;
using Together.BuildingBlocks.Infrastructure.Idempotency;

namespace Together.Activity.Infrastructure.Idempotency
{
    public class EfRequestManager : IRequestManager
    {
        private readonly ActivityDbContext _context;

        public EfRequestManager(ActivityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<bool> ExistAsync(Guid id)
        {
            var request = await _context.
                FindAsync<ClientRequest>(id);

            return request != null;
        }

        public async Task CreateRequestForCommandAsync<T>(Guid id)
        {
            var exists = await ExistAsync(id);

            var request = exists ?
                throw new DomainException($"Request with {id} already exists") :
                new ClientRequest()
                {
                    Id = id,
                    Name = typeof(T).Name,
                    Time = DateTime.UtcNow
                };

            _context.Add(request);

            await _context.SaveChangesAsync();
        }
    }
}
