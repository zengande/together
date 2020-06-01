using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.BuildingBlocks.Domain;

namespace Together.BuildingBlocks.Infrastructure.Idempotency
{
    public class InMemoryRequestManager : IRequestManager
    {
        private static ConcurrentDictionary<Guid, ClientRequest> _requests = new ConcurrentDictionary<Guid, ClientRequest>();

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

            _requests.TryAdd(request.Id, request);
        }

        public Task<bool> ExistAsync(Guid id)
        {
            return Task.FromResult(_requests.ContainsKey(id));
        }
    }
}
