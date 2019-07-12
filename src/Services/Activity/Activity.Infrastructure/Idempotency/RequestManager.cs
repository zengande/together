using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Together.Activity.Domain.Exceptions;
using Together.Activity.Infrastructure.Data;

namespace Together.Activity.Infrastructure.Idempotency
{
    public class RequestManager : IRequestManager
    {
        private readonly ActivityDbContext _context;
        public RequestManager(ActivityDbContext context)
        {
            _context = context;
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
                throw new ActivityDomainException($"Request with {id} already exists") :
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
